using Night.Characters;
using Night.Shops;
using System;
using System.Collections.Generic;

namespace Rpg_Dungeon
{
    internal class TailorShop : Shop
    {
        public override string ShopName => "Fine Threads (Tailor)";

        private readonly List<StockEntry<GenericItem>> _clothMaterials;
        private readonly List<StockEntry<Equipment>> _clothGoods;

        public TailorShop()
        {
            _clothMaterials = new List<StockEntry<GenericItem>>
            {
                new StockEntry<GenericItem>(new GenericItem("Spider Silk", 10), 10, 25),
                new StockEntry<GenericItem>(new GenericItem("Enchanted Thread", 20), 20, 15),
                new StockEntry<GenericItem>(new GenericItem("Torn Cloth", 2), 2, 60),
                new StockEntry<GenericItem>(new GenericItem("Fine Linen", 12), 12, 20)
            };

            _clothGoods = new List<StockEntry<Equipment>>
            {
                new StockEntry<Equipment>(new Equipment("Cloth Robes", EquipmentType.Armor, 35, 50, intel: 4, mana: 10), 50, 5),
                new StockEntry<Equipment>(new Equipment("Mage Robes", EquipmentType.Armor, 40, 75, intel: 6, mana: 20), 75, 3),
                new StockEntry<Equipment>(new Equipment("Silk Cloak", EquipmentType.Accessory, 30, 55, intel: 3, mana: 15), 55, 4),
                new StockEntry<Equipment>(new Equipment("Priest's Vestments", EquipmentType.Armor, 38, 70, intel: 5, mana: 18), 70, 3),
                new StockEntry<Equipment>(new Equipment("Prayer Book", EquipmentType.OffHand, 40, 65, intel: 5, mana: 15, hp: 10), 65, 5),
                new StockEntry<Equipment>(new Equipment("Holy Symbol", EquipmentType.OffHand, 50, 85, intel: 6, mana: 20, hp: 15), 85, 4),
                new StockEntry<Equipment>(new Equipment("Blessed Tome", EquipmentType.OffHand, 60, 120, intel: 8, mana: 30, hp: 20), 120, 2),
                new StockEntry<Equipment>(new Equipment("Divine Relic", EquipmentType.OffHand, 70, 160, intel: 10, mana: 40, hp: 30), 160, 1),
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
                Console.WriteLine("\nTailor: 1) Buy Cloth Materials  2) Sell Materials  3) Buy Cloth Goods  4) Repair Cloth  0) Leave");
                Console.Write("Choice: ");
                var input = Console.ReadLine() ?? string.Empty;
                if (input.Trim() == "0") return;

                switch (input.Trim())
                {
                    case "1":
                        BuyFromStock(party, _clothMaterials, "cloth materials");
                        break;
                    case "2":
                        SellToStock(party, _clothMaterials, "Tailor");
                        break;
                    case "3":
                        BuyEquipment(party, _clothGoods);
                        break;
                    case "4":
                        RepairEquipmentByType(party, EquipmentType.Armor, "cloth");
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
    }
}
