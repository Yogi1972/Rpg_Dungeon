using Night.Characters;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Rpg_Dungeon
{
    /// <summary>
    /// Provides an exciting opening sequence to hook players immediately
    /// </summary>
    internal static class OpeningHook
    {
        public static void ShowOpeningSequence()
        {
            Console.Clear();

            var originalColor = Console.ForegroundColor;

            // Dramatic opening
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\n\n");
            VisualEffects.TypewriterEffect("The ground trembles beneath your feet...", 40);
            Thread.Sleep(800);

            Console.ForegroundColor = ConsoleColor.Red;
            VisualEffects.TypewriterEffect("A monstrous roar echoes through the darkness...", 40);
            Thread.Sleep(800);

            Console.ForegroundColor = ConsoleColor.Yellow;
            VisualEffects.TypewriterEffect("This is your chance to prove yourself...", 40);
            Thread.Sleep(800);

            Console.ForegroundColor = originalColor;
            Console.WriteLine("\n");

            // Show dramatic enemy appearance
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("║                                                           ║");
            Console.WriteLine("║              A GOBLIN RAIDER ATTACKS!                     ║");
            Console.WriteLine("║                                                           ║");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝");
            Console.ForegroundColor = originalColor;

            Thread.Sleep(1000);

            Console.WriteLine("\nPress any key to defend yourself!");
            Console.ReadKey(true);

            // Quick tutorial combat
            ShowTutorialCombat();
        }

        private static void ShowTutorialCombat()
        {
            Console.Clear();

            VisualEffects.WriteLineColored("\n=== COMBAT TUTORIAL ===\n", ConsoleColor.Cyan);

            Console.WriteLine("You face a Goblin Raider!");
            Console.WriteLine("In combat, you'll roll a d20 (1-20) to determine success.");
            Console.WriteLine("Critical hits (roll 20) deal double damage!");
            Console.WriteLine("Critical fails (roll 1) miss completely!");
            Console.WriteLine();

            VisualEffects.WriteLineColored("Enemy: Goblin Raider [HP: 15]", ConsoleColor.Red);
            Console.WriteLine("Your Stats: [HP: 50] [Attack: +5]");
            Console.WriteLine();

            // Simulate a combat turn
            Console.WriteLine("Press Enter to attack...");
            Console.ReadLine();

            var rng = new Random();
            int roll = rng.Next(10, 21); // Guarantee a hit for tutorial
            int damage = 8;

            if (roll == 20)
            {
                VisualEffects.ShowCriticalHitEffect();
                Console.WriteLine($" {VisualEffects.GetRandomCritMessage()}");
                damage = 16;
            }

            Console.WriteLine($"\nYou roll d20: {roll} (+5 attack) = {roll + 5}");
            Thread.Sleep(500);

            VisualEffects.WriteDamage($"⚔️  You strike for {damage} damage!\n");
            Thread.Sleep(500);

            int goblinHp = 15 - damage;

            if (goblinHp > 0)
            {
                VisualEffects.WriteInfo($"Goblin HP: {goblinHp}/15\n");
                Console.WriteLine("\nThe goblin strikes back!");
                Thread.Sleep(500);
                Console.WriteLine("Goblin rolls: 8 (+3) = 11");
                Thread.Sleep(300);
                VisualEffects.WriteDamage("The goblin hits you for 3 damage!\n");
                Thread.Sleep(500);
                Console.WriteLine("Your HP: 47/50");
                Thread.Sleep(800);

                Console.WriteLine("\n\nPress Enter to finish the fight...");
                Console.ReadLine();

                Console.WriteLine($"\nYou roll d20: {rng.Next(12, 19)} (+5 attack)");
                Thread.Sleep(500);
                VisualEffects.WriteDamage($"⚔️  You strike for {damage} damage!\n");
                Thread.Sleep(500);
            }

            VisualEffects.ShowVictoryBanner();
            VisualEffects.WriteSuccess($"💀 Goblin Raider was {VisualEffects.GetRandomKillMessage()}\n");

            Console.WriteLine();
            VisualEffects.WriteSuccess("💰 You found 10 gold!\n");
            VisualEffects.WriteSuccess("⭐ You gained 25 XP!\n");

            Thread.Sleep(1000);

            Console.WriteLine();
            VisualEffects.WriteLineColored("Tutorial Complete! You're ready for adventure!", ConsoleColor.Green);
            Console.WriteLine();
            Console.WriteLine("In the full game, you'll:");
            Console.WriteLine("  • Create a custom character with race and class");
            Console.WriteLine("  • Explore dungeons and towns");
            Console.WriteLine("  • Complete quests and earn achievements");
            Console.WriteLine("  • Collect legendary equipment");
            Console.WriteLine("  • Master skills and upgrade to champion classes");
            Console.WriteLine();

            Console.WriteLine("Press any key to continue to the main menu...");
            Console.ReadKey(true);
        }

        public static bool ShouldShowOpening()
        {
            // Show opening hook for first-time players (can be expanded with save file check)
            Console.WriteLine("\n╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                           ║");
            Console.WriteLine("║     Would you like to see the opening sequence?          ║");
            Console.WriteLine("║     (Recommended for first-time players)                  ║");
            Console.WriteLine("║                                                           ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝");
            Console.WriteLine();
            Console.Write("Show opening? (y/n): ");

            var response = Console.ReadLine()?.Trim().ToLower();
            return response == "y" || response == "yes";
        }
    }
}
