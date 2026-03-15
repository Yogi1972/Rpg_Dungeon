using Night.Items;
using Rpg_Dungeon;
using System;
using System.Collections.Generic;
using System.Text;

namespace Night.Characters
{
    #region Base Character Class

    internal abstract class Character
    {
        #region Properties

        public string Name { get; set; }
        public int Health { get; protected set; }
        public int MaxHealth { get; protected set; }
        public int Mana { get; protected set; }
        public int MaxMana { get; protected set; }
        public int Stamina { get; protected set; }
        public int MaxStamina { get; protected set; }
        public int Strength { get; protected set; }
        public int Agility { get; protected set; }
        public int Intelligence { get; protected set; }
        public int ArmorRating { get; protected set; }

        public bool IsAlive => Health > 0;

        public Inventory Inventory { get; }

        public Pet? Pet { get; set; }

        public int Level { get; private set; }

        public int Experience { get; private set; }

        public int ExperienceToNextLevel => Playerleveling.GetXPToNextLevel(Level);

        public SkillTree SkillTree { get; set; }

        public int ThreatLevel { get; set; }

        public string CurrentLocation { get; set; }

        #endregion

        #region Constructor

        protected Character(string name)
        {
            Name = name;
            Inventory = new Inventory();
            Level = 1;
            Experience = 0;
            ThreatLevel = 0;
            CurrentLocation = "Town";
            SkillTree = SkillTreeFactory.CreateSkillTreeForCharacter(this);
        }

        #endregion

        #region Stats and Progression

        public void ApplyRaceBonuses(Race race)
        {
            MaxHealth += race.HealthBonus;
            Health += race.HealthBonus;
            MaxMana += race.ManaBonus;
            Mana += race.ManaBonus;
            MaxStamina += race.StaminaBonus;
            Stamina += race.StaminaBonus;
            Strength += race.StrengthBonus;
            Agility += race.AgilityBonus;
            Intelligence += race.IntelligenceBonus;
            ArmorRating += race.ArmorBonus;
        }

        public void RestoreProgress(int level, int experience, int health, int maxHealth, int mana, int maxMana, int stamina, int maxStamina, int str, int agi, int intel, int ar = 0)
        {
            Level = Math.Max(1, level);
            Experience = Math.Max(0, experience);
            MaxHealth = Math.Max(1, maxHealth);
            Health = Math.Min(MaxHealth, health);
            MaxMana = Math.Max(0, maxMana);
            Mana = Math.Min(MaxMana, mana);
            MaxStamina = Math.Max(0, maxStamina);
            Stamina = Math.Min(MaxStamina, stamina);
            Strength = str;
            Agility = agi;
            Intelligence = intel;
            ArmorRating = Math.Max(0, ar);
        }

        #endregion

        #region Stat Calculations

        public int GetTotalStrength()
        {
            int total = Strength;
            if (Inventory.EquippedWeapon != null) total += Inventory.EquippedWeapon.StrengthBonus;
            if (Inventory.EquippedArmor != null) total += Inventory.EquippedArmor.StrengthBonus;
            if (Inventory.EquippedAccessory != null) total += Inventory.EquippedAccessory.StrengthBonus;
            if (Inventory.EquippedNecklace != null) total += Inventory.EquippedNecklace.StrengthBonus;
            if (Inventory.EquippedRing1 != null) total += Inventory.EquippedRing1.StrengthBonus;
            if (Inventory.EquippedRing2 != null) total += Inventory.EquippedRing2.StrengthBonus;
            if (Inventory.EquippedOffHand != null) total += Inventory.EquippedOffHand.StrengthBonus;
            if (SkillTree != null) total += SkillTree.GetTotalStrengthBonus();
            return total;
        }

        public int GetTotalAgility()
        {
            int total = Agility;
            if (Inventory.EquippedWeapon != null) total += Inventory.EquippedWeapon.AgilityBonus;
            if (Inventory.EquippedArmor != null) total += Inventory.EquippedArmor.AgilityBonus;
            if (Inventory.EquippedAccessory != null) total += Inventory.EquippedAccessory.AgilityBonus;
            if (Inventory.EquippedNecklace != null) total += Inventory.EquippedNecklace.AgilityBonus;
            if (Inventory.EquippedRing1 != null) total += Inventory.EquippedRing1.AgilityBonus;
            if (Inventory.EquippedRing2 != null) total += Inventory.EquippedRing2.AgilityBonus;
            if (Inventory.EquippedOffHand != null) total += Inventory.EquippedOffHand.AgilityBonus;
            if (SkillTree != null) total += SkillTree.GetTotalAgilityBonus();
            return total;
        }

