using Night.Characters;
using Night.Shops;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rpg_Dungeon
{
    internal class BlacksmithShop : Shop
    {
        public override string ShopName => "The Rusty Anvil (Blacksmith)";

        private readonly List<StockEntry<GenericItem>> _ores;
        private readonly List<StockEntry<Equipment>> _metalArmor;

        public BlacksmithShop()
        {
            _ores = new List<StockEntry<GenericItem>>
            {
                new StockEntry<GenericItem>(new GenericItem("Iron Ore", 5), 5, 50),
                new StockEntry<GenericItem>(new GenericItem("Copper Ore", 3), 3, 80),
                new StockEntry<GenericItem>(new GenericItem("Silver Ore", 15), 15, 20),
                new StockEntry<GenericItem>(new GenericItem("Gold Nugget", 60), 60, 5)
            };

            _metalArmor = new List<StockEntry<Equipment>>
            {
                new StockEntry<Equipment>(new Equipment("Iron Sword", EquipmentType.Weapon, 50, 75, str: 5), 75, 10),
                new StockEntry<Equipment>(new Equipment("Steel Axe", EquipmentType.Weapon, 60, 90, str: 7), 90, 7),
                new StockEntry<Equipment>(new Equipment("War Hammer", EquipmentType.Weapon, 65, 95, str: 8), 95, 6),
                new StockEntry<Equipment>(new Equipment("Battle Sword", EquipmentType.Weapon, 70, 110, str: 10), 110, 4),
                new StockEntry<Equipment>(new Equipment("Great Axe", EquipmentType.Weapon, 80, 130, str: 12), 130, 3),
                new StockEntry<Equipment>(new Equipment("Chainmail Armor", EquipmentType.Armor, 70, 100, str: 3, hp: 20), 100, 5),
                new StockEntry<Equipment>(new Equipment("Plate Helmet", EquipmentType.Armor, 40, 60, hp: 15), 60, 8),
                new StockEntry<Equipment>(new Equipment("Iron Breastplate", EquipmentType.Armor, 75, 115, str: 4, hp: 25), 115, 4),
                new StockEntry<Equipment>(new Equipment("Steel Plate Armor", EquipmentType.Armor, 90, 150, str: 6, hp: 40), 150, 2),
                new StockEntry<Equipment>(new Equipment("Iron Shield", EquipmentType.Accessory, 45, 70, str: 2, hp: 15), 70, 6),
                new StockEntry<Equipment>(new Equipment("Steel Gauntlets", EquipmentType.Accessory, 35, 55, str: 4), 55, 7),
                new StockEntry<Equipment>(new Equipment("Reinforced Boots", EquipmentType.Accessory, 30, 50, agi: 2, hp: 10), 50, 8),
                new StockEntry<Equipment>(new Equipment("Wooden Shield", EquipmentType.OffHand, 40, 55, hp: 10, armor: 3), 55, 8),
                new StockEntry<Equipment>(new Equipment("Iron Shield", EquipmentType.OffHand, 60, 85, str: 2, hp: 20, armor: 5), 85, 5),
                new StockEntry<Equipment>(new Equipment("Steel Shield", EquipmentType.OffHand, 75, 120, str: 3, hp: 30, armor: 8), 120, 3),
                new StockEntry<Equipment>(new Equipment("Tower Shield", EquipmentType.OffHand, 90, 180, str: 4, hp: 50, armor: 12), 180, 2),
                new StockEntry<Equipment>(new Equipment("Buckler", EquipmentType.OffHand, 35, 45, agi: 2, armor: 2), 45, 6),
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
                Console.WriteLine("\nBlacksmith: 1) Buy Ore  2) Sell Ore  3) Repair Equipment 4) Buy equipment 0) Leave");
                Console.Write("Choice: ");
                var input = Console.ReadLine() ?? string.Empty;
                if (input.Trim() == "0") return;
                if (input.Trim() == "1")
                {
                    BuyOre(party);
                }
                else if (input.Trim() == "2")
                {
                    SellOre(party);
                }
                else if (input.Trim() == "3")
                {
                    RepairAllEquipment(party);
                }
                else if (input.Trim() == "4")
                {
                    BuyEquipment(party, _metalArmor);
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                }
            }
        }

        private void BuyOre(List<Character> party)
        {
            BuyFromStock(party, _ores, "ores");
        }

        private void SellOre(List<Character> party)
        {
            SellToStock(party, _ores, "Blacksmith");
        }
    }
}
