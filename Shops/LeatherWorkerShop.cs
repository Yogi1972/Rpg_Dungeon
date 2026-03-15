using Night.Characters;
using Night.Shops;
using System;
using System.Collections.Generic;

namespace Rpg_Dungeon
{
    internal class LeatherWorkerShop : Shop
    {
        public override string ShopName => "Tanner's Workshop (Leather Worker)";

        private readonly List<StockEntry<GenericItem>> _leatherMaterials;
        private readonly List<StockEntry<Equipment>> _leatherGoods;

        public LeatherWorkerShop()
        {
            _leatherMaterials = new List<StockEntry<GenericItem>>
            {
                new StockEntry<GenericItem>(new GenericItem("Wolf Pelt", 8), 8, 30),
                new StockEntry<GenericItem>(new GenericItem("Troll Hide", 25), 25, 10),
                new StockEntry<GenericItem>(new GenericItem("Leather Scraps", 3), 3, 50),
                new StockEntry<GenericItem>(new GenericItem("Tanning Oil", 5), 5, 40)
            };

            _leatherGoods = new List<StockEntry<Equipment>>
            {
                new StockEntry<Equipment>(new Equipment("Leather Armor", EquipmentType.Armor, 40, 60, agi: 3, hp: 10), 60, 5),
                new StockEntry<Equipment>(new Equipment("Studded Leather", EquipmentType.Armor, 50, 85, agi: 5, hp: 15), 85, 3),
                new StockEntry<Equipment>(new Equipment("Leather Boots", EquipmentType.Accessory, 30, 40, agi: 4), 40, 6),
                new StockEntry<Equipment>(new Equipment("Leather Gloves", EquipmentType.Accessory, 25, 35, agi: 3), 35, 6),
                new StockEntry<Equipment>(new Equipment("Throwing Knife", EquipmentType.OffHand, 35, 50, agi: 4, str: 2), 50, 7),
                new StockEntry<Equipment>(new Equipment("Parrying Dagger", EquipmentType.OffHand, 40, 70, agi: 6, armor: 2), 70, 5),
                new StockEntry<Equipment>(new Equipment("Shadow Blade", EquipmentType.OffHand, 50, 95, agi: 8, str: 3), 95, 3),
                new StockEntry<Equipment>(new Equipment("Assassin's Dirk", EquipmentType.OffHand, 45, 85, agi: 7, str: 4), 85, 4),
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
                Console.WriteLine("\nLeather Worker: 1) Buy Materials  2) Sell Materials  3) Buy Leather Goods  4) Repair Leather  0) Leave");
                Console.Write("Choice: ");
                var input = Console.ReadLine() ?? string.Empty;
                if (input.Trim() == "0") return;

                switch (input.Trim())
                {
                    case "1":
                        BuyFromStock(party, _leatherMaterials, "leather materials");
                        break;
                    case "2":
                        SellToStock(party, _leatherMaterials, "Leather Worker");
                        break;
                    case "3":
                        BuyEquipment(party, _leatherGoods);
                        break;
                    case "4":
                        RepairEquipmentByType(party, EquipmentType.Armor, "leather");
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
    }
}
