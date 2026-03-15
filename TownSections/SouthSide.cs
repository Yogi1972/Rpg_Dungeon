using Night.Characters;
using Night.Shops;
using System;
using System.Collections.Generic;

namespace Rpg_Dungeon
{
    internal class SouthSide
    {
        #region Fields

        private readonly Shops _shops;
        private AchievementTracker? _achievementTracker;

        #endregion

        #region Constructor

        public SouthSide(Shops shops)
        {
            _shops = shops;
        }

        #endregion

        #region Setup Methods

        public void SetAchievementTracker(AchievementTracker? achievementTracker)
        {
            _achievementTracker = achievementTracker;
        }

        #endregion

        #region Public Methods

        public void EnterSouthSide(List<Character> party)
        {
            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║     South Side - Mystic Quarter       ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine("Mystical energy fills the air here.");
            Console.WriteLine("Healers and mages practice their arts.\n");

            while (true)
            {
                Console.WriteLine("\n--- South Side: Mystic Quarter ---");
                Console.WriteLine("1) Arcane Emporium (Mage Shop)");
                Console.WriteLine("2) Healing Hands (Apothecary)");
                Console.WriteLine("3) Hall of Fame (Achievements)");
                Console.WriteLine("0) Return to Town Square");
                Console.Write("Choice: ");

                var choice = Console.ReadLine() ?? string.Empty;

                switch (choice.Trim())
                {
                    case "1":
                        VisitMageShop(party);
                        break;
                    case "2":
                        VisitApothecary(party);
                        break;
                    case "3":
                        ViewAchievements(party);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        #endregion

        #region Private Visit Methods

        private void VisitMageShop(List<Character> party)
        {
            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║     Arcane Emporium (Mage Shop)       ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine("Mystical energies swirl around the shop.");
            Console.WriteLine("'Seeking arcane wares? You've come to the right place.'\n");
            _shops.OpenMageShop(party);
        }

        private void VisitApothecary(List<Character> party)
        {
            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║      Healing Hands (Apothecary)       ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine("Shelves of potions and herbs line the walls.");
            Console.WriteLine("'Need healing supplies? I have the finest potions!'\n");
            _shops.OpenApothecary(party);
        }

        private void ViewAchievements(List<Character> party)
        {
            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║         Hall of Fame - Achievements   ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine("Your greatest accomplishments are recorded here.\n");

            if (_achievementTracker == null)
            {
                Console.WriteLine("Achievement system unavailable!");
                return;
            }

            _achievementTracker.DisplayAchievements();

            // Option to claim rewards
            Console.Write("\nClaim rewards? (y/n): ");
            var claim = Console.ReadLine() ?? string.Empty;
            if (claim.Trim().Equals("y", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Which party member will claim rewards?");
                for (int i = 0; i < party.Count; i++)
                {
                    Console.WriteLine($"{i + 1}) {party[i].Name}");
                }
                var input = Console.ReadLine() ?? string.Empty;
                if (int.TryParse(input, out var idx) && idx >= 1 && idx <= party.Count)
                {
                    _achievementTracker.ClaimRewards(party[idx - 1]);
                }
            }
        }

        #endregion
    }
}
