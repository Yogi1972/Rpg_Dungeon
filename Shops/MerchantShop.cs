using Night.Characters;
using Night.Shops;
using System;
using System.Collections.Generic;

namespace Rpg_Dungeon
{
    internal class MerchantShop : Shop
    {
        public override string ShopName => "General Goods (Merchant)";

        private readonly List<Backpack> _stock;
        private readonly List<StockEntry<Equipment>> _generalEquipment;

        public MerchantShop()
        {
            _stock = new List<Backpack>
            {
                new Backpack("Small Backpack", 5, 50),
                new Backpack("Medium Backpack", 10, 100),
                new Backpack("Large Backpack", 20, 200)
            };

            _generalEquipment = new List<StockEntry<Equipment>>
            {
                new StockEntry<Equipment>(new Torch("Basic Torch", 8, 10), 10, 20),
                new StockEntry<Equipment>(new Torch("Long-Burning Torch", 15, 20), 20, 12),
                new StockEntry<Equipment>(new Equipment("Lantern", EquipmentType.OffHand, 50, 35, hp: 5), 35, 8),
                new StockEntry<Equipment>(new Equipment("Sturdy Lantern", EquipmentType.OffHand, 75, 60, hp: 10, armor: 1), 60, 5),
            };
        }

        public override void OpenShop(List<Character> party)
        {
            if (party == null || party.Count == 0)
            {
                Console.WriteLine("No customers available.");
                return;
            }

            while (true)
            {
                Console.WriteLine("\nMerchant items:");
                for (int i = 0; i < _stock.Count; i++)
                {
                    var bp = _stock[i];
                    Console.WriteLine($"{i + 1}) {bp.Name} - +{bp.Slots} slots - {bp.Price} gold");
                }
                Console.WriteLine("E) Buy General Equipment");
                Console.WriteLine("S) Sell item");
                Console.WriteLine("0) Leave shop");
                Console.Write("Choose an item to buy: ");
                var input = Console.ReadLine() ?? string.Empty;
                if (input.Trim().Equals("0", StringComparison.OrdinalIgnoreCase)) return;
                if (input.Trim().Equals("E", StringComparison.OrdinalIgnoreCase))
                {
                    BuyEquipment(party, _generalEquipment);
                    continue;
                }
                if (input.Trim().Equals("S", StringComparison.OrdinalIgnoreCase))
                {
                    HandleSell(party);
                    continue;
                }

                if (!int.TryParse(input, out var choice) || choice < 1 || choice > _stock.Count)
                {
                    Console.WriteLine("Invalid selection.");
                    continue;
                }

                var selected = _stock[choice - 1];
                Console.WriteLine($"Who should buy the {selected.Name}? Enter party index (1-{party.Count}): ");
                for (int i = 0; i < party.Count; i++) Console.WriteLine($"{i + 1}) {party[i].Name} - {party[i].Inventory.Gold} gold");

                var who = Console.ReadLine() ?? string.Empty;
                if (!int.TryParse(who, out var idx) || idx < 1 || idx > party.Count)
                {
                    Console.WriteLine("Invalid party selection.");
                    continue;
                }

                var buyer = party[idx - 1];
                if (buyer.Inventory.SpendGold(selected.Price))
                {
                    buyer.Inventory.EquipBackpack(selected);
                    Console.WriteLine($"{buyer.Name} bought and equipped {selected.Name} (+{selected.Slots} slots).");
                }
                else
                {
                    Console.WriteLine($"{buyer.Name} doesn't have enough gold ({selected.Price} required).");
                }
            }
        }

        private void HandleSell(List<Character> party)
        {
            Console.WriteLine("Select seller (party member index):");
            for (int i = 0; i < party.Count; i++) Console.WriteLine($"{i + 1}) {party[i].Name} - {party[i].Inventory.Gold} gold");
            var who = Console.ReadLine() ?? string.Empty;
            if (!int.TryParse(who, out var idx) || idx < 1 || idx > party.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            var seller = party[idx - 1];
            var slots = seller.Inventory.Slots;
            Console.WriteLine($"Items for {seller.Name}:");
            for (int i = 0; i < slots.Count; i++)
            {
                var it = slots[i];
                Console.WriteLine($"{i + 1}) {(it == null ? "(empty)" : it.Name)}");
            }

            Console.WriteLine("Enter slot number to sell (0 to cancel):");
            var s = Console.ReadLine() ?? string.Empty;
            if (!int.TryParse(s, out var slotIdx) || slotIdx < 0 || slotIdx > slots.Count) { Console.WriteLine("Invalid slot."); return; }
            if (slotIdx == 0) return;
            var item = slots[slotIdx - 1];
            if (item == null)
            {
                Console.WriteLine("No item in that slot.");
                return;
            }

            int sellPrice = (int)Math.Ceiling(item.Price * 0.75);
            seller.Inventory.AddGold(sellPrice);
            seller.Inventory.RemoveItem(slotIdx - 1);
            Console.WriteLine($"{seller.Name} sold {item.Name} for {sellPrice} gold (merchant pays 75% of base price).");
        }
    }
}
