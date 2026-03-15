using Night.Characters;
using Night.Shops;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rpg_Dungeon
{
    internal class Town
    {
        #region Fields

        private readonly Shops _shops;
        private readonly NorthSide _northSide;
        private readonly EastSide _eastSide;
        private readonly SouthSide _southSide;
        private readonly WestSide _westSide;
        private readonly CentralSquare _centralSquare;
        private readonly Trading _trading;

        #endregion

        #region Constructor

        public Town()
        {
            _shops = new Shops();
            _northSide = new NorthSide(_shops);
            _eastSide = new EastSide();
            _southSide = new SouthSide(_shops);
            _westSide = new WestSide(_shops);
            _centralSquare = new CentralSquare();
            _trading = new Trading();
        }

        #endregion

        #region Setup Methods

        public void SetTrackers(QuestBoard questBoard, BountyBoard bountyBoard, AchievementTracker achievementTracker)
        {
            _eastSide.SetTrackers(questBoard, bountyBoard);
            _southSide.SetAchievementTracker(achievementTracker);
        }

        public void SetJournal(Journal journal)
        {
            _eastSide.SetJournal(journal);
        }

        public void SetWeather(Weather weather)
        {
            _eastSide.SetWeather(weather);
            _centralSquare.SetWeather(weather);
        }

        public void SetTimeTracker(TimeOfDay timeTracker)
        {
            _eastSide.SetTimeTracker(timeTracker);
            _centralSquare.SetTimeTracker(timeTracker);
        }

        #endregion

        #region Town Interface

        public void EnterTown(List<Character> party)
        {
            if (party == null || party.Count == 0)
            {
                Console.WriteLine("You have no party to enter town with.");
                return;
            }

            while (true)
            {
                _centralSquare.ShowCentralSquare(party);

                Console.WriteLine("\n--- Town Directions ---");
                Console.WriteLine("Where would you like to go?");
                Console.WriteLine("1) North Side (Crafters District)");
                Console.WriteLine("2) East Side (Service Quarter)");
                Console.WriteLine("3) South Side (Mystic Quarter)");
                Console.WriteLine("4) West Side (Entertainment District)");
                Console.WriteLine("5) Trade with Party Members");
                Console.WriteLine("0) Leave Town");
                Console.Write("Choice: ");

                var choice = Console.ReadLine() ?? string.Empty;

                switch (choice.Trim())
                {
                    case "1":
                        _northSide.EnterNorthSide(party);
                        break;
                    case "2":
                        _eastSide.EnterEastSide(party);
                        break;
                    case "3":
                        _southSide.EnterSouthSide(party);
                        break;
                    case "4":
                        _westSide.EnterWestSide(party);
                        break;
                    case "5":
                        _trading.OpenTradeMenu(party);
                        break;
                    case "0":
                        Console.WriteLine("\nYou leave the town and head back on the road.");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please choose 0-5.");
                        break;
                }
            }
        }

        #endregion
    }
}
