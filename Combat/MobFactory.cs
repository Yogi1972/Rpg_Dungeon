using Night.Characters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rpg_Dungeon
{
    internal static class Mobs
    {
        private static readonly Random _rng = new Random();

        private static readonly List<(string Name, int BaseHP, int BaseStr, int BaseAgi, int BaseInt, int GoldMin, int GoldMax, int BaseAR)> _mobTemplates = new()
        {
            ("Goblin", 20, 5, 8, 2, 5, 15, 0),
            ("Orc", 40, 12, 6, 3, 10, 25, 5),
            ("Skeleton", 25, 7, 7, 4, 8, 20, 2),
            ("Zombie", 35, 10, 4, 2, 12, 22, 3),
            ("Spider", 18, 6, 12, 3, 7, 18, 1),
            ("Wolf", 28, 9, 11, 4, 9, 20, 2),
            ("Troll", 60, 15, 5, 3, 15, 35, 8),
            ("Bandit", 32, 10, 10, 5, 12, 28, 4),
            ("Dark Mage", 30, 6, 8, 15, 18, 32, 2),
            ("Wraith", 35, 8, 10, 12, 20, 35, 3),
            ("Ogre", 70, 18, 4, 2, 20, 40, 10),
            ("Dragon", 100, 20, 12, 15, 50, 100, 15)
        };

        public static Mob GetRandomMobForParty(List<Character> party, int targetLevel)
        {
            if (party == null || party.Count == 0)
            {
                return CreateMobFromTemplate(0, 1);
            }

            int partyAvgLevel = (int)party.Average(c => c.Level);
            int mobLevel = Math.Max(1, targetLevel > 0 ? targetLevel : partyAvgLevel);

            return CreateMobFromTemplate(_rng.Next(_mobTemplates.Count), mobLevel);
        }

        private static Mob CreateMobFromTemplate(int templateIndex, int level)
        {
            var template = _mobTemplates[templateIndex % _mobTemplates.Count];

            var lootTable = new List<(Item, double)>
            {
                (new GenericItem("Health Potion", 15), 0.3),
                (new GenericItem("Mana Potion", 20), 0.2),
                (new Equipment("Common Sword", EquipmentType.Weapon, 10, 25), 0.15),
                (new Equipment("Leather Armor", EquipmentType.Armor, 8, 20), 0.15)
            };

            var mob = new Mob(
                template.Name,
                template.BaseHP,
                template.BaseStr,
                template.BaseAgi,
                template.BaseInt,
                template.GoldMin,
                template.GoldMax,
                lootTable,
                1,
                template.BaseAR
            );

            return mob.ScaleToLevel(level);
        }
    }
}
