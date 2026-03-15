using Night.Characters;
using Night.Shops;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rpg_Dungeon
{
    internal class JewelerShop : Shop
    {
        public override string ShopName => "Golden Trinkets (Jeweler)";

        private readonly List<StockEntry<Equipment>> _jewelry;

        public JewelerShop()
        {
            _jewelry = new List<StockEntry<Equipment>>
            {
                new StockEntry<Equipment>(new Equipment("Iron Necklace", EquipmentType.Necklace, 35, 55, str: 4, hp: 10), 55, 4),
                new StockEntry<Equipment>(new Equipment("Ring of Strength", EquipmentType.Ring, 30, 45, str: 5), 45, 6),
                new StockEntry<Equipment>(new Equipment("Berserker's Band", EquipmentType.Ring, 32, 50, str: 6, hp: 5), 50, 4),
                new StockEntry<Equipment>(new Equipment("Arcane Pendant", EquipmentType.Necklace, 30, 70, intel: 5, mana: 15), 70, 3),
                new StockEntry<Equipment>(new Equipment("Ring of Intellect", EquipmentType.Ring, 28, 50, intel: 6), 50, 5),
                new StockEntry<Equipment>(new Equipment("Mystic Circle", EquipmentType.Ring, 30, 55, intel: 5, mana: 10), 55, 4),
                new StockEntry<Equipment>(new Equipment("Shadow Amulet", EquipmentType.Necklace, 32, 65, agi: 5, hp: 8), 65, 3),
                new StockEntry<Equipment>(new Equipment("Ring of Agility", EquipmentType.Ring, 29, 48, agi: 6), 48, 5),
                new StockEntry<Equipment>(new Equipment("Thief's Signet", EquipmentType.Ring, 31, 52, agi: 5, str: 2), 52, 4),
                new StockEntry<Equipment>(new Equipment("Holy Symbol", EquipmentType.Necklace, 33, 68, intel: 5, mana: 12), 68, 3),
                new StockEntry<Equipment>(new Equipment("Ring of Restoration", EquipmentType.Ring, 30, 50, intel: 4, mana: 8), 50, 5),
                new StockEntry<Equipment>(new Equipment("Blessed Band", EquipmentType.Ring, 32, 54, intel: 5, hp: 10), 54, 4),
                new StockEntry<Equipment>(new Equipment("Golden Necklace", EquipmentType.Necklace, 40, 90, str: 3, agi: 3, intel: 3), 90, 2),
                new StockEntry<Equipment>(new Equipment("Dragon Scale Amulet", EquipmentType.Necklace, 45, 120, str: 5, agi: 5, intel: 5, hp: 20), 120, 1),
                new StockEntry<Equipment>(new Equipment("Ring of Protection", EquipmentType.Ring, 35, 60, hp: 15, mana: 10), 60, 3),
                new StockEntry<Equipment>(new Equipment("Platinum Band", EquipmentType.Ring, 38, 75, str: 4, agi: 4), 75, 2),
                new StockEntry<Equipment>(new Equipment("Ruby Ring", EquipmentType.Ring, 33, 65, str: 5, intel: 2), 65, 3),
                new StockEntry<Equipment>(new Equipment("Emerald Ring", EquipmentType.Ring, 33, 65, agi: 5, intel: 2), 65, 3)
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
                Console.WriteLine("\nJeweler: 1) Buy Jewelry  2) Sell Jewelry  3) Repair Jewelry  0) Leave");
                Console.Write("Choice: ");
                var input = Console.ReadLine() ?? string.Empty;
                if (input.Trim() == "0") return;

                switch (input.Trim())
                {
                    case "1":
                        BuyEquipment(party, _jewelry);
                        break;
                    case "2":
                        SellJewelry(party);
                        break;
                    case "3":
                        RepairJewelry(party);
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        private void SellJewelry(List<Character> party)
        {
            Console.WriteLine("Select seller (party member):");
            for (int i = 0; i < party.Count; i++) Console.WriteLine($"{i + 1}) {party[i].Name} - Gold: {party[i].Inventory.Gold}");
            var who = Console.ReadLine() ?? string.Empty;
            if (!int.TryParse(who, out var whoIdx) || whoIdx < 1 || whoIdx > party.Count) { Console.WriteLine("Invalid."); return; }
            var seller = party[whoIdx - 1];

            var slots = seller.Inventory.Slots;
            Console.WriteLine("Select slot number of jewelry to sell:");
            for (int i = 0; i < slots.Count; i++) Console.WriteLine($"{i + 1}) {(slots[i] == null ? "(empty)" : slots[i]!.Name)}");
            var s = Console.ReadLine() ?? string.Empty;
            if (!int.TryParse(s, out var slotIdx) || slotIdx < 1 || slotIdx > slots.Count) { Console.WriteLine("Invalid."); return; }
            var item = slots[slotIdx - 1];
            if (item == null) { Console.WriteLine("No item."); return; }

            if (item is not Equipment eq || (eq.Type != EquipmentType.Necklace && eq.Type != EquipmentType.Ring))
            {
                Console.WriteLine("This is not jewelry!");
                return;
            }

            var shopEntry = _jewelry.FirstOrDefault(j => string.Equals(j.Item.Name, item.Name, StringComparison.OrdinalIgnoreCase));
            int sellPrice;
            if (shopEntry != null)
            {
                sellPrice = (int)Math.Ceiling(shopEntry.Price * 0.75);
                shopEntry.Quantity++;
            }
            else
            {
                sellPrice = (int)Math.Ceiling(eq.Price * 0.75);
            }

            seller.Inventory.AddGold(sellPrice);
            seller.Inventory.RemoveItem(slotIdx - 1);
            Console.WriteLine($"{seller.Name} sold {item.Name} for {sellPrice} gold.");
        }

        private void RepairJewelry(List<Character> party)
        {
            Console.WriteLine("\n=== JEWELRY REPAIR SERVICE ===");
            Console.WriteLine("Select who needs jewelry repair:");
            for (int i = 0; i < party.Count; i++) 
            {
                Console.WriteLine($"{i + 1}) {party[i].Name} - Gold: {party[i].Inventory.Gold}");
            }
            var who = Console.ReadLine() ?? string.Empty;
            if (!int.TryParse(who, out var whoIdx) || whoIdx < 1 || whoIdx > party.Count) { Console.WriteLine("Invalid."); return; }
            var c = party[whoIdx - 1];

            Console.WriteLine($"\n{c.Name}'s Jewelry:");
            var repairableItems = new List<(string location, Equipment eq)>();
            int itemNum = 1;

            if (c.Inventory.EquippedNecklace != null)
            {
                var eq = c.Inventory.EquippedNecklace;
                string durStatus = eq.Durability <= eq.MaxDurability / 4 ? " [LOW!]" : "";
                Console.WriteLine($"{itemNum}) [EQUIPPED] Necklace: {eq.Name} (Dur: {eq.Durability}/{eq.MaxDurability}){durStatus} - Repair: {eq.RepairCost()}g");
                repairableItems.Add(("Necklace", eq));
                itemNum++;
            }

            if (c.Inventory.EquippedRing1 != null)
            {
                var eq = c.Inventory.EquippedRing1;
                string durStatus = eq.Durability <= eq.MaxDurability / 4 ? " [LOW!]" : "";
                Console.WriteLine($"{itemNum}) [EQUIPPED] Ring 1: {eq.Name} (Dur: {eq.Durability}/{eq.MaxDurability}){durStatus} - Repair: {eq.RepairCost()}g");
                repairableItems.Add(("Ring1", eq));
                itemNum++;
            }

            if (c.Inventory.EquippedRing2 != null)
            {
                var eq = c.Inventory.EquippedRing2;
                string durStatus = eq.Durability <= eq.MaxDurability / 4 ? " [LOW!]" : "";
                Console.WriteLine($"{itemNum}) [EQUIPPED] Ring 2: {eq.Name} (Dur: {eq.Durability}/{eq.MaxDurability}){durStatus} - Repair: {eq.RepairCost()}g");
                repairableItems.Add(("Ring2", eq));
                itemNum++;
            }

            var slots = c.Inventory.Slots;
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i] is Equipment eq && (eq.Type == EquipmentType.Necklace || eq.Type == EquipmentType.Ring))
                {
                    string durStatus = eq.Durability <= eq.MaxDurability / 4 ? " [LOW!]" : "";
                    Console.WriteLine($"{itemNum}) [Inventory Slot {i+1}]: {eq.Name} (Dur: {eq.Durability}/{eq.MaxDurability}){durStatus} - Repair: {eq.RepairCost()}g");
                    repairableItems.Add(($"Slot{i}", eq));
                    itemNum++;
                }
            }

            if (repairableItems.Count == 0) 
            { 
                Console.WriteLine("No jewelry to repair."); 
                return; 
            }

            Console.WriteLine("\nOptions:");
            Console.WriteLine("R) Repair All (pay full cost for all jewelry)");
            Console.WriteLine("0) Cancel");
            Console.Write("Choose jewelry number to repair or R for all: ");
            var sel = Console.ReadLine() ?? string.Empty;

            if (sel.Trim().Equals("R", StringComparison.OrdinalIgnoreCase))
            {
                int totalCost = repairableItems.Sum(item => item.eq.RepairCost());
                if (totalCost <= 0) 
                { 
                    Console.WriteLine("All jewelry is already at full durability!"); 
                    return; 
                }

                Console.WriteLine($"\nTotal repair cost for all jewelry: {totalCost} gold");
                Console.Write("Proceed with repair all? (y/n): ");
                var confirm = Console.ReadLine() ?? string.Empty;
                if (!confirm.Trim().Equals("y", StringComparison.OrdinalIgnoreCase)) 
                {
                    Console.WriteLine("Repair cancelled.");
                    return;
                }

                if (!c.Inventory.SpendGold(totalCost)) 
                { 
                    Console.WriteLine($"Not enough gold. You have {c.Inventory.Gold}g but need {totalCost}g."); 
                    return; 
                }

                foreach (var item in repairableItems)
                {
                    if (item.eq.RepairCost() > 0)
                    {
                        item.eq.Repair();
                        Console.WriteLine($"✓ {item.eq.Name} repaired to full durability.");
                    }
                }

                Console.WriteLine($"\n💎 All jewelry repaired! Gold remaining: {c.Inventory.Gold}");
            }
            else if (int.TryParse(sel, out var selIdx) && selIdx >= 1 && selIdx <= repairableItems.Count)
            {
                var chosen = repairableItems[selIdx - 1];
                int cost = chosen.eq.RepairCost();
                if (cost <= 0) { Console.WriteLine("Item is already at full durability!"); return; }
                if (!c.Inventory.SpendGold(cost)) 
                { 
                    Console.WriteLine($"Not enough gold. Repair costs {cost}g but you have {c.Inventory.Gold}g."); 
                    return; 
                }
                chosen.eq.Repair();
                Console.WriteLine($"💎 {chosen.eq.Name} repaired to full durability! Gold remaining: {c.Inventory.Gold}");
            }
            else
            {
                Console.WriteLine("Invalid selection.");
            }
        }
    }
}
