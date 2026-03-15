using Night.Characters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rpg_Dungeon
{
    #region World Map Manager

    internal class WorldMap
    {
        #region Properties

        private readonly List<Location> _allLocations;
        private readonly GameState _gameState;

        #endregion

        #region Constructor

        public WorldMap(Weather weather, TimeOfDay timeTracker)
        {
            _allLocations = new List<Location>();
            _gameState = new GameState
            {
                Weather = weather,
                TimeTracker = timeTracker
            };
            InitializeWorld();
        }

        #endregion

        #region Initialization

        private void InitializeWorld()
        {
            InitializeMajorTowns();
            InitializeSettlements();
            InitializeCamps();
            SetupDistances();
        }

        private void InitializeMajorTowns()
        {
            // Town 1: Havenbrook (Starting Town - Trading Hub)
            var havenbrook = new MajorTown(
                "Havenbrook",
                "A bustling trade city at the crossroads of major routes. The starting point for many adventurers.",
                "Trade and Commerce",
                1
            );
            havenbrook.IsDiscovered = true; // Starting location
            _allLocations.Add(havenbrook);

            // Town 2: Ironforge Citadel (Mountain Town - Crafting)
            var ironforge = new MajorTown(
                "Ironforge Citadel",
                "A fortress city built into the mountain, renowned for its master craftsmen and legendary smiths.",
                "Blacksmithing and Armor Crafting",
                10
            );
            _allLocations.Add(ironforge);

            // Town 3: Mysthaven (Coastal Town - Magic)
            var mysthaven = new MajorTown(
                "Mysthaven",
                "A mysterious port city shrouded in mist, home to powerful mages and arcane academies.",
                "Magic and Enchanting",
                15
            );
            _allLocations.Add(mysthaven);

            // Town 4: Sunspire (Desert City - Rare Goods)
            var sunspire = new MajorTown(
                "Sunspire",
                "A golden city in the desert, famous for exotic goods and ancient treasures from tomb raiders.",
                "Exotic Goods and Artifacts",
                20
            );
            _allLocations.Add(sunspire);

            // Town 5: Shadowkeep (Dark City - Elite Services)
            var shadowkeep = new MajorTown(
                "Shadowkeep",
                "A dark gothic city for experienced adventurers, offering elite training and rare dark artifacts.",
                "Elite Training and Dark Arts",
                25
            );
            _allLocations.Add(shadowkeep);

            // Town 6: Crystalshore (Crystal Coast - Jewelry & Gems)
            var crystalshore = new MajorTown(
                "Crystalshore",
                "A magnificent coastal city built with crystalline architecture, famous for gem cutting and jewelry crafting.",
                "Jewelry and Gemcraft",
                12
            );
            _allLocations.Add(crystalshore);

            // Town 7: Emberpeak (Volcanic City - Alchemy & Fire Magic)
            var emberpeak = new MajorTown(
                "Emberpeak",
                "A city carved into an ancient volcano, where alchemists harness geothermal power for potent creations.",
                "Alchemy and Fire Crafts",
                18
            );
            _allLocations.Add(emberpeak);

            // Town 8: Stormwatch (Sky City - Navigation & Weather Magic)
            var stormwatch = new MajorTown(
                "Stormwatch",
                "A fortified city atop towering cliffs, home to storm mages and master navigators who chart the skies.",
                "Navigation and Storm Magic",
                22
            );
            _allLocations.Add(stormwatch);
        }

        private void InitializeSettlements()
        {
            // Settlements with varying services (all have inns)
            var settlements = new[]
            {
                new Settlement("Willowdale", "A peaceful farming village surrounded by wheat fields.", 1, true, true),
                new Settlement("Crossroads Keep", "A fortified waystation where three roads meet.", 3, true, true),
                new Settlement("Pinewood", "A logging community deep in the forest.", 5, true, false),
                new Settlement("Riverside", "A quiet fishing village by the great river.", 5, true, true),
                new Settlement("Stonebridge", "A settlement built around an ancient stone bridge.", 8, false, true),
                new Settlement("Frosthollow", "A cold mountain settlement of hardy folk.", 10, true, false),
                new Settlement("Oasis Rest", "A desert settlement around a precious water source.", 12, true, true),
                new Settlement("Moonwell", "A mystical settlement near a magical spring.", 15, false, false),
                new Settlement("Thornwall", "A walled settlement on the dangerous frontier.", 18, true, true),
                new Settlement("Ghostlight", "An eerie settlement near the haunted lands.", 20, false, true),
                new Settlement("Silvermist", "A foggy coastal village where sailors share tales of the deep.", 13, true, true),
                new Settlement("Copperhill", "A mining settlement extracting precious copper from the hills.", 16, true, false),
                new Settlement("Ravencrest", "A fortified settlement on a rocky outcrop, watching for dangers.", 24, false, true)
            };

            _allLocations.AddRange(settlements);
        }

        private void InitializeCamps()
        {
            // 23 Random camps across the world
            var camps = new[]
            {
                // Roadside Camps (5)
                new Camp("Traveler's Rest", "A common stop for merchants and adventurers.", CampType.Roadside),
                new Camp("Wagon Circle", "A defensive camp formation used by caravans.", CampType.Roadside),
                new Camp("Milestone Camp", "A camp marked by an ancient stone milestone.", CampType.Roadside),
                new Camp("Crossroads Camp", "A busy camp where multiple paths converge.", CampType.Roadside),
                new Camp("Guard Post", "An abandoned guard post turned makeshift camp.", CampType.Roadside),

                // Forest Camps (4)
                new Camp("Hunter's Clearing", "A camp used by local hunters.", CampType.Forest),
                new Camp("Woodcutter's Site", "A clearing with stumps and a fire pit.", CampType.Forest),
                new Camp("Druid Circle", "A sacred grove with ancient standing stones.", CampType.Forest),
                new Camp("Ranger Outpost", "A hidden camp used by forest rangers.", CampType.Forest),

                // Mountain Camps (4)
                new Camp("Eagle's Nest", "A high altitude camp with a commanding view.", CampType.Mountain),
                new Camp("Cave Shelter", "A natural cave offering protection from elements.", CampType.Mountain),
                new Camp("Mountain Pass Camp", "A camp at the highest point of the pass.", CampType.Mountain),
                new Camp("Windswept Ridge", "A camp on a windy mountain ridge with spectacular views.", CampType.Mountain),

                // Desert Camps (3)
                new Camp("Dune Hollow", "A depression in the sand providing shelter from wind.", CampType.Desert),
                new Camp("Nomad Circle", "A traditional circular camp of desert nomads.", CampType.Desert),
                new Camp("Rock Shelter", "A camp sheltered by large desert rocks.", CampType.Desert),

                // Riverside Camps (4)
                new Camp("Fisher's Camp", "A camp by the water with drying racks.", CampType.Riverside),
                new Camp("Ferry Landing", "A camp near an old ferry crossing.", CampType.Riverside),
                new Camp("Beaver Dam", "A camp near a large beaver dam.", CampType.Riverside),
                new Camp("Seaside Camp", "A coastal camp with the sound of crashing waves.", CampType.Riverside),

                // Ruins Camps (3)
                new Camp("Temple Steps", "A camp among crumbling temple ruins.", CampType.Ruins),
                new Camp("Old Fort", "A camp within the walls of an ancient fort.", CampType.Ruins),
                new Camp("Merchant's Rest", "A camp in the ruins of an old trading post.", CampType.Ruins)
            };

            _allLocations.AddRange(camps);
        }

        private void SetupDistances()
        {
            // Set up distances between major locations
            // This is a simplified system - in a full implementation, you'd calculate actual distances
            var rng = new Random(42); // Fixed seed for consistent distances

            foreach (var location in _allLocations)
            {
                foreach (var otherLocation in _allLocations)
                {
                    if (location != otherLocation && !location.DistancesToOtherLocations.ContainsKey(otherLocation.Name))
                    {
                        int distance = CalculateDistance(location, otherLocation, rng);
                        location.DistancesToOtherLocations[otherLocation.Name] = distance;
                        otherLocation.DistancesToOtherLocations[location.Name] = distance; // Symmetric
                    }
                }
            }
        }

        private int CalculateDistance(Location from, Location to, Random rng)
        {
            // Base distance depends on location types
            int baseDistance = (from.Type, to.Type) switch
            {
                (LocationCategory.Town, LocationCategory.Town) => rng.Next(50, 100),
                (LocationCategory.Town, LocationCategory.Settlement) => rng.Next(20, 50),
                (LocationCategory.Town, LocationCategory.Camp) => rng.Next(10, 30),
                (LocationCategory.Settlement, LocationCategory.Settlement) => rng.Next(15, 40),
                (LocationCategory.Settlement, LocationCategory.Camp) => rng.Next(5, 20),
                (LocationCategory.Camp, LocationCategory.Camp) => rng.Next(3, 15),
                _ => rng.Next(10, 30)
            };

            return baseDistance;
        }

        #endregion

        #region Map Interface

        public void ShowWorldMap(List<Character> party)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n╔════════════════════════════════════════════════╗");
                Console.WriteLine("║              WORLD MAP                         ║");
                Console.WriteLine("╚════════════════════════════════════════════════╝");

                if (_gameState.Weather != null)
                {
                    Console.WriteLine($"🌤️  Current Weather: {_gameState.Weather.GetWeatherDescription()}");
                }
                if (_gameState.TimeTracker != null)
                {
                    Console.WriteLine($"🕐 Current Time: {_gameState.TimeTracker.GetTimeDescription()}");
                }

                Console.WriteLine("\n--- Filter Locations ---");
                Console.WriteLine("1) Show All Discovered Locations");
                Console.WriteLine("2) Show Major Towns");
                Console.WriteLine("3) Show Settlements");
                Console.WriteLine("4) Show Camps");
                Console.WriteLine("5) Travel to Location");
                Console.WriteLine("0) Close Map");
                Console.Write("Choice: ");

                var choice = Console.ReadLine()?.Trim() ?? "";

                switch (choice)
                {
                    case "1":
                        ShowAllLocations();
                        break;
                    case "2":
                        ShowLocationsByType(LocationCategory.Town);
                        break;
                    case "3":
                        ShowLocationsByType(LocationCategory.Settlement);
                        break;
                    case "4":
                        ShowLocationsByType(LocationCategory.Camp);
                        break;
                    case "5":
                        TravelToLocation(party);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void ShowAllLocations()
        {
            Console.Clear();
            Console.WriteLine("\n=== DISCOVERED LOCATIONS ===\n");

            var discovered = _allLocations.Where(l => l.IsDiscovered).OrderBy(l => l.Type).ThenBy(l => l.Name).ToList();

            if (discovered.Count == 0)
            {
                Console.WriteLine("No locations discovered yet!");
            }
            else
            {
                foreach (var location in discovered)
                {
                    location.DisplayInfo();
                }

                Console.WriteLine($"\n📍 Total: {discovered.Count}/{_allLocations.Count} locations discovered");
            }

            Console.ReadKey();
        }

        private void ShowLocationsByType(LocationCategory type)
        {
            Console.Clear();
            Console.WriteLine($"\n=== {type.ToString().ToUpper()}S ===\n");

            var locations = _allLocations.Where(l => l.Type == type && l.IsDiscovered).OrderBy(l => l.Name).ToList();

            if (locations.Count == 0)
            {
                Console.WriteLine($"No {type}s discovered yet!");
            }
            else
            {
                foreach (var location in locations)
                {
                    location.DisplayInfo();
                }
            }

            Console.ReadKey();
        }

        private void TravelToLocation(List<Character> party)
        {
            Console.Clear();
            Console.WriteLine("\n=== TRAVEL ===\n");

            var discovered = _allLocations.Where(l => l.IsDiscovered).OrderBy(l => l.Name).ToList();

            if (discovered.Count == 0)
            {
                Console.WriteLine("No locations available for travel!");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < discovered.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {discovered[i].Name} ({discovered[i].Type})");
            }

            Console.WriteLine("0) Cancel");
            Console.Write("\nSelect destination: ");

            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= discovered.Count)
            {
                var destination = discovered[choice - 1];
                
                // Check level requirement
                if (party[0].Level < destination.RequiredLevel)
                {
                    Console.WriteLine($"\n⚠️  {destination.Name} requires level {destination.RequiredLevel}!");
                    Console.WriteLine($"   Your level: {party[0].Level}");
                    Console.ReadKey();
                    return;
                }

                // Simulate travel
                Console.WriteLine($"\n🗺️  Traveling to {destination.Name}...");
                
                if (_gameState.TimeTracker != null)
                {
                    _gameState.TimeTracker.AdvanceTime(2); // Travel takes time
                }

                Console.ReadKey();
                
                // Enter the location
                destination.Enter(party, _gameState);
            }
        }

        #endregion

        #region Helper Methods

        public void SetGameStateReferences(QuestBoard questBoard, BountyBoard bountyBoard, 
                                          AchievementTracker achievementTracker, Journal journal,
                                          NPCManager npcManager, MainStoryline mainStoryline, FogOfWarMap fogOfWarMap)
        {
            _gameState.QuestBoard = questBoard;
            _gameState.BountyBoard = bountyBoard;
            _gameState.AchievementTracker = achievementTracker;
            _gameState.Journal = journal;
            _gameState.NPCManager = npcManager;
            _gameState.MainStoryline = mainStoryline;
            _gameState.FogOfWarMap = fogOfWarMap;
        }

        public void DiscoverLocation(string locationName)
        {
            var location = _allLocations.FirstOrDefault(l => l.Name == locationName);
            if (location != null)
            {
                location.IsDiscovered = true;
                _gameState.FogOfWarMap?.DiscoverLocation(locationName);
                Console.WriteLine($"\n✨ {locationName} has been discovered!");
            }
        }

        public int GetDiscoveredCount() => _allLocations.Count(l => l.IsDiscovered);

        public int GetTotalLocationCount() => _allLocations.Count;

        #endregion
    }

    #endregion
}
