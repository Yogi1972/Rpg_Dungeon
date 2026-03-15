using Night.Characters;
using Night.Shops;
using System;
using System.Collections.Generic;

namespace Rpg_Dungeon
{
    internal class MageShop : Shop
    {
        public override string ShopName => "Arcane Emporium (Mage Shop)";

        private readonly List<StockEntry<GenericItem>> _magicItems;
        private readonly List<StockEntry<Equipment>> _magicEquipment;

        public MageShop()
        {
            _magicItems = new List<StockEntry<GenericItem>>
            {
                new StockEntry<GenericItem>(new GenericItem("Mana Crystal", 30), 30, 15),
                new StockEntry<GenericItem>(new GenericItem("Magic Dust", 15), 15, 25),
                new StockEntry<GenericItem>(new GenericItem("Arcane Dust", 20), 20, 20),
                new StockEntry<GenericItem>(new GenericItem("Spell Scroll", 40), 40, 10),
                new StockEntry<GenericItem>(new GenericItem("Potion of Mana", 25), 25, 20),
                new StockEntry<GenericItem>(new GenericItem("Enchanted Gem", 50), 50, 8)
            };

            _magicEquipment = new List<StockEntry<Equipment>>
            {
                new StockEntry<Equipment>(new Equipment("Apprentice Staff", EquipmentType.Weapon, 45, 70, intel: 6, mana: 15), 70, 4),
                new StockEntry<Equipment>(new Equipment("Arcane Staff", EquipmentType.Weapon, 55, 110, intel: 10, mana: 25), 110, 2),
                new StockEntry<Equipment>(new Equipment("Wizard's Ring", EquipmentType.Accessory, 30, 65, intel: 4, mana: 10), 65, 3),
                new StockEntry<Equipment>(new Equipment("Arcane Amulet", EquipmentType.Accessory, 35, 80, intel: 5, mana: 15), 80, 2),
                new StockEntry<Equipment>(new Equipment("Spell Tome", EquipmentType.OffHand, 50, 75, intel: 5, mana: 20), 75, 5),
                new StockEntry<Equipment>(new Equipment("Crystal Orb", EquipmentType.OffHand, 60, 110, intel: 8, mana: 30), 110, 3),
                new StockEntry<Equipment>(new Equipment("Arcane Codex", EquipmentType.OffHand, 70, 150, intel: 10, mana: 40), 150, 2),
                new StockEntry<Equipment>(new Equipment("Focus Crystal", EquipmentType.OffHand, 45, 65, intel: 4, mana: 15), 65, 4),
            };
        }

        public override void OpenShop(List<Character> party)
        {
            if (party == null || party.Count == 0)
            {
                Console.WriteLine("No customers.");
                return;
            }

            while (true)
            {
                Console.WriteLine("\nMage Shop: 1) Buy Magic Items  2) Sell Magic Items  3) Buy Magic Equipment  4) Enchant Equipment  0) Leave");
                Console.Write("Choice: ");
                var input = Console.ReadLine() ?? string.Empty;
                if (input.Trim() == "0") return;

                switch (input.Trim())
                {
                    case "1":
                        BuyFromStock(party, _magicItems, "magic items");
                        break;
                    case "2":
                        SellToStock(party, _magicItems, "Mage");
                        break;
                    case "3":
                        BuyEquipment(party, _magicEquipment);
                        break;
                    case "4":
                        EnchantEquipment(party);
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        private void EnchantEquipment(List<Character> party)
        {
            Console.WriteLine("Select who has equipment to enchant:");
            for (int i = 0; i < party.Count; i++) Console.WriteLine($"{i + 1}) {party[i].Name}");
            var who = Console.ReadLine() ?? string.Empty;
            if (!int.TryParse(who, out var whoIdx) || whoIdx < 1 || whoIdx > party.Count) { Console.WriteLine("Invalid."); return; }
            var c = party[whoIdx - 1];

            Console.WriteLine("Enchanting increases equipment durability by 20%.");
            Console.WriteLine("Cost: 50 gold + Magic Dust");

            if (c.Inventory.Gold < 50) { Console.WriteLine("Not enough gold."); return; }

            var slots = c.Inventory.Slots;
            int dustSlot = -1;
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i]?.Name == "Magic Dust")
                {
                    dustSlot = i;
                    break;
                }
            }

            if (dustSlot == -1) { Console.WriteLine("You need Magic Dust to enchant!"); return; }

            var equipmentSlots = new List<(int idx, Equipment eq)>();
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i] is Equipment eq)
                {
                    equipmentSlots.Add((i, eq));
                    Console.WriteLine($"{equipmentSlots.Count}) {eq.Name} [{eq.Type}] (Dur {eq.Durability}/{eq.MaxDurability})");
                }
            }

            if (equipmentSlots.Count == 0) { Console.WriteLine("No equipment to enchant."); return; }
            Console.WriteLine("Choose equipment to enchant:");
            var sel = Console.ReadLine() ?? string.Empty;
            if (!int.TryParse(sel, out var selIdx) || selIdx < 1 || selIdx > equipmentSlots.Count) { Console.WriteLine("Invalid."); return; }

            var chosen = equipmentSlots[selIdx - 1];
            c.Inventory.SpendGold(50);
            c.Inventory.RemoveItem(dustSlot);

            Console.WriteLine($"{chosen.eq.Name} has been enchanted! (This is a cosmetic feature - durability bonus not yet implemented)");
        }
    }
}