        public int GetTotalIntelligence()
        {
            int total = Intelligence;
            if (Inventory.EquippedWeapon != null) total += Inventory.EquippedWeapon.IntelligenceBonus;
            if (Inventory.EquippedArmor != null) total += Inventory.EquippedArmor.IntelligenceBonus;
            if (Inventory.EquippedAccessory != null) total += Inventory.EquippedAccessory.IntelligenceBonus;
            if (Inventory.EquippedNecklace != null) total += Inventory.EquippedNecklace.IntelligenceBonus;
            if (Inventory.EquippedRing1 != null) total += Inventory.EquippedRing1.IntelligenceBonus;
            if (Inventory.EquippedRing2 != null) total += Inventory.EquippedRing2.IntelligenceBonus;
            if (Inventory.EquippedOffHand != null) total += Inventory.EquippedOffHand.IntelligenceBonus;
            if (SkillTree != null) total += SkillTree.GetTotalIntelligenceBonus();
            return total;
        }

        public int GetTotalMaxHP()
        {
            int total = MaxHealth;
            if (Inventory.EquippedWeapon != null) total += Inventory.EquippedWeapon.MaxHPBonus;
            if (Inventory.EquippedArmor != null) total += Inventory.EquippedArmor.MaxHPBonus;
            if (Inventory.EquippedAccessory != null) total += Inventory.EquippedAccessory.MaxHPBonus;
            if (Inventory.EquippedNecklace != null) total += Inventory.EquippedNecklace.MaxHPBonus;
            if (Inventory.EquippedRing1 != null) total += Inventory.EquippedRing1.MaxHPBonus;
            if (Inventory.EquippedRing2 != null) total += Inventory.EquippedRing2.MaxHPBonus;
            if (Inventory.EquippedOffHand != null) total += Inventory.EquippedOffHand.MaxHPBonus;
            if (SkillTree != null) total += SkillTree.GetTotalMaxHPBonus();
            return total;
        }

        public int GetTotalMaxMana()
        {
            int total = MaxMana;
            if (Inventory.EquippedWeapon != null) total += Inventory.EquippedWeapon.MaxManaBonus;
            if (Inventory.EquippedArmor != null) total += Inventory.EquippedArmor.MaxManaBonus;
            if (Inventory.EquippedAccessory != null) total += Inventory.EquippedAccessory.MaxManaBonus;
            if (Inventory.EquippedNecklace != null) total += Inventory.EquippedNecklace.MaxManaBonus;
            if (Inventory.EquippedRing1 != null) total += Inventory.EquippedRing1.MaxManaBonus;
            if (Inventory.EquippedRing2 != null) total += Inventory.EquippedRing2.MaxManaBonus;
            if (Inventory.EquippedOffHand != null) total += Inventory.EquippedOffHand.MaxManaBonus;
            if (SkillTree != null) total += SkillTree.GetTotalMaxManaBonus();
            return total;
        }

        public int GetTotalMaxStamina()
        {
            int total = MaxStamina;
            if (Inventory.EquippedWeapon != null) total += Inventory.EquippedWeapon.MaxStaminaBonus;
            if (Inventory.EquippedArmor != null) total += Inventory.EquippedArmor.MaxStaminaBonus;
            if (Inventory.EquippedAccessory != null) total += Inventory.EquippedAccessory.MaxStaminaBonus;
            if (Inventory.EquippedNecklace != null) total += Inventory.EquippedNecklace.MaxStaminaBonus;
            if (Inventory.EquippedRing1 != null) total += Inventory.EquippedRing1.MaxStaminaBonus;
            if (Inventory.EquippedRing2 != null) total += Inventory.EquippedRing2.MaxStaminaBonus;
            if (Inventory.EquippedOffHand != null) total += Inventory.EquippedOffHand.MaxStaminaBonus;
            if (SkillTree != null) total += SkillTree.GetTotalMaxStaminaBonus();
            return total;
        }

