using Night.Characters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rpg_Dungeon
{
    internal static class Crafting
    {
        // Forge Menu - Craft equipment from materials
        public static void OpenForge(List<Character> party)
        {
            if (party == null || party.Count == 0)
            {
                Console.WriteLine("No one to craft.");
                return;
            }

            while (true)
            {
                Console.WriteLine("\n=== FORGE ===");
                Console.WriteLine("Craft equipment from materials you've collected.");
                Console.WriteLine("\n1) Craft Weapon");
                Console.WriteLine("2) Craft Armor");
                Console.WriteLine("3) Craft Accessory");
                Console.WriteLine("4) Craft Torches");
                Console.WriteLine("5) View Recipes");
                Console.WriteLine("0) Leave Forge");
                Console.Write("Choice: ");
                var choice = Console.ReadLine() ?? string.Empty;

                switch (choice.Trim())
                {
                    case "1":
                        CraftWeapon(party);
                        break;
                    case "2":
                        CraftArmor(party);
                        break;
                    case "3":
                        CraftAccessory(party);
                        break;
                    case "4":
                        CraftTorches(party);
                        break;
                    case "5":
                        ShowForgeRecipes();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        // Potion Brewing Menu - For Mages and Priests
        public static void OpenPotionBrewing(List<Character> party)
        {
            if (party == null || party.Count == 0)
            {
                Console.WriteLine("No one to brew.");
                return;
            }

            // Only mages and priests can brew
            var brewers = party.Where(p => p is Mage || p is Priest).ToList();
            if (brewers.Count == 0)
            {
                Console.WriteLine("You need a Mage or Priest to brew potions!");
                return;
            }

            while (true)
            {
                Console.WriteLine("\n=== POTION BREWING ===");
                Console.WriteLine("Create powerful potions and elixirs.");
                Console.WriteLine("\n1) Brew Healing Potions");
                Console.WriteLine("2) Brew Mana Potions");
                Console.WriteLine("3) Brew Stamina Potions");
                Console.WriteLine("4) Brew Special Elixirs");
                Console.WriteLine("5) View Recipes");
                Console.WriteLine("0) Leave Brewing Station");
                Console.Write("Choice: ");
                var choice = Console.ReadLine() ?? string.Empty;

                switch (choice.Trim())
                {
                    case "1":
                        BrewHealingPotions(brewers);
                        break;
                    case "2":
                        BrewManaPotions(brewers);
                        break;
                    case "3":
                        BrewStaminaPotions(brewers);
                        break;
                    case "4":
                        BrewSpecialElixirs(brewers);
                        break;
                    case "5":
                        ShowBrewingRecipes();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        #region Forge Crafting

        private static void CraftWeapon(List<Character> party)
        {
            Console.WriteLine("\n=== WEAPON CRAFTING ===");
            Console.WriteLine("1) Iron Sword - 3x Iron Ore + 1x Leather Scraps (Dur: 50)");
            Console.WriteLine("2) Steel Blade - 2x Iron Ore + 1x Silver Ore (Dur: 60)");
            Console.WriteLine("3) Assassin's Dagger - 2x Copper Ore + 1x Fang (Dur: 40)");
            Console.WriteLine("4) Battle Axe - 4x Iron Ore + 1x Large Bone (Dur: 65)");
            Console.WriteLine("5) Enchanted Staff - 1x Gold Nugget + 2x Magic Dust + 1x Spider Silk (Dur: 55)");
            Console.WriteLine("0) Cancel");
            Console.Write("Choose weapon to craft: ");

            var choice = Console.ReadLine() ?? string.Empty;
            if (choice == "0") return;

            var crafter = SelectCrafter(party);
            if (crafter == null) return;

            bool success = false;
            switch (choice)
            {
                case "1":
                    success = CraftItem(crafter, 
                        new[] { ("Iron Ore", 3), ("Leather Scraps", 1) },
                        new Equipment("Iron Sword", EquipmentType.Weapon, 50, 80, str: 5),
                        "Iron Sword");
                    break;
                case "2":
                    success = CraftItem(crafter,
                        new[] { ("Iron Ore", 2), ("Silver Ore", 1) },
                        new Equipment("Steel Blade", EquipmentType.Weapon, 60, 110, str: 8),
                        "Steel Blade");
                    break;
                case "3":
                    success = CraftItem(crafter,
                        new[] { ("Copper Ore", 2), ("Fang", 1) },
                        new Equipment("Assassin's Dagger", EquipmentType.Weapon, 40, 70, agi: 7),
                        "Assassin's Dagger");
                    break;
                case "4":
                    success = CraftItem(crafter,
                        new[] { ("Iron Ore", 4), ("Large Bone", 1) },
                        new Equipment("Battle Axe", EquipmentType.Weapon, 65, 120, str: 10),
                        "Battle Axe");
                    break;
                case "5":
                    success = CraftItem(crafter,
                        new[] { ("Gold Nugget", 1), ("Magic Dust", 2), ("Spider Silk", 1) },
                        new Equipment("Enchanted Staff", EquipmentType.Weapon, 55, 150, intel: 9, mana: 20),
                        "Enchanted Staff");
                    break;
            }

            if (success)
                Console.WriteLine($"\n{crafter.Name} successfully crafted the weapon!");
        }

        private static void CraftArmor(List<Character> party)
        {
            Console.WriteLine("\n=== ARMOR CRAFTING ===");
            Console.WriteLine("1) Leather Armor - 3x Wolf Pelt + 1x Tanning Oil (Dur: 45)");
            Console.WriteLine("2) Chainmail - 5x Iron Ore + 2x Copper Ore (Dur: 70)");
            Console.WriteLine("3) Troll Hide Armor - 2x Troll Hide + 1x Large Bone (Dur: 80)");
            Console.WriteLine("4) Mage Robes - 3x Spider Silk + 1x Enchanted Thread (Dur: 40)");
            Console.WriteLine("5) Blessed Vestments - 2x Fine Linen + 1x Blessed Water (Dur: 50)");
            Console.WriteLine("0) Cancel");
            Console.Write("Choose armor to craft: ");

            var choice = Console.ReadLine() ?? string.Empty;
            if (choice == "0") return;

            var crafter = SelectCrafter(party);
            if (crafter == null) return;

            bool success = false;
            switch (choice)
            {
                case "1":
                    success = CraftItem(crafter,
                        new[] { ("Wolf Pelt", 3), ("Tanning Oil", 1) },
                        new Equipment("Leather Armor", EquipmentType.Armor, 45, 70, agi: 3, hp: 10),
                        "Leather Armor");
                    break;
                case "2":
                    success = CraftItem(crafter,
                        new[] { ("Iron Ore", 5), ("Copper Ore", 2) },
                        new Equipment("Chainmail", EquipmentType.Armor, 70, 130, str: 3, hp: 20),
                        "Chainmail");
                    break;
                case "3":
                    success = CraftItem(crafter,
                        new[] { ("Troll Hide", 2), ("Large Bone", 1) },
                        new Equipment("Troll Hide Armor", EquipmentType.Armor, 80, 150, str: 5, hp: 30),
                        "Troll Hide Armor");
                    break;
                case "4":
                    success = CraftItem(crafter,
                        new[] { ("Spider Silk", 3), ("Enchanted Thread", 1) },
                        new Equipment("Mage Robes", EquipmentType.Armor, 40, 90, intel: 6, mana: 20),
                        "Mage Robes");
                    break;
                case "5":
                    success = CraftItem(crafter,
                        new[] { ("Fine Linen", 2), ("Blessed Water", 1) },
                        new Equipment("Blessed Vestments", EquipmentType.Armor, 50, 100, intel: 5, mana: 18, hp: 15),
                        "Blessed Vestments");
                    break;
            }

            if (success)
                Console.WriteLine($"\n{crafter.Name} successfully crafted the armor!");
        }

        private static void CraftAccessory(List<Character> party)
        {
            Console.WriteLine("\n=== ACCESSORY CRAFTING ===");
            Console.WriteLine("1) Silver Ring - 2x Silver Ore + 1x Small Gem (Dur: 30)");
            Console.WriteLine("2) Golden Amulet - 1x Gold Nugget + 1x Large Gem (Dur: 40)");
            Console.WriteLine("3) Arcane Ring - 1x Enchanted Gem + 2x Arcane Dust (Dur: 35)");
            Console.WriteLine("4) Fang Necklace - 3x Fang + 1x Spider Silk (Dur: 25)");
            Console.WriteLine("5) Bone Charm - 2x Bone + 1x Magic Dust (Dur: 20)");
            Console.WriteLine("0) Cancel");
            Console.Write("Choose accessory to craft: ");

            var choice = Console.ReadLine() ?? string.Empty;
            if (choice == "0") return;

            var crafter = SelectCrafter(party);
            if (crafter == null) return;

            bool success = false;
            switch (choice)
            {
                case "1":
                    success = CraftItem(crafter,
                        new[] { ("Silver Ore", 2), ("Small Gem", 1) },
                        new Equipment("Silver Ring", EquipmentType.Accessory, 30, 60, agi: 4, intel: 2),
                        "Silver Ring");
                    break;
                case "2":
                    success = CraftItem(crafter,
                        new[] { ("Gold Nugget", 1), ("Large Gem", 1) },
                        new Equipment("Golden Amulet", EquipmentType.Accessory, 40, 100, str: 3, agi: 3, intel: 3),
                        "Golden Amulet");
                    break;
                case "3":
                    success = CraftItem(crafter,
                        new[] { ("Enchanted Gem", 1), ("Arcane Dust", 2) },
                        new Equipment("Arcane Ring", EquipmentType.Accessory, 35, 85, intel: 6, mana: 12),
                        "Arcane Ring");
                    break;
                case "4":
                    success = CraftItem(crafter,
                        new[] { ("Fang", 3), ("Spider Silk", 1) },
                        new Equipment("Fang Necklace", EquipmentType.Accessory, 25, 50, str: 4, agi: 2),
                        "Fang Necklace");
                    break;
                case "5":
                    success = CraftItem(crafter,
                        new[] { ("Bone", 2), ("Magic Dust", 1) },
                        new Equipment("Bone Charm", EquipmentType.Accessory, 20, 40, intel: 3, hp: 8),
                        "Bone Charm");
                    break;
            }

            if (success)
                Console.WriteLine($"\n{crafter.Name} successfully crafted the accessory!");
        }

        private static void CraftTorches(List<Character> party)
        {
            Console.WriteLine("\n=== TORCH CRAFTING ===");
            Console.WriteLine("1) Basic Torch - 1x Wood + 1x Cloth (Dur: 4 hours)");
            Console.WriteLine("2) Oil Torch - 1x Wood + 1x Oil Flask (Dur: 6 hours)");
            Console.WriteLine("3) Everburning Torch - 1x Iron Ore + 1x Magic Dust + 1x Oil Flask (Dur: 12 hours)");
            Console.WriteLine("0) Cancel");
            Console.Write("Choose torch to craft: ");

            var choice = Console.ReadLine() ?? string.Empty;
            if (choice == "0") return;

            var crafter = SelectCrafter(party);
            if (crafter == null) return;

            bool success = false;
            switch (choice)
            {
                case "1":
                    success = CraftTorchItem(crafter,
                        new[] { ("Wood", 1), ("Cloth", 1) },
                        new Torch("Basic Torch", 4, 5),
                        "Basic Torch");
                    break;
                case "2":
                    success = CraftTorchItem(crafter,
                        new[] { ("Wood", 1), ("Oil Flask", 1) },
                        new Torch("Oil Torch", 6, 12),
                        "Oil Torch");
                    break;
                case "3":
                    success = CraftTorchItem(crafter,
                        new[] { ("Iron Ore", 1), ("Magic Dust", 1), ("Oil Flask", 1) },
                        new Torch("Everburning Torch", 12, 35),
                        "Everburning Torch");
                    break;
            }

            if (success)
            {
                Console.WriteLine($"\n🔥 {crafter.Name} successfully crafted the torch!");
                Console.WriteLine("💡 Tip: Equip torches in your off-hand slot and light them for illumination!");
            }
        }

        private static void ShowForgeRecipes()
        {
            Console.WriteLine("\n=== FORGE RECIPES ===");
            Console.WriteLine("\n--- WEAPONS ---");
            Console.WriteLine("Iron Sword: 3x Iron Ore + 1x Leather Scraps");
            Console.WriteLine("Steel Blade: 2x Iron Ore + 1x Silver Ore");
            Console.WriteLine("Assassin's Dagger: 2x Copper Ore + 1x Fang");
            Console.WriteLine("Battle Axe: 4x Iron Ore + 1x Large Bone");
            Console.WriteLine("Enchanted Staff: 1x Gold Nugget + 2x Magic Dust + 1x Spider Silk");

            Console.WriteLine("\n--- ARMOR ---");
            Console.WriteLine("Leather Armor: 3x Wolf Pelt + 1x Tanning Oil");
            Console.WriteLine("Chainmail: 5x Iron Ore + 2x Copper Ore");
            Console.WriteLine("Troll Hide Armor: 2x Troll Hide + 1x Large Bone");
            Console.WriteLine("Mage Robes: 3x Spider Silk + 1x Enchanted Thread");
            Console.WriteLine("Blessed Vestments: 2x Fine Linen + 1x Blessed Water");

            Console.WriteLine("\n--- ACCESSORIES ---");
            Console.WriteLine("Silver Ring: 2x Silver Ore + 1x Small Gem");
            Console.WriteLine("Golden Amulet: 1x Gold Nugget + 1x Large Gem");
            Console.WriteLine("Arcane Ring: 1x Enchanted Gem + 2x Arcane Dust");
            Console.WriteLine("Fang Necklace: 3x Fang + 1x Spider Silk");
            Console.WriteLine("Bone Charm: 2x Bone + 1x Magic Dust");
        }

        #endregion

        #region Potion Brewing

        private static void BrewHealingPotions(List<Character> brewers)
        {
            Console.WriteLine("\n=== HEALING POTION RECIPES ===");
            Console.WriteLine("1) Healing Potion - 2x Healing Herb (Heals 40 HP)");
            Console.WriteLine("2) Greater Healing Potion - 1x Healing Potion + 1x Blessed Water (Heals 80 HP)");
            Console.WriteLine("3) Superior Healing Potion - 2x Greater Healing Potion + 1x Large Gem (Heals 150 HP)");
            Console.WriteLine("0) Cancel");
            Console.Write("Choose potion to brew: ");

            var choice = Console.ReadLine() ?? string.Empty;
            if (choice == "0") return;

            var brewer = SelectBrewer(brewers);
            if (brewer == null) return;

            bool success = false;
            switch (choice)
            {
                case "1":
                    success = BrewPotion(brewer,
                        new[] { ("Healing Herb", 2) },
                        new GenericItem("Healing Potion", 25),
                        5,
                        "Healing Potion");
                    break;
                case "2":
                    success = BrewPotion(brewer,
                        new[] { ("Healing Potion", 1), ("Blessed Water", 1) },
                        new GenericItem("Greater Healing Potion", 50),
                        15,
                        "Greater Healing Potion");
                    break;
                case "3":
                    success = BrewPotion(brewer,
                        new[] { ("Greater Healing Potion", 2), ("Large Gem", 1) },
                        new GenericItem("Superior Healing Potion", 100),
                        30,
                        "Superior Healing Potion");
                    break;
            }

            if (success)
            {
                int bonus = brewer.Intelligence / 5;
                Console.WriteLine($"\n{brewer.Name} successfully brewed the potion!");
                if (bonus > 0)
                    Console.WriteLine($"Intelligence bonus: +{bonus}% effectiveness!");
            }
        }

        private static void BrewManaPotions(List<Character> brewers)
        {
            Console.WriteLine("\n=== MANA POTION RECIPES ===");
            Console.WriteLine("1) Potion of Mana - 1x Mana Crystal + 1x Healing Herb (Restores 40 Mana)");
            Console.WriteLine("2) Greater Mana Potion - 2x Mana Crystal + 1x Arcane Dust (Restores 80 Mana)");
            Console.WriteLine("3) Elixir of Arcane Power - 1x Greater Mana Potion + 1x Enchanted Gem (Restores 150 Mana)");
            Console.WriteLine("0) Cancel");
            Console.Write("Choose potion to brew: ");

            var choice = Console.ReadLine() ?? string.Empty;
            if (choice == "0") return;

            var brewer = SelectBrewer(brewers);
            if (brewer == null) return;

            bool success = false;
            switch (choice)
            {
                case "1":
                    success = BrewPotion(brewer,
                        new[] { ("Mana Crystal", 1), ("Healing Herb", 1) },
                        new GenericItem("Potion of Mana", 30),
                        10,
                        "Potion of Mana");
                    break;
                case "2":
                    success = BrewPotion(brewer,
                        new[] { ("Mana Crystal", 2), ("Arcane Dust", 1) },
                        new GenericItem("Greater Mana Potion", 60),
                        20,
                        "Greater Mana Potion");
                    break;
                case "3":
                    success = BrewPotion(brewer,
                        new[] { ("Greater Mana Potion", 1), ("Enchanted Gem", 1) },
                        new GenericItem("Elixir of Arcane Power", 120),
                        40,
                        "Elixir of Arcane Power");
                    break;
            }

            if (success)
            {
                int bonus = brewer.Intelligence / 5;
                Console.WriteLine($"\n{brewer.Name} successfully brewed the potion!");
                if (bonus > 0)
                    Console.WriteLine($"Intelligence bonus: +{bonus}% effectiveness!");
            }
        }

        private static void BrewStaminaPotions(List<Character> brewers)
        {
            Console.WriteLine("\n=== STAMINA POTION RECIPES ===");
            Console.WriteLine("1) Potion of Stamina - 1x Raw Meat + 1x Healing Herb (Restores 40 Stamina)");
            Console.WriteLine("2) Greater Stamina Potion - 2x Raw Meat + 1x Troll Blood (Restores 80 Stamina)");
            Console.WriteLine("3) Elixir of Endurance - 1x Greater Stamina Potion + 1x Large Bone (Restores 150 Stamina)");
            Console.WriteLine("0) Cancel");
            Console.Write("Choose potion to brew: ");

            var choice = Console.ReadLine() ?? string.Empty;
            if (choice == "0") return;

            var brewer = SelectBrewer(brewers);
            if (brewer == null) return;

            bool success = false;
            switch (choice)
            {
                case "1":
                    success = BrewPotion(brewer,
                        new[] { ("Raw Meat", 1), ("Healing Herb", 1) },
                        new GenericItem("Potion of Stamina", 30),
                        10,
                        "Potion of Stamina");
                    break;
                case "2":
                    success = BrewPotion(brewer,
                        new[] { ("Raw Meat", 2), ("Troll Blood", 1) },
                        new GenericItem("Greater Stamina Potion", 60),
                        20,
                        "Greater Stamina Potion");
                    break;
                case "3":
                    success = BrewPotion(brewer,
                        new[] { ("Greater Stamina Potion", 1), ("Large Bone", 1) },
                        new GenericItem("Elixir of Endurance", 120),
                        40,
                        "Elixir of Endurance");
                    break;
            }

            if (success)
            {
                int bonus = brewer.Intelligence / 5;
                Console.WriteLine($"\n{brewer.Name} successfully brewed the potion!");
                if (bonus > 0)
                    Console.WriteLine($"Intelligence bonus: +{bonus}% effectiveness!");
            }
        }

        private static void BrewSpecialElixirs(List<Character> brewers)
        {
            Console.WriteLine("\n=== SPECIAL ELIXIR RECIPES ===");
            Console.WriteLine("1) Elixir of Strength - 1x Troll Blood + 1x Large Bone (Temp +5 Str)");
            Console.WriteLine("2) Elixir of Agility - 1x Venom Sac + 1x Spider Leg (Temp +5 Agi)");
            Console.WriteLine("3) Elixir of Intelligence - 2x Magic Dust + 1x Spell Scroll (Temp +5 Int)");
            Console.WriteLine("4) Elixir of Vitality - 1x Healing Potion + 1x Potion of Mana + 1x Restorative Salve (Restore All)");
            Console.WriteLine("5) Phoenix Elixir - 1x Superior Healing Potion + 1x Gold Nugget + 1x Enchanted Gem (Revive fallen ally)");
            Console.WriteLine("0) Cancel");
            Console.Write("Choose elixir to brew: ");

            var choice = Console.ReadLine() ?? string.Empty;
            if (choice == "0") return;

            var brewer = SelectBrewer(brewers);
            if (brewer == null) return;

            bool success = false;
            switch (choice)
            {
                case "1":
                    success = BrewPotion(brewer,
                        new[] { ("Troll Blood", 1), ("Large Bone", 1) },
                        new GenericItem("Elixir of Strength", 80),
                        25,
                        "Elixir of Strength");
                    break;
                case "2":
                    success = BrewPotion(brewer,
                        new[] { ("Venom Sac", 1), ("Spider Leg", 1) },
                        new GenericItem("Elixir of Agility", 80),
                        25,
                        "Elixir of Agility");
                    break;
                case "3":
                    success = BrewPotion(brewer,
                        new[] { ("Magic Dust", 2), ("Spell Scroll", 1) },
                        new GenericItem("Elixir of Intelligence", 90),
                        30,
                        "Elixir of Intelligence");
                    break;
                case "4":
                    success = BrewPotion(brewer,
                        new[] { ("Healing Potion", 1), ("Potion of Mana", 1), ("Restorative Salve", 1) },
                        new GenericItem("Elixir of Vitality", 120),
                        35,
                        "Elixir of Vitality");
                    break;
                case "5":
                    success = BrewPotion(brewer,
                        new[] { ("Superior Healing Potion", 1), ("Gold Nugget", 1), ("Enchanted Gem", 1) },
                        new GenericItem("Phoenix Elixir", 200),
                        50,
                        "Phoenix Elixir");
                    break;
            }

            if (success)
            {
                Console.WriteLine($"\n{brewer.Name} successfully brewed the elixir!");
                Console.WriteLine("Note: Special effects not yet implemented - currently works as consumable item.");
            }
        }

        private static void ShowBrewingRecipes()
        {
            Console.WriteLine("\n=== BREWING RECIPES ===");
            Console.WriteLine("\n--- HEALING POTIONS ---");
            Console.WriteLine("Healing Potion: 2x Healing Herb + 5 gold");
            Console.WriteLine("Greater Healing Potion: 1x Healing Potion + 1x Blessed Water + 15 gold");
            Console.WriteLine("Superior Healing Potion: 2x Greater Healing Potion + 1x Large Gem + 30 gold");

            Console.WriteLine("\n--- MANA POTIONS ---");
            Console.WriteLine("Potion of Mana: 1x Mana Crystal + 1x Healing Herb + 10 gold");
            Console.WriteLine("Greater Mana Potion: 2x Mana Crystal + 1x Arcane Dust + 20 gold");
            Console.WriteLine("Elixir of Arcane Power: 1x Greater Mana Potion + 1x Enchanted Gem + 40 gold");

            Console.WriteLine("\n--- STAMINA POTIONS ---");
            Console.WriteLine("Potion of Stamina: 1x Raw Meat + 1x Healing Herb + 10 gold");
            Console.WriteLine("Greater Stamina Potion: 2x Raw Meat + 1x Troll Blood + 20 gold");
            Console.WriteLine("Elixir of Endurance: 1x Greater Stamina Potion + 1x Large Bone + 40 gold");

            Console.WriteLine("\n--- SPECIAL ELIXIRS ---");
            Console.WriteLine("Elixir of Strength: 1x Troll Blood + 1x Large Bone + 25 gold");
            Console.WriteLine("Elixir of Agility: 1x Venom Sac + 1x Spider Leg + 25 gold");
            Console.WriteLine("Elixir of Intelligence: 2x Magic Dust + 1x Spell Scroll + 30 gold");
            Console.WriteLine("Elixir of Vitality: 1x Healing Potion + 1x Potion of Mana + 1x Restorative Salve + 35 gold");
            Console.WriteLine("Phoenix Elixir: 1x Superior Healing Potion + 1x Gold Nugget + 1x Enchanted Gem + 50 gold");
        }

        #endregion

        #region Helper Methods

        private static Character? SelectCrafter(List<Character> party)
        {
            Console.WriteLine("\nSelect crafter:");
            for (int i = 0; i < party.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {party[i].Name} - Gold: {party[i].Inventory.Gold}");
            }
            Console.Write("Choice: ");
            var s = Console.ReadLine() ?? string.Empty;
            if (!int.TryParse(s, out var idx) || idx < 1 || idx > party.Count)
            {
                Console.WriteLine("Invalid selection.");
                return null;
            }
            return party[idx - 1];
        }

        private static Character? SelectBrewer(List<Character> brewers)
        {
            Console.WriteLine("\nSelect brewer (Mage/Priest):");
            for (int i = 0; i < brewers.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {brewers[i].Name} ({brewers[i].GetType().Name}) - Gold: {brewers[i].Inventory.Gold}, Int: {brewers[i].Intelligence}");
            }
            Console.Write("Choice: ");
            var s = Console.ReadLine() ?? string.Empty;
            if (!int.TryParse(s, out var idx) || idx < 1 || idx > brewers.Count)
            {
                Console.WriteLine("Invalid selection.");
                return null;
            }
            return brewers[idx - 1];
        }

        private static bool CraftItem(Character crafter, (string name, int count)[] materials, Equipment result, string itemName)
        {
            // Check if crafter has all materials
            var slots = crafter.Inventory.Slots;
            var materialSlots = new Dictionary<string, List<int>>();

            foreach (var (name, count) in materials)
            {
                materialSlots[name] = new List<int>();
                for (int i = 0; i < slots.Count; i++)
                {
                    if (slots[i]?.Name == name)
                    {
                        materialSlots[name].Add(i);
                    }
                }

                if (materialSlots[name].Count < count)
                {
                    Console.WriteLine($"Missing materials! Need {count}x {name}, have {materialSlots[name].Count}.");
                    return false;
                }
            }

            // Remove materials
            foreach (var (name, count) in materials)
            {
                for (int i = 0; i < count; i++)
                {
                    crafter.Inventory.RemoveItem(materialSlots[name][i]);
                }
            }

            // Add crafted item
            if (crafter.Inventory.AddItem(result))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Inventory full! Item crafted but dropped.");
                // Could add materials back here, but leaving as is for simplicity
                return false;
            }
        }

        private static bool BrewPotion(Character brewer, (string name, int count)[] materials, GenericItem result, int goldCost, string potionName)
        {
            // Check gold
            if (brewer.Inventory.Gold < goldCost)
            {
                Console.WriteLine($"Not enough gold! Need {goldCost} gold.");
                return false;
            }

            // Check if brewer has all materials
            var slots = brewer.Inventory.Slots;
            var materialSlots = new Dictionary<string, List<int>>();

            foreach (var (name, count) in materials)
            {
                materialSlots[name] = new List<int>();
                for (int i = 0; i < slots.Count; i++)
                {
                    if (slots[i]?.Name == name)
                    {
                        materialSlots[name].Add(i);
                    }
                }

                if (materialSlots[name].Count < count)
                {
                    Console.WriteLine($"Missing ingredients! Need {count}x {name}, have {materialSlots[name].Count}.");
                    return false;
                }
            }

            // Spend gold
            brewer.Inventory.SpendGold(goldCost);

            // Remove materials
            foreach (var (name, count) in materials)
            {
                for (int i = 0; i < count; i++)
                {
                    brewer.Inventory.RemoveItem(materialSlots[name][i]);
                }
            }

            // Add brewed potion
            if (brewer.Inventory.AddItem(result))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Inventory full! Potion brewed but spilled.");
                return false;
            }
        }

        private static bool CraftTorchItem(Character crafter, (string name, int count)[] materials, Torch result, string itemName)
        {
            // Check if crafter has all materials
            var slots = crafter.Inventory.Slots;
            var materialSlots = new Dictionary<string, List<int>>();

            foreach (var (name, count) in materials)
            {
                materialSlots[name] = new List<int>();
                for (int i = 0; i < slots.Count; i++)
                {
                    if (slots[i]?.Name == name)
                    {
                        materialSlots[name].Add(i);
                    }
                }

                if (materialSlots[name].Count < count)
                {
                    Console.WriteLine($"Missing materials! Need {count}x {name}, have {materialSlots[name].Count}.");
                    return false;
                }
            }

            // Remove materials
            foreach (var (name, count) in materials)
            {
                for (int i = 0; i < count; i++)
                {
                    crafter.Inventory.RemoveItem(materialSlots[name][i]);
                }
            }

            // Add crafted torch
            if (crafter.Inventory.AddItem(result))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Inventory full! Torch crafted but dropped.");
                return false;
            }
        }

        #endregion
    }
}
