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
        /// Version information from GitHub Releases API
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
        /// GitHub API release response model
        /// </summary>
        private class GitHubRelease
        {
            public string? tag_name { get; set; }
            public string? name { get; set; }
            public string? body { get; set; }
            public string? published_at { get; set; }
            public bool prerelease { get; set; }
        }

        /// <summary>
        /// Check for updates from GitHub Releases API
        /// </summary>
        public static async Task<VersionInfo?> CheckForUpdatesAsync()
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("User-Agent", "Rpg-Dungeon-Crawler/3.0");
                _httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.github+json");
                _httpClient.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");

                var response = await _httpClient.GetStringAsync(VersionControl.GitHubVersionCheckUrl);
                var githubRelease = JsonSerializer.Deserialize<GitHubRelease>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (githubRelease == null || string.IsNullOrWhiteSpace(githubRelease.tag_name))
                {
                    return null;
                }

                return ParseGitHubRelease(githubRelease);
            }
            catch (HttpRequestException ex)
            {
                // Check if it's a 404 - no releases published yet
                if (ex.Message.Contains("404"))
                {
                    ErrorLogger.LogWarning("No GitHub releases published yet", "Update check skipped - no releases available");
                }
                else
                {
                    ErrorLogger.LogWarning($"Failed to check for updates: {ex.Message}", "Network error or GitHub unavailable");
                }
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
        /// Parse GitHub release info into VersionInfo
        /// </summary>
        private static VersionInfo ParseGitHubRelease(GitHubRelease release)
        {
            var versionInfo = new VersionInfo
            {
                ReleaseNotes = release.body,
                ReleaseDate = release.published_at
            };

            // Parse tag name (e.g., "v2.1.0" or "2.1.0-alpha")
            string tagName = release.tag_name?.TrimStart('v', 'V') ?? "0.0.0";

            // Split by '-' to separate version from pre-release tag
            string[] parts = tagName.Split('-');
            string versionPart = parts[0];

            if (parts.Length > 1)
            {
                versionInfo.PreReleaseTag = parts[1];
            }

            // Parse version numbers
            string[] versionNumbers = versionPart.Split('.');
            if (versionNumbers.Length >= 1 && int.TryParse(versionNumbers[0], out int major))
            {
                versionInfo.MajorVersion = major;
            }
            if (versionNumbers.Length >= 2 && int.TryParse(versionNumbers[1], out int minor))
            {
                versionInfo.MinorVersion = minor;
            }
            if (versionNumbers.Length >= 3 && int.TryParse(versionNumbers[2], out int patch))
            {
                versionInfo.PatchVersion = patch;
            }

            return versionInfo;
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
