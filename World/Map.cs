using Night.Characters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rpg_Dungeon
{
    internal class Map
    {
        #region Properties

        private readonly List<Area> _areas;
        private Area? _currentArea;
        private readonly Weather _weather;
        private readonly TimeOfDay _timeTracker;

        #endregion

        #region Constructor

        public Map(Weather weather, TimeOfDay timeTracker)
        {
            _areas = new List<Area>();
            _weather = weather;
            _timeTracker = timeTracker;
            InitializeAreas();
        }

        #endregion

        #region Initialization

        private void InitializeAreas()
        {
            // Area 1: Starting Forest (Always unlocked)
            var startingForest = new Area("Whispering Woods", "A peaceful forest near the town, perfect for beginners.", 1, true);
            startingForest.SetWeather(_weather);
            startingForest.SetTimeTracker(_timeTracker);
            startingForest.Dungeons.Add(new DungeonLocation("Goblin Cave", 1, 2));
            startingForest.Dungeons.Add(new DungeonLocation("Abandoned Mine", 3, 3));
            startingForest.QuestSpots.Add(new QuestSpot("Herb Garden", "A small clearing with medicinal herbs.", "Healing Herb"));
            startingForest.QuestSpots.Add(new QuestSpot("Old Watchtower", "An abandoned watchtower with a view of the forest.", "Dungeon Level"));
            _areas.Add(startingForest);

            // Area 2: Dark Swamp
            var darkSwamp = new Area("Shadowfen Marsh", "A murky swamp filled with dangerous creatures.", 5, false);
            darkSwamp.SetWeather(_weather);
            darkSwamp.SetTimeTracker(_timeTracker);
            darkSwamp.Dungeons.Add(new DungeonLocation("Witch's Lair", 5, 3));
            darkSwamp.Dungeons.Add(new DungeonLocation("Sunken Temple", 7, 4));
            darkSwamp.QuestSpots.Add(new QuestSpot("Murky Pools", "Dark pools where rare ingredients can be found.", "Mana Crystal"));
            darkSwamp.QuestSpots.Add(new QuestSpot("Witch's Hut", "An eerie hut on stilts in the marsh.", "Dragon"));
            _areas.Add(darkSwamp);

            // Area 3: Mountain Pass
            var mountainPass = new Area("Frostpeak Mountains", "Treacherous mountain paths with ancient ruins.", 8, false);
            mountainPass.SetWeather(_weather);
            mountainPass.SetTimeTracker(_timeTracker);
            mountainPass.Dungeons.Add(new DungeonLocation("Ice Caverns", 8, 3));
            mountainPass.Dungeons.Add(new DungeonLocation("Dwarf Ruins", 10, 4));
            mountainPass.Dungeons.Add(new DungeonLocation("Dragon's Nest", 12, 5));
            mountainPass.QuestSpots.Add(new QuestSpot("Mountain Shrine", "An ancient shrine dedicated to forgotten gods.", "Dungeon Level"));
            mountainPass.QuestSpots.Add(new QuestSpot("Frozen Lake", "A lake frozen year-round with treasures beneath.", "Mana Crystal"));
            _areas.Add(mountainPass);

            // Area 4: Desert Wasteland
            var desertWasteland = new Area("Scorching Sands", "An endless desert with buried secrets.", 10, false);
            desertWasteland.SetWeather(_weather);
            desertWasteland.SetTimeTracker(_timeTracker);
            desertWasteland.Dungeons.Add(new DungeonLocation("Tomb of Kings", 10, 4));
            desertWasteland.Dungeons.Add(new DungeonLocation("Sand Worm Nest", 12, 3));
            desertWasteland.QuestSpots.Add(new QuestSpot("Oasis", "A rare water source in the desert.", "Healing Herb"));
            desertWasteland.QuestSpots.Add(new QuestSpot("Nomad Camp", "A temporary camp of desert traders.", "Goblin"));
            _areas.Add(desertWasteland);

            // Area 5: Haunted Cemetery
            var hauntedCemetery = new Area("Deathwhisper Graveyard", "A cursed cemetery where the dead don't rest.", 12, false);
            hauntedCemetery.SetWeather(_weather);
            hauntedCemetery.SetTimeTracker(_timeTracker);
            hauntedCemetery.Dungeons.Add(new DungeonLocation("Crypt of Shadows", 12, 4));
            hauntedCemetery.Dungeons.Add(new DungeonLocation("Necromancer's Tower", 15, 5));
            hauntedCemetery.QuestSpots.Add(new QuestSpot("Cursed Mausoleum", "A mausoleum emanating dark energy.", "Dragon"));
            hauntedCemetery.QuestSpots.Add(new QuestSpot("Bone Yard", "A field of ancient bones and forgotten graves.", "Skeleton"));
            _areas.Add(hauntedCemetery);

            // Area 6: Volcanic Wasteland
            var volcanicWasteland = new Area("Ashfall Crater", "A volcanic region with rivers of lava.", 15, false);
            volcanicWasteland.SetWeather(_weather);
            volcanicWasteland.SetTimeTracker(_timeTracker);
            volcanicWasteland.Dungeons.Add(new DungeonLocation("Lava Core", 15, 5));
            volcanicWasteland.Dungeons.Add(new DungeonLocation("Fire Elemental Forge", 17, 4));
            volcanicWasteland.QuestSpots.Add(new QuestSpot("Obsidian Cliffs", "Cliffs made of razor-sharp volcanic glass.", "Mana Crystal"));
            volcanicWasteland.QuestSpots.Add(new QuestSpot("Magma Falls", "A waterfall of molten rock.", "Fire Elemental"));
            _areas.Add(volcanicWasteland);

            // Area 7: Enchanted Forest
            var enchantedForest = new Area("Moonlight Glade", "A magical forest where reality bends.", 18, false);
            enchantedForest.SetWeather(_weather);
            enchantedForest.SetTimeTracker(_timeTracker);
            enchantedForest.Dungeons.Add(new DungeonLocation("Fairy Ring", 18, 3));
            enchantedForest.Dungeons.Add(new DungeonLocation("Ancient Tree Heart", 20, 5));
            enchantedForest.QuestSpots.Add(new QuestSpot("Pixie Grove", "A grove where pixies dance in moonlight.", "Healing Herb"));
            enchantedForest.QuestSpots.Add(new QuestSpot("Moonwell", "A well that reflects the moon even during day.", "Mana Crystal"));
            enchantedForest.QuestSpots.Add(new QuestSpot("Druid Circle", "A stone circle of ancient druids.", "Dungeon Level"));
            _areas.Add(enchantedForest);

            // Area 8: Coastal Cliffs
            var coastalCliffs = new Area("Stormbreaker Shores", "Treacherous cliffs overlooking a stormy sea.", 20, false);
            coastalCliffs.SetWeather(_weather);
            coastalCliffs.SetTimeTracker(_timeTracker);
            coastalCliffs.Dungeons.Add(new DungeonLocation("Smuggler's Cove", 20, 4));
            coastalCliffs.Dungeons.Add(new DungeonLocation("Kraken's Depth", 22, 5));
            coastalCliffs.Dungeons.Add(new DungeonLocation("Pirate Shipwreck", 21, 3));
            coastalCliffs.QuestSpots.Add(new QuestSpot("Lighthouse Ruins", "An abandoned lighthouse.", "Dungeon Level"));
            coastalCliffs.QuestSpots.Add(new QuestSpot("Tidal Pools", "Pools full of sea life and treasure.", "Mana Crystal"));
            _areas.Add(coastalCliffs);

            // Area 9: Corrupted Lands
            var corruptedLands = new Area("Blightlands", "A corrupted region twisted by dark magic.", 25, false);
            corruptedLands.SetWeather(_weather);
            corruptedLands.SetTimeTracker(_timeTracker);
            corruptedLands.Dungeons.Add(new DungeonLocation("Corruption Pit", 25, 5));
            corruptedLands.Dungeons.Add(new DungeonLocation("Dark Citadel", 27, 6));
            corruptedLands.QuestSpots.Add(new QuestSpot("Twisted Grove", "Trees warped by corruption.", "Healing Herb"));
            corruptedLands.QuestSpots.Add(new QuestSpot("Shadow Portal", "A portal leaking dark energy.", "Dragon"));
            _areas.Add(corruptedLands);

            // Area 10: Crystal Caverns
            var crystalCaverns = new Area("Luminous Depths", "Underground caverns filled with glowing crystals.", 28, false);
            crystalCaverns.SetWeather(_weather);
            crystalCaverns.SetTimeTracker(_timeTracker);
            crystalCaverns.Dungeons.Add(new DungeonLocation("Crystal Mines", 28, 5));
            crystalCaverns.Dungeons.Add(new DungeonLocation("Elemental Nexus", 30, 6));
            crystalCaverns.QuestSpots.Add(new QuestSpot("Crystal Garden", "A cavern where crystals grow like plants.", "Mana Crystal"));
            crystalCaverns.QuestSpots.Add(new QuestSpot("Echo Chamber", "A chamber that amplifies all sounds.", "Dungeon Level"));
            _areas.Add(crystalCaverns);

            // Area 11: Ancient Battleground
            var battleground = new Area("Fields of Valor", "The site of a legendary battle from ages past.", 32, false);
            battleground.SetWeather(_weather);
            battleground.SetTimeTracker(_timeTracker);
            battleground.Dungeons.Add(new DungeonLocation("War Catacombs", 32, 6));
            battleground.Dungeons.Add(new DungeonLocation("General's Tomb", 35, 5));
            battleground.QuestSpots.Add(new QuestSpot("Memorial Stone", "A monument to fallen heroes.", "Dungeon Level"));
            battleground.QuestSpots.Add(new QuestSpot("Battlefield Wreckage", "Remnants of ancient war machines.", "Orc"));
            battleground.QuestSpots.Add(new QuestSpot("Commander's Tent", "A preserved command post.", "Skeleton"));
            _areas.Add(battleground);

            // Area 12: Celestial Observatory
            var observatory = new Area("Starfall Peak", "The highest peak where stars touch the earth.", 35, false);
            observatory.SetWeather(_weather);
            observatory.SetTimeTracker(_timeTracker);
            observatory.Dungeons.Add(new DungeonLocation("Astral Tower", 35, 7));
            observatory.Dungeons.Add(new DungeonLocation("Void Rift", 38, 8));
            observatory.QuestSpots.Add(new QuestSpot("Observatory", "An ancient telescope pointed at the heavens.", "Mana Crystal"));
            observatory.QuestSpots.Add(new QuestSpot("Star Altar", "An altar where celestial energy gathers.", "Dragon"));
            _areas.Add(observatory);

            // Set starting area
            _currentArea = startingForest;
        }

        #endregion

        #region Map Navigation

        public void OpenMap(List<Character> party, QuestBoard questBoard, BountyBoard bountyBoard, 
                           AchievementTracker achievementTracker, Journal journal)
        {
            while (true)
            {
                Console.WriteLine("\n╔════════════════════════════════════════╗");
                Console.WriteLine("║            World Map                  ║");
                Console.WriteLine("╚════════════════════════════════════════╝");

                // Calculate party level for unlocking areas
                int partyLevel = party.Max(p => p.Level);

                // Unlock areas based on party level
                foreach (var area in _areas)
                {
                    if (!area.IsUnlocked && partyLevel >= area.RecommendedLevel)
                    {
                        area.IsUnlocked = true;
                        Console.WriteLine($"\n🎉 NEW AREA UNLOCKED: {area.Name}!");
                    }
                }

                Console.WriteLine($"\n📍 Current Location: {_currentArea?.Name ?? "Town"}");
                Console.WriteLine($"\n=== Available Areas ===");

                for (int i = 0; i < _areas.Count; i++)
                {
                    var area = _areas[i];
                    string status = area.IsUnlocked ? "✓" : "🔒";
                    string current = area == _currentArea ? " <- YOU ARE HERE" : "";
                    string levelInfo = area.IsUnlocked ? $"(Lv {area.RecommendedLevel})" : $"[Requires Lv {area.RecommendedLevel}]";
                    
                    Console.WriteLine($"{status} {i + 1}) {area.Name} {levelInfo}{current}");
                    
                    if (area.IsUnlocked)
                    {
                        int dungeonCount = area.Dungeons.Count;
                        int completedDungeons = area.Dungeons.Count(d => d.IsCompleted);
                        Console.WriteLine($"      🏰 Dungeons: {completedDungeons}/{dungeonCount} | 📜 Quest Spots: {area.QuestSpots.Count}");
                    }
                }

                Console.WriteLine("\n=== Options ===");
                Console.WriteLine("T) Travel to Area");
                Console.WriteLine("V) View Area Details");
                Console.WriteLine("E) Explore Current Area");
                Console.WriteLine("0) Return to Main Menu");
                Console.Write("Choose: ");

                var choice = Console.ReadLine() ?? string.Empty;

                switch (choice.Trim().ToUpper())
                {
                    case "T":
                        TravelToArea(party);
                        break;
                    case "V":
                        ViewAreaDetails();
                        break;
                    case "E":
                        if (_currentArea != null)
                        {
                            _currentArea.ExploreArea(party, questBoard, bountyBoard, achievementTracker, journal);
                        }
                        else
                        {
                            Console.WriteLine("You are currently in town. Select an area to travel to first.");
                        }
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        private void TravelToArea(List<Character> party)
        {
            Console.WriteLine("\n=== Travel to Area ===");
            var unlockedAreas = _areas.Where(a => a.IsUnlocked).ToList();

            if (unlockedAreas.Count == 0)
            {
                Console.WriteLine("No areas unlocked yet!");
                return;
            }

            for (int i = 0; i < unlockedAreas.Count; i++)
            {
                var area = unlockedAreas[i];
                string current = area == _currentArea ? " <- Current" : "";
                Console.WriteLine($"{i + 1}) {area.Name} (Lv {area.RecommendedLevel}){current}");
            }

            Console.Write("\nTravel to which area (or 0 to cancel): ");
            var input = Console.ReadLine() ?? string.Empty;
            if (!int.TryParse(input, out var idx) || idx < 1 || idx > unlockedAreas.Count)
            {
                return;
            }

            var selectedArea = unlockedAreas[idx - 1];
            
            if (selectedArea == _currentArea)
            {
                Console.WriteLine("You are already in this area!");
                return;
            }

            // Travel with weather effects
            Console.WriteLine($"\n🚶 Traveling from {_currentArea?.Name ?? "Town"} to {selectedArea.Name}...");
            TravelManager.TravelBetweenAreas(party, _weather, _timeTracker);

            _currentArea = selectedArea;
            Console.WriteLine($"\n✨ Arrived at {selectedArea.Name}!");
            selectedArea.DisplayAreaInfo();
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }

        private void ViewAreaDetails()
        {
            Console.WriteLine("\n=== View Area Details ===");
            
            for (int i = 0; i < _areas.Count; i++)
            {
                var area = _areas[i];
                if (area.IsUnlocked)
                {
                    Console.WriteLine($"{i + 1}) {area.Name}");
                }
                else
                {
                    Console.WriteLine($"{i + 1}) 🔒 ???");
                }
            }

            Console.Write("\nView which area (or 0 to cancel): ");
            var input = Console.ReadLine() ?? string.Empty;
            if (!int.TryParse(input, out var idx) || idx < 1 || idx > _areas.Count)
            {
                return;
            }

            var selectedArea = _areas[idx - 1];
            
            if (!selectedArea.IsUnlocked)
            {
                Console.WriteLine($"\n🔒 This area is locked. Reach level {selectedArea.RecommendedLevel} to unlock it.");
                return;
            }

            selectedArea.DisplayAreaInfo();
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }

        #endregion

        #region Helper Methods

        public Area? GetCurrentArea()
        {
            return _currentArea;
        }

        public List<Area> GetAllAreas()
        {
            return _areas;
        }

        public void SetCurrentArea(Area area)
        {
            if (_areas.Contains(area))
            {
                _currentArea = area;
            }
        }

        #endregion
    }
}
