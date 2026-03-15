using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Rpg_Dungeon.Systems
{
    /// <summary>
    /// Handles checking for game updates from GitHub
    /// </summary>
    internal static class UpdateChecker
    {
        private static readonly HttpClient _httpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(10)
        };

        /// <summary>
        /// Version information from GitHub
        /// </summary>
        public class VersionInfo
        {
            public int MajorVersion { get; set; }
            public int MinorVersion { get; set; }
            public int PatchVersion { get; set; }
            public string? PreReleaseTag { get; set; }
            public string? ReleaseNotes { get; set; }
            public string? ReleaseDate { get; set; }
        }

        /// <summary>
        /// Check for updates from GitHub
        /// </summary>
        public static async Task<VersionInfo?> CheckForUpdatesAsync()
        {
            try
            {
                _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Rpg-Dungeon-Crawler/1.0");

                var response = await _httpClient.GetStringAsync(VersionControl.GitHubVersionCheckUrl);
                var versionInfo = JsonSerializer.Deserialize<VersionInfo>(response, new JsonSerializerOptions 
                { 
                    PropertyNameCaseInsensitive = true 
                });

                return versionInfo;
            }
            catch (HttpRequestException ex)
            {
                ErrorLogger.LogWarning($"Failed to check for updates: {ex.Message}", "Network error or GitHub unavailable");
                return null;
            }
            catch (TaskCanceledException)
            {
                ErrorLogger.LogWarning("Update check timed out", "Network may be slow or unavailable");
                return null;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogWarning($"Update check failed: {ex.Message}", "Unexpected error during update check");
                return null;
            }
        }

        /// <summary>
        /// Compare versions and determine if update is available
        /// </summary>
        public static bool IsNewerVersion(VersionInfo remoteVersion)
        {
            if (remoteVersion == null) return false;

            if (remoteVersion.MajorVersion > VersionControl.MajorVersion) return true;
            if (remoteVersion.MajorVersion < VersionControl.MajorVersion) return false;

            if (remoteVersion.MinorVersion > VersionControl.MinorVersion) return true;
            if (remoteVersion.MinorVersion < VersionControl.MinorVersion) return false;

            if (remoteVersion.PatchVersion > VersionControl.PatchVersion) return true;
            if (remoteVersion.PatchVersion < VersionControl.PatchVersion) return false;

            // Same version but remote is stable and local is pre-release
            if (string.IsNullOrWhiteSpace(remoteVersion.PreReleaseTag) && !string.IsNullOrWhiteSpace(VersionControl.PreReleaseTag))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Show update check screen with results
        /// </summary>
        public static void ShowUpdateCheckScreen()
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                      CHECK FOR UPDATES                           ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine($"  📌 Current Version: {VersionControl.FullVersion}");
            Console.WriteLine($"  📅 Build Date: {VersionControl.BuildDate:yyyy-MM-dd}");
            Console.WriteLine();
            Console.WriteLine("  🔍 Checking for updates...");
            Console.WriteLine();

            try
            {
                var task = CheckForUpdatesAsync();
                task.Wait();
                var remoteVersion = task.Result;

                if (remoteVersion == null)
                {
                    Console.WriteLine("  ⚠️  Unable to check for updates.");
                    Console.WriteLine("  • Check your internet connection");
                    Console.WriteLine("  • GitHub may be temporarily unavailable");
                    Console.WriteLine();
                    Console.WriteLine("  💡 You can manually check for updates at:");
                    Console.WriteLine($"     {VersionControl.GitHubReleaseUrl}");
                }
                else if (IsNewerVersion(remoteVersion))
                {
                    Console.WriteLine("  🎉 NEW UPDATE AVAILABLE!");
                    Console.WriteLine();
                    Console.WriteLine($"  📦 Latest Version: {remoteVersion.MajorVersion}.{remoteVersion.MinorVersion}.{remoteVersion.PatchVersion}");
                    
                    if (!string.IsNullOrWhiteSpace(remoteVersion.PreReleaseTag))
                    {
                        Console.WriteLine($"  🚧 Pre-release: {remoteVersion.PreReleaseTag}");
                    }
                    
                    if (!string.IsNullOrWhiteSpace(remoteVersion.ReleaseDate))
                    {
                        Console.WriteLine($"  📅 Released: {remoteVersion.ReleaseDate}");
                    }
                    
                    Console.WriteLine();
                    
                    if (!string.IsNullOrWhiteSpace(remoteVersion.ReleaseNotes))
                    {
                        Console.WriteLine("  📝 What's New:");
                        Console.WriteLine($"     {remoteVersion.ReleaseNotes}");
                        Console.WriteLine();
                    }
                    
                    Console.WriteLine("  💡 Press 'D' to download now, or Enter to continue...");
                    var key = Console.ReadKey(true);
                    
                    if (key.KeyChar == 'D' || key.KeyChar == 'd')
                    {
                        OpenGitHubReleases();
                    }
                }
                else
                {
                    Console.WriteLine("  ✅ You're running the latest version!");
                    Console.WriteLine();
                    Console.WriteLine("  📌 No updates available at this time.");
                    
                    if (VersionControl.IsPreRelease)
                    {
                        Console.WriteLine();
                        Console.WriteLine("  🚧 You're running a pre-release version.");
                        Console.WriteLine("  💡 Check the dev repository for latest changes:");
                        Console.WriteLine($"     {VersionControl.GitHubDevUrl}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("  ❌ Error checking for updates:");
                Console.WriteLine($"     {ex.Message}");
                ErrorLogger.LogWarning($"Update check failed: {ex.Message}", "Error in ShowUpdateCheckScreen");
            }

            Console.WriteLine();
            Console.WriteLine("  🔗 GitHub Repositories:");
            Console.WriteLine($"     📦 Release: {VersionControl.GitHubReleaseUrl}");
            Console.WriteLine($"     🔧 Development: {VersionControl.GitHubDevUrl}");
            Console.WriteLine();
            Console.WriteLine("  💡 Press 'R' for releases, 'D' for dev repo, or Enter to return...");

            var finalKey = Console.ReadKey(true);
            if (finalKey.KeyChar == 'R' || finalKey.KeyChar == 'r')
            {
                OpenGitHubReleases();
            }
            else if (finalKey.KeyChar == 'D' || finalKey.KeyChar == 'd')
            {
                OpenGitHubDev();
            }
        }

        /// <summary>
        /// Open the GitHub releases page in the default browser
        /// </summary>
        public static void OpenGitHubReleases()
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("  🌐 Opening GitHub releases page...");
                Process.Start(new ProcessStartInfo
                {
                    FileName = VersionControl.GitHubReleaseUrl,
                    UseShellExecute = true
                });
                System.Threading.Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ❌ Failed to open browser: {ex.Message}");
                Console.WriteLine($"  📋 Copy this URL manually: {VersionControl.GitHubReleaseUrl}");
                ErrorLogger.LogWarning($"Failed to open GitHub URL: {ex.Message}", "Browser launch failed");
                System.Threading.Thread.Sleep(2000);
            }
        }

        /// <summary>
        /// Open the GitHub development repository in the default browser
        /// </summary>
        public static void OpenGitHubDev()
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("  🌐 Opening GitHub development repository...");
                Process.Start(new ProcessStartInfo
                {
                    FileName = VersionControl.GitHubDevUrl,
                    UseShellExecute = true
                });
                System.Threading.Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ❌ Failed to open browser: {ex.Message}");
                Console.WriteLine($"  📋 Copy this URL manually: {VersionControl.GitHubDevUrl}");
                ErrorLogger.LogWarning($"Failed to open GitHub URL: {ex.Message}", "Browser launch failed");
                System.Threading.Thread.Sleep(2000);
            }
        }
    }
}
