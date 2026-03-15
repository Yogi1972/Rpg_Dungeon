using System;
using System.Diagnostics;
using System.Threading;

namespace Rpg_Dungeon
{
    internal static class Program
    {
        private static void Main()
        {
            try
            {
                Console.WriteLine("PROGRAM STARTING...");
                Console.WriteLine("Please wait...");
                Thread.Sleep(500);

                try
                {
                    Console.OutputEncoding = System.Text.Encoding.UTF8;
                    Console.WriteLine("UTF-8 encoding set.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"UTF-8 failed: {ex.Message}");
                    ErrorLogger.LogWarning($"UTF-8 encoding failed: {ex.Message}", "Non-critical - continuing with default encoding");
                }

                Console.WriteLine("Showing title screen...");
                Thread.Sleep(500);

                TitleScreenManager.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n╔══════════════════════════════════════════════════════════════════╗");
                Console.WriteLine("║                    CRITICAL ERROR                                ║");
                Console.WriteLine("╚══════════════════════════════════════════════════════════════════╝");
                Console.WriteLine($"\n❌ An unexpected error occurred: {ex.Message}");
                Console.WriteLine($"\n📋 Error Type: {ex.GetType().Name}");

                string logFilePath = ErrorLogger.LogCriticalError(ex, "Unhandled exception in Main()");

                if (!string.IsNullOrEmpty(logFilePath))
                {
                    Console.WriteLine($"\n💾 Error details saved to: {logFilePath}");
                    Console.WriteLine("\n📧 To report this error:");
                    Console.WriteLine($"   1. Navigate to: {ErrorLogger.GetLogDirectory()}");
                    Console.WriteLine("   2. Send the error log file to the developer");
                    Console.WriteLine("\n💡 You can also press 'O' to open the error log folder now.");
                }

                Console.WriteLine("\n📋 Stack Trace:");
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine("\nPress 'O' to open error logs folder, or Enter to exit...");

                var key = Console.ReadKey(true);
                if (key.KeyChar == 'O' || key.KeyChar == 'o')
                {
                    ErrorLogger.OpenLogDirectory();
                    Console.WriteLine("\nOpening error logs folder...");
                    Thread.Sleep(1000);
                }

                Environment.Exit(1);
            }
        }
    }
}