        public int GetTotalArmorRating()
        {
            int total = ArmorRating;
            if (Inventory.EquippedWeapon != null) total += Inventory.EquippedWeapon.ArmorBonus;
            if (Inventory.EquippedArmor != null) total += Inventory.EquippedArmor.ArmorBonus;
            if (Inventory.EquippedAccessory != null) total += Inventory.EquippedAccessory.ArmorBonus;
            if (Inventory.EquippedNecklace != null) total += Inventory.EquippedNecklace.ArmorBonus;
            if (Inventory.EquippedRing1 != null) total += Inventory.EquippedRing1.ArmorBonus;
            if (Inventory.EquippedRing2 != null) total += Inventory.EquippedRing2.ArmorBonus;
            if (Inventory.EquippedOffHand != null) total += Inventory.EquippedOffHand.ArmorBonus;
            if (SkillTree != null) total += SkillTree.GetTotalArmorBonus();
            return total;
        }

        #endregion

        #region Health, Mana, and Stamina Management

        public virtual void ReceiveDamage(int amount)
        {
            if (amount <= 0) return;

            int totalAR = GetTotalArmorRating();
            int reducedDamage = Math.Max(1, amount - totalAR);

            if (totalAR > 0 && reducedDamage < amount)
            {
                Console.Write($" [AR: {totalAR}, Reduced: {amount} → {reducedDamage}]");
            }

            Health = Math.Max(0, Health - reducedDamage);
        }

        public virtual void Heal(int amount)
        {
            if (amount <= 0) return;
            Health = Math.Min(MaxHealth, Health + amount);
        }

        public virtual void RestoreMana(int amount)
        {
            if (amount <= 0) return;
            Mana = Math.Min(MaxMana, Mana + amount);
        }

        public virtual void RestoreStamina(int amount)
        {
            if (amount <= 0) return;
            Stamina = Math.Min(MaxStamina, Stamina + amount);
        }

        public int GetEffectiveManaCost(int baseCost)
        {
            if (SkillTree == null) return baseCost;

            double efficiency = SkillTree.GetTotalManaEfficiencyPercent();
            if (efficiency > 0)
            {
                int reducedCost = (int)(baseCost * (1.0 - efficiency));
                return Math.Max(1, reducedCost);
            }

            return baseCost;
        }

        #endregion

        #region Combat

        public abstract void Attack(Character target);

        public abstract void SpecialAbility(Character target);

        #endregion

        #region Experience and Leveling

        public void GainExperience(int amount)
        {
            if (amount <= 0) return;
            if (Level >= Playerleveling.MaxLevel)
            {
                Console.WriteLine($"{Name} is already at max level ({Playerleveling.MaxLevel})!");
                return;
            }

            Experience += amount;
            Console.WriteLine($"{Name} gains {amount} XP ({Experience}/{ExperienceToNextLevel}).");

            while (Experience >= ExperienceToNextLevel && Level < Playerleveling.MaxLevel)
            {
                Experience -= ExperienceToNextLevel;
                LevelUp();
            }
        }

        private void LevelUp()
        {
            Level++;

            var gains = Playerleveling.GetStatGainsForLevel(this, Level);

            MaxHealth += gains.Health;
            Health = MaxHealth;
            MaxMana += gains.Mana;
            Mana = MaxMana;
            MaxStamina += gains.Stamina;
            Stamina = MaxStamina;
            Strength += gains.Strength;
            Agility += gains.Agility;
            Intelligence += gains.Intelligence;
            ArmorRating += gains.ArmorRating;

            string title = Playerleveling.GetLevelTitle(Level);
            Console.WriteLine($"\n*** {Name} leveled up to {Level} ({title})! ***");
            Console.WriteLine($"Stat gains: {gains}");
            Console.WriteLine($"Health, Mana, and Stamina fully restored!");

            if (SkillTree != null)
            {
                SkillTree.AddSkillPoint();
                Console.WriteLine($"✨ Earned 1 Skill Point! Total: {SkillTree.SkillPoints}");
            }

            if (Level % 25 == 0)
            {
                Console.WriteLine($"*** MILESTONE LEVEL {Level} REACHED! Bonus stats awarded! ***");
            }
            else if (Level == Playerleveling.MaxLevel)
            {
                Console.WriteLine($"*** {Name} has reached the MAXIMUM LEVEL! You are ASCENDED! ***");
            }
        }

        #endregion
    }

    #endregion
}
