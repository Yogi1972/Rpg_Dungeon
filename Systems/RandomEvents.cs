using Night.Characters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rpg_Dungeon
{
    #region Enums

    internal enum EventType
    {
        Combat,
        Treasure,
        Merchant,
        Mystery,
        Blessing,
        Curse
    }

    #endregion

    #region RandomEvent Class

    internal class RandomEvent
    {
        #region Properties

        public string Name { get; }
        public string Description { get; }
        public EventType Type { get; }
        public Action<List<Character>> Effect { get; }

        #endregion

        #region Constructor

        public RandomEvent(string name, string description, EventType type, Action<List<Character>> effect)
        {
            Name = name;
            Description = description;
            Type = type;
            Effect = effect;
        }

        #endregion

        #region Methods

        public void Trigger(List<Character> party)
        {
            Console.WriteLine($"\n✨ RANDOM EVENT: {Name}");
            Console.WriteLine(Description);
            Effect?.Invoke(party);
        }

        #endregion
    }

    #endregion

    #region RandomEventManager Class

    internal class RandomEventManager
    {
        #region Fields

        private readonly List<RandomEvent> _events;
        private readonly Random _random;

        #endregion

        #region Constructor

        public RandomEventManager()
        {
            _random = new Random();
            _events = new List<RandomEvent>();
            InitializeEvents();
        }

        #endregion

        #region Event Initialization

        private void InitializeEvents()
        {
            // Treasure Events
            _events.Add(new RandomEvent(
                "Hidden Cache",
                "You discover a hidden cache of treasure!",
                EventType.Treasure,
                party =>
                {
                    int gold = _random.Next(50, 200);
                    var leader = party.FirstOrDefault(p => p.IsAlive);
                    if (leader != null)
                    {
                        leader.Inventory.AddGold(gold);
                        Console.WriteLine($"💰 Found {gold} gold!");
                    }
                }
            ));

            _events.Add(new RandomEvent(
                "Chest in the Shadows",
                "A mysterious chest appears before you...",
                EventType.Treasure,
                party =>
                {
                    Console.Write("Open the chest? (y/n): ");
                    var choice = Console.ReadLine();
                    if (choice?.ToLower() == "y")
                    {
                        if (_random.Next(100) < 70)
                        {
                            int gold = _random.Next(100, 300);
                            var leader = party.FirstOrDefault(p => p.IsAlive);
                            if (leader != null)
                            {
                                leader.Inventory.AddGold(gold);
                                Console.WriteLine($"💰 The chest contained {gold} gold!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("💀 It was a trap! The chest was cursed!");
                            foreach (var member in party.Where(p => p.IsAlive))
                            {
                                member.ReceiveDamage(_random.Next(10, 30));
                            }
                        }
                    }
                }
            ));

            // Merchant Events
            _events.Add(new RandomEvent(
                "Traveling Merchant",
                "A traveling merchant offers you a special deal!",
                EventType.Merchant,
                party =>
                {
                    Console.WriteLine("'I have a Rare Healing Potion for only 30 gold!'");
                    Console.Write("Buy it? (y/n): ");
                    var choice = Console.ReadLine();
                    if (choice?.ToLower() == "y")
                    {
                        var leader = party.FirstOrDefault(p => p.IsAlive);
                        if (leader != null && leader.Inventory.SpendGold(30))
                        {
                            leader.Inventory.AddItem(new GenericItem("Rare Healing Potion", 60));
                            Console.WriteLine("✓ Purchased Rare Healing Potion!");
                        }
                        else
                        {
                            Console.WriteLine("Not enough gold!");
                        }
                    }
                }
            ));

            // Blessing Events
            _events.Add(new RandomEvent(
                "Shrine of Healing",
                "You find an ancient shrine emanating divine energy.",
                EventType.Blessing,
                party =>
                {
                    Console.WriteLine("The shrine's power washes over your party!");
                    foreach (var member in party.Where(p => p.IsAlive))
                    {
                        int healAmount = _random.Next(20, 50);
                        member.Heal(healAmount);
                        Console.WriteLine($"✨ {member.Name} healed for {healAmount} HP!");
                    }
                }
            ));

            _events.Add(new RandomEvent(
                "Blessing of Fortune",
                "A mysterious figure blesses you with good fortune!",
                EventType.Blessing,
                party =>
                {
                    var leader = party.FirstOrDefault(p => p.IsAlive);
                    if (leader != null)
                    {
                        int expGain = _random.Next(100, 300);
                        leader.GainExperience(expGain);
                        Console.WriteLine($"✨ {leader.Name} gained {expGain} experience!");
                    }
                }
            ));

            // Mystery Events
            _events.Add(new RandomEvent(
                "Mysterious Stranger",
                "A hooded figure approaches...",
                EventType.Mystery,
                party =>
                {
                    Console.WriteLine("'I can make you stronger... for a price.'");
                    Console.Write("Accept? (y/n): ");
                    var choice = Console.ReadLine();
                    if (choice?.ToLower() == "y")
                    {
                        var leader = party.FirstOrDefault(p => p.IsAlive);
                        if (leader != null && leader.Inventory.SpendGold(100))
                        {
                            leader.GainExperience(500);
                            Console.WriteLine($"✨ {leader.Name} gained 500 experience!");
                        }
                        else
                        {
                            Console.WriteLine("'Come back when you have 100 gold.'");
                        }
                    }
                }
            ));

            _events.Add(new RandomEvent(
                "Ancient Tome",
                "You find an ancient tome filled with knowledge.",
                EventType.Mystery,
                party =>
                {
                    var livingMembers = party.Where(p => p.IsAlive).ToList();
                    if (livingMembers.Count > 0)
                    {
                        var reader = livingMembers[_random.Next(livingMembers.Count)];
                        int expGain = _random.Next(150, 400);
                        reader.GainExperience(expGain);
                        Console.WriteLine($"📖 {reader.Name} learned from the tome and gained {expGain} experience!");
                    }
                }
            ));

            // Curse Events
            _events.Add(new RandomEvent(
                "Cursed Ground",
                "You step on cursed ground!",
                EventType.Curse,
                party =>
                {
                    foreach (var member in party.Where(p => p.IsAlive))
                    {
                        int damage = _random.Next(5, 20);
                        member.ReceiveDamage(damage);
                        Console.WriteLine($"💀 {member.Name} took {damage} curse damage!");
                    }
                }
            ));

            // Ambush Event
            _events.Add(new RandomEvent(
                "Ambush!",
                "Enemies leap out from hiding!",
                EventType.Combat,
                party =>
                {
                    Console.WriteLine("⚔️ A group of bandits attack!");
                    Console.WriteLine("(This would trigger combat in the full game)");
                    // In actual implementation, this would start combat
                }
            ));

            // Resting Spot
            _events.Add(new RandomEvent(
                "Safe Haven",
                "You find a peaceful clearing.",
                EventType.Blessing,
                party =>
                {
                    Console.WriteLine("Your party takes a moment to rest.");
                    foreach (var member in party.Where(p => p.IsAlive))
                    {
                        int healAmount = member.MaxHealth / 4;
                        int manaAmount = member.MaxMana / 4;
                        member.Heal(healAmount);
                        member.RestoreMana(manaAmount);
                        Console.WriteLine($"✨ {member.Name} recovered {healAmount} HP and {manaAmount} Mana!");
                    }
                }
            ));

            // Gambling opportunity
            _events.Add(new RandomEvent(
                "Game of Chance",
                "A gambler challenges you to a dice game!",
                EventType.Mystery,
                party =>
                {
                    Console.Write("Bet 50 gold on a dice roll? (y/n): ");
                    var choice = Console.ReadLine();
                    if (choice?.ToLower() == "y")
                    {
                        var leader = party.FirstOrDefault(p => p.IsAlive);
                        if (leader != null && leader.Inventory.SpendGold(50))
                        {
                            if (_random.Next(100) < 50)
                            {
                                leader.Inventory.AddGold(150);
                                Console.WriteLine("🎲 You win! Gained 150 gold!");
                            }
                            else
                            {
                                Console.WriteLine("🎲 You lose! Lost 50 gold!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Not enough gold!");
                        }
                    }
                }
            ));

            // Equipment drop
            _events.Add(new RandomEvent(
                "Abandoned Gear",
                "You find equipment left behind by a fallen adventurer.",
                EventType.Treasure,
                party =>
                {
                    var equipmentTypes = new[] { EquipmentType.Weapon, EquipmentType.Armor, EquipmentType.Accessory };
                    var randomType = equipmentTypes[_random.Next(equipmentTypes.Length)];
                    var equipment = new Equipment($"Found {randomType}", randomType, _random.Next(30, 60), _random.Next(40, 100));
                    
                    var leader = party.FirstOrDefault(p => p.IsAlive);
                    if (leader != null)
                    {
                        if (leader.Inventory.AddItem(equipment))
                        {
                            Console.WriteLine($"⚔️ Found {equipment.Name}!");
                        }
                        else
                        {
                            Console.WriteLine("Inventory full! Item left behind.");
                        }
                    }
                }
            ));
        }

        #endregion

        #region Event Triggering

        public void TriggerRandomEvent(List<Character> party, int chancePercent = 25)
        {
            if (_random.Next(100) < chancePercent)
            {
                var randomEvent = _events[_random.Next(_events.Count)];
                randomEvent.Trigger(party);
            }
        }

        public RandomEvent? GetRandomEvent()
        {
            return _events[_random.Next(_events.Count)];
        }

        public void TriggerSpecificEvent(string eventName, List<Character> party)
        {
            var evt = _events.FirstOrDefault(e => e.Name.Equals(eventName, StringComparison.OrdinalIgnoreCase));
            evt?.Trigger(party);
        }

        #endregion
    }

    #endregion
}
