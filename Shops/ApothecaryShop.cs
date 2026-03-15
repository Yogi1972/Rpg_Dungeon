using Night.Characters;
using Night.Shops;
using System;
using System.Collections.Generic;

namespace Rpg_Dungeon
{
    internal class ApothecaryShop : Shop
    {
        public override string ShopName => "Healing Hands (Apothecary)";

        private readonly List<StockEntry<GenericItem>> _potionsAndHerbs;
        private readonly List<StockEntry<Equipment>> _healingEquipment;

        public ApothecaryShop()
        {
            _potionsAndHerbs = new List<StockEntry<GenericItem>>
            {
                new StockEntry<GenericItem>(new GenericItem("Healing Potion", 20), 20, 30),
                new StockEntry<GenericItem>(new GenericItem("Greater Healing Potion", 45), 45, 15),
                new StockEntry<GenericItem>(new GenericItem("Healing Herb", 8), 8, 40),
                new StockEntry<GenericItem>(new GenericItem("Potion of Mana", 25), 25, 20),
                new StockEntry<GenericItem>(new GenericItem("Greater Mana Potion", 50), 50, 12),
                new StockEntry<GenericItem>(new GenericItem("Potion of Stamina", 25), 25, 20),
                new StockEntry<GenericItem>(new GenericItem("Greater Stamina Potion", 50), 50, 12),
                new StockEntry<GenericItem>(new GenericItem("Antidote", 15), 15, 20),
                new StockEntry<GenericItem>(new GenericItem("Restorative Salve", 25), 25, 18),
                new StockEntry<GenericItem>(new GenericItem("Bandages", 5), 5, 50),
                new StockEntry<GenericItem>(new GenericItem("Blessed Water", 30), 30, 12)
            };

            _healingEquipment = new List<StockEntry<Equipment>>
            {
                new StockEntry<Equipment>(new Equipment("Healing Pouch", EquipmentType.OffHand, 40, 55, intel: 2, hp: 15), 55, 6),
                new StockEntry<Equipment>(new Equipment("Medicine Bag", EquipmentType.OffHand, 50, 80, intel: 4, hp: 25, mana: 10), 80, 4),
                new StockEntry<Equipment>(new Equipment("Herbalist's Satchel", EquipmentType.OffHand, 60, 110, intel: 5, hp: 35, mana: 15), 110, 2),
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
                Console.WriteLine("\nApothecary: 1) Buy Potions & Herbs  2) Sell Ingredients  3) Brew Potion  4) Buy Healing Equipment  0) Leave");
                Console.Write("Choice: ");
                var input = Console.ReadLine() ?? string.Empty;
                if (input.Trim() == "0") return;

                switch (input.Trim())
                {
                    case "1":
                        BuyFromStock(party, _potionsAndHerbs, "potions and herbs");
                        break;
                    case "2":
                        SellToStock(party, _potionsAndHerbs, "Apothecary");
                        break;
                    case "3":
                        BrewPotion(party);
                        break;
                    case "4":
                        BuyEquipment(party, _healingEquipment);
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        private void BrewPotion(List<Character> party)
        {
            Console.WriteLine("Select who will brew:");
            for (int i = 0; i < party.Count; i++) Console.WriteLine($"{i + 1}) {party[i].Name}");
            var who = Console.ReadLine() ?? string.Empty;
            if (!int.TryParse(who, out var whoIdx) || whoIdx < 1 || whoIdx > party.Count) { Console.WriteLine("Invalid."); return; }
            var c = party[whoIdx - 1];

            Console.WriteLine("\nBrew recipes:");
            Console.WriteLine("1) Healing Potion - Requires 2x Healing Herb (Cost: 5 gold)");
            Console.WriteLine("2) Greater Healing Potion - Requires 1x Healing Potion + 1x Blessed Water (Cost: 15 gold)");
            Console.WriteLine("0) Cancel");

            var choice = Console.ReadLine() ?? string.Empty;
            if (choice == "0") return;

            if (choice == "1")
            {
                var slots = c.Inventory.Slots;
                var herbSlots = new List<int>();
                for (int i = 0; i < slots.Count; i++)
                {
                    if (slots[i]?.Name == "Healing Herb") herbSlots.Add(i);
                }

                if (herbSlots.Count < 2) { Console.WriteLine("Need 2 Healing Herbs!"); return; }
                if (c.Inventory.Gold < 5) { Console.WriteLine("Need 5 gold!"); return; }

                c.Inventory.SpendGold(5);
                c.Inventory.RemoveItem(herbSlots[0]);
                c.Inventory.RemoveItem(herbSlots[1]);
                c.Inventory.AddItem(new GenericItem("Healing Potion", 20));
                Console.WriteLine($"{c.Name} brewed a Healing Potion!");
            }
            else if (choice == "2")
            {
                var slots = c.Inventory.Slots;
                int potionSlot = -1;
                int waterSlot = -1;

                for (int i = 0; i < slots.Count; i++)
                {
                    if (slots[i]?.Name == "Healing Potion" && potionSlot == -1) potionSlot = i;
                    if (slots[i]?.Name == "Blessed Water" && waterSlot == -1) waterSlot = i;
                }

                if (potionSlot == -1 || waterSlot == -1) { Console.WriteLine("Need Healing Potion and Blessed Water!"); return; }
                if (c.Inventory.Gold < 15) { Console.WriteLine("Need 15 gold!"); return; }

                c.Inventory.SpendGold(15);
                c.Inventory.RemoveItem(potionSlot);
                c.Inventory.RemoveItem(waterSlot);
                c.Inventory.AddItem(new GenericItem("Greater Healing Potion", 45));
                Console.WriteLine($"{c.Name} brewed a Greater Healing Potion!");
            }
        }
    }
}
