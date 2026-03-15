using Night.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rpg_Dungeon
{
    internal static class Combat
    {
        #region Fields

        private static readonly Random _rng = new Random();
        // boolean to see if specialability was used last turn, to prevent spamming
        public static Dictionary<string, bool> SpecialUsedLastTurn = new Dictionary<string, bool>();

        #endregion

        #region Utility Methods

        // Roll a d20 (1-20)
        public static int RollD20()
        {
            return _rng.Next(1, 21);
        }

        #endregion

        #region Attack Methods

        // Character attacks a mob using a d20 roll.
        public static void Attack(Character attacker, Mob target)
        {
            if (attacker == null || target == null) return;
            if (!attacker.IsAlive) return;

            int roll = RollD20();
            int attackStat = GetAttackStat(attacker);
            int targetDefense = 10 + (target.Agility / 2);

            Console.Write($"{attacker.Name} rolls d20: {roll} (+{attackStat} attack)");

            if (roll == 1)
            {
                Console.Write("Critical miss!");
                return;
            }

            int total = roll + attackStat;
            bool isCrit = roll == 20;
            if (isCrit || total > targetDefense)
            {
                int baseDamage = attackStat;
                if (attacker is Mage) baseDamage = attacker.Intelligence;
                else if (attacker is Rogue) baseDamage = attacker.Agility;
                else if (attacker is Priest) baseDamage = Math.Max(1, attacker.Intelligence / 2);

                int damage = isCrit ? baseDamage * 2 : baseDamage;
                Console.Write($"Hit! {attacker.Name} deals {damage} damage to {target.Name}.");
                // We don't have mutable mob HP on Mob class; simulate by reporting and relying on external flow
                // For simple flow, we will reduce a temporary HP container if used by RunEncounter.
                // Here just output.
            }
            else
            {
                Console.Write($"{attacker.Name} misses {target.Name}.");
            }
        }

        // Mob attacks a character using d20
        public static void Attack(Mob attacker, Character target)
        {
            if (attacker == null || target == null) return;
            if (!target.IsAlive) return;

            int roll = RollD20();
            int attackStat = attacker.Strength;
            int targetDefense = 10 + (target.GetTotalAgility() / 2);

            Console.Write($"{attacker.Name} rolls d20: {roll} (+{attackStat} attack)");

            if (roll == 1)
            {
                Console.Write("Mob critically misses!");
                return;
            }

            int total = roll + attackStat;
            bool isCrit = roll == 20;
            if (isCrit || total > targetDefense)
            {
                // Check for dodge
                if (target.SkillTree != null)
                {
                    int dodgeChance = target.SkillTree.GetTotalDodgeChanceBonus();
                    if (dodgeChance > 0 && _rng.Next(1, 101) <= dodgeChance)
                    {
                        Console.Write($"{target.Name} dodges the attack! [DODGE]");
                        return;
                    }
                }

                int damage = isCrit ? attacker.Strength * 2 : attacker.Strength;

                // Apply defense reduction from skills
                if (target.SkillTree != null)
                {
                    int defenseBonus = target.SkillTree.GetTotalDefenseBonus();
                    if (defenseBonus > 0)
                    {
                        int reduction = Math.Min(damage - 1, defenseBonus); // Defense can't reduce below 1
                        damage -= reduction;
                        Console.Write($" [Defense: -{reduction}]");
                    }
                }

                Console.Write($"{attacker.Name} hits! Deals {damage} damage to {target.Name}.");
                target.ReceiveDamage(damage);

                // Damage equipment when character takes damage (1-3 durability loss per hit)
                int durabilityLoss = _rng.Next(1, 4);
                target.Inventory.DamageEquipment(durabilityLoss);
            }
            else
            {
                Console.Write($"{attacker.Name} misses {target.Name}.");
            }
        }

        #endregion

        #region Combat Encounter

        // Run a simple encounter between a party and a single mob. Returns true if party wins.
        public static bool RunEncounter(List<Character> party, Mob mob)
        {
            if (party == null || party.Count == 0 || mob == null) return false;

            int mobHp = mob.Health;

            // Calculate party average level safely
            int totalLevel = 0;
            int aliveCount = 0;
            foreach (var p in party)
            {
                if (p.IsAlive)
                {
                    totalLevel += p.Level;
                    aliveCount++;
                }
            }

            int partyAvgLevel = aliveCount > 0 ? totalLevel / aliveCount : 1;
            string mobRank = Playerleveling.GetMobRankTitle(mob.Level, partyAvgLevel);
            Console.Write($"Encounter begins! A Level {mob.Level} {mob.Name} {mobRank} appears (HP {mobHp})");

            // Reset threat levels at the start of combat
            foreach (var p in party)
            {
                p.ThreatLevel = 0;
                if (p is Warrior warrior)
                {
                    warrior.IsTaunting = false;
                    warrior.TauntDuration = 0;
                }
            }

            while (mobHp > 0 && party.Any(p => p.IsAlive))
            {
                // Party turn
                foreach (var member in party.Where(p => p.IsAlive))
                {
                    if (mobHp <= 0) break;

                    // Process status effects at start of turn
                    StatusEffectManager.ProcessEffects(member);

                    // Check if stunned - skip turn
                    if (StatusEffectManager.IsStunned(member))
                    {
                        Console.WriteLine($"\n{member.Name} is stunned and skips this turn!");
                        continue;
                    }

                    // Prompt player for action
                    Console.Write($"\n{member.Name}'s turn (Lv {member.Level}). HP={member.Health}");

                    // Display Mana or Stamina based on class
                    if (member is Warrior || member is Rogue)
                        Console.Write($", Stamina={member.Stamina}");
                    else if (member is Mage || member is Priest)
                        Console.Write($", Mana={member.Mana}");

                    // Display threat level and taunt status
                    Console.Write($", Threat={member.ThreatLevel}");
                    if (member is Warrior w && w.IsTaunting)
                    {
                        Console.Write($" [TAUNTING:{w.TauntDuration}]");
                    }

                    // Display current stance
                    Console.Write($" {CombatStanceModifiers.GetStanceIcon(member.CurrentStance)}");

                    // Display active status effects
                    var activeEffects = StatusEffectManager.GetActiveEffects(member);
                    if (activeEffects.Any())
                    {
                        Console.Write(" [");
                        foreach (var effect in activeEffects)
                        {
                            Console.Write($"{effect.GetIcon()}");
                        }
                        Console.Write("]");
                    }

                    Console.Write($"\n{mob.Name} (Lv {mob.Level}): {mobHp}/{mob.Health} HP");
                    Console.Write($"\nChoose action: ");
                    Console.Write($"\n1. Attack ");
                    Console.Write($"\n2. Special ");
                    if (member is Warrior warrior)
                    {
                        Console.Write($"\n3. Taunt (Draw aggro) ");
                        Console.Write($"\n4. Change Stance ({member.CurrentStance}) ");
                        Console.Write($"\n5. Use item ");
                        Console.Write($"\n6. Pass ");
                    }
                    else
                    {
                        Console.Write($"\n3. Change Stance ({member.CurrentStance}) ");
                        Console.Write($"\n4. Use item ");
                        Console.Write($"\n5. Pass ");
                    }
                    Console.Write("Action: ");

                    // This is to get the users input for their action, in a real game this would be more robust with error handling and possibly a UI instead of console.
                    String act = Console.ReadLine() ?? string.Empty;
                    act = act.Trim();

                    if (act == "1")
                    {
                        // Setting special used last turn to false at start of turn to allow using special again after using item or attacking
                        SpecialUsedLastTurn[member.Name] = false;

                        // Attack
                        int roll = RollD20();
                        int attackStat = GetAttackStat(member);
                        int targetDefense = 10 + (mob.Agility / 2);
                        Console.Write($"{member.Name} rolls d20: {roll} (+{attackStat}) against defense {targetDefense}");
                        if (roll == 1)
                        {
                            Console.Write($"{member.Name} critically misses!");// lol this just checking to see if your character sucks or not :)
                            continue;
                        }

                        int total = roll + attackStat; // getting total of roll and attack stat to compare against defense, crits will ignore defense but still get the bonus from attack stat
                        bool crit = roll == 20;// natural 20 always crits

                        // Check for additional crit chance from skills
                        if (!crit && member.SkillTree != null)
                        {
                            int critChance = member.SkillTree.GetTotalCritChanceBonus();
                            if (critChance > 0 && _rng.Next(1, 101) <= critChance)
                            {
                                crit = true;
                                Console.Write(" [SKILL CRIT!]");
                            }
                        }

                        if (crit || total > targetDefense)
                        {
                            int baseDamage = attackStat;
                            if (member is Mage) baseDamage = member.GetTotalIntelligence();
                            else if (member is Rogue) baseDamage = member.GetTotalAgility();
                            else if (member is Priest) baseDamage = Math.Max(1, member.GetTotalIntelligence() / 2);
                            else if (member is Warrior) baseDamage = member.GetTotalStrength();

                            int dmg = crit ? baseDamage * 2 : baseDamage;

                            // Apply combat stance damage modifier
                            double stanceMultiplier = CombatStanceModifiers.GetDamageMultiplier(member.CurrentStance);
                            if (stanceMultiplier != 1.0)
                            {
                                int stanceDmg = (int)(dmg * stanceMultiplier);
                                if (stanceDmg != dmg)
                                {
                                    Console.Write($" [{CombatStanceModifiers.GetStanceIcon(member.CurrentStance)} Stance: {dmg} → {stanceDmg}]");
                                    dmg = stanceDmg;
                                }
                            }

                            // Apply status effect damage modifiers
                            double statusMultiplier = StatusEffectManager.GetDamageModifier(member);
                            if (statusMultiplier != 1.0)
                            {
                                int statusDmg = (int)(dmg * statusMultiplier);
                                if (statusDmg != dmg)
                                {
                                    Console.Write($" [Weakened: {dmg} → {statusDmg}]");
                                    dmg = statusDmg;
                                }
                            }

                            // Apply skill damage bonus
                            if (member.SkillTree != null)
                            {
                                dmg += member.SkillTree.GetTotalDamageBonus();
                            }

                            // Apply pet damage boost if present
                            if (member.Pet != null && member.Pet.Ability == PetAbility.DamageBoost)
                            {
                                dmg = (int)(dmg * 1.05); // +5% damage
                            }

                            // Apply life steal if character has the bonus
                            if (member.SkillTree != null)
                            {
                                double lifeStealPercent = member.SkillTree.GetTotalLifeStealPercent();
                                if (lifeStealPercent > 0)
                                {
                                    int healAmount = (int)(dmg * lifeStealPercent);
                                    if (healAmount > 0)
                                    {
                                        member.Heal(healAmount);
                                        Console.Write($" [Life Steal: +{healAmount} HP]");
                                    }
                                }
                            }

                            // Apply mob armor rating reduction
                            int finalDamage = dmg;
                            if (mob.ArmorRating > 0)
                            {
                                finalDamage = Math.Max(1, dmg - mob.ArmorRating); // Minimum 1 damage
                                if (finalDamage < dmg)
                                {
                                    Console.Write($" [Mob AR: {mob.ArmorRating}, {dmg} → {finalDamage}]");
                                }
                            }

                            mobHp = Math.Max(0, mobHp - finalDamage);
                            Console.Write($"{member.Name} hits {mob.Name} for {finalDamage} damage (mob HP now {mobHp}).");

                            // Generate threat based on damage dealt
                            int threat = finalDamage;
                            if (member is Warrior) threat = (int)(threat * 1.5); // Warriors generate 50% more threat
                            member.ThreatLevel += threat;
                        }
                        else
                        {
                            Console.Write($"{member.Name} misses {mob.Name}.");
                        }
                    }
                    else if (act == "2")
                    {
                        // Special ability
                        UseSpecialAgainstMob(member, mob, ref mobHp, party);
                    }
                    else if (member is Warrior && act == "3")
                    {
                        // Warrior Taunt ability
                        SpecialUsedLastTurn[member.Name] = false;
                        ((Warrior)member).Taunt();
                    }
                    else if (member is Warrior && act == "4")
                    {
                        // Change stance (Warrior)
                        ChangeStanceMenu(member);
                    }
                    else if (member is Warrior && act == "5")
                    {
                        // Use item (Warrior)
                        SpecialUsedLastTurn[member.Name] = false;
                        UseItemDuringCombat(member, party, mob, ref mobHp);
                    }
                    else if (!(member is Warrior) && act == "3")
                    {
                        // Change stance (non-Warrior)
                        ChangeStanceMenu(member);
                    }
                    else if (!(member is Warrior) && act == "4")
                    {
                        // Use item (non-Warrior)
                        SpecialUsedLastTurn[member.Name] = false;
                        UseItemDuringCombat(member, party, mob, ref mobHp);
                    }
                    else
                    {
                        Console.Write($"{member.Name} passes.");
                    }
                }

                if (mobHp <= 0) break;

                // Decrement taunt duration for warriors
                foreach (var member in party.Where(p => p is Warrior))
                {
                    ((Warrior)member).DecrementTaunt();
                }

                // Mob turn: target character with highest threat
                var alive = party.Where(p => p.IsAlive).ToList();
                if (alive.Count == 0) break;

                // Find target based on threat system
                Character target;
                var taunters = alive.Where(p => p is Warrior warrior && warrior.IsTaunting).ToList();
                if (taunters.Any())
                {
                    // If anyone is taunting, enemy MUST attack them (choose highest threat taunt if multiple)
                    target = taunters.OrderByDescending(p => p.ThreatLevel).First();
                    Console.Write($"\n🎯 {target.Name}'s taunt forces {mob.Name} to attack!");
                }
                else
                {
                    // Otherwise, target highest threat character
                    target = alive.OrderByDescending(p => p.ThreatLevel).First();
                    if (target.ThreatLevel > 0)
                    {
                        Console.Write($"\n🎯 {mob.Name} targets {target.Name} (Threat: {target.ThreatLevel})!");
                    }
                }
                Attack(mob, target);
            }

            bool partyWon = mobHp <= 0;
            if (partyWon)
            {
                Console.Write($"{mob.Name} was defeated!");
                var loot = mob.DropLoot(_rng);

                // Award experience to alive members using improved calculation
                int xp = Playerleveling.CalculateXPReward(mob, party.Count);

                foreach (var member in party.Where(p => p.IsAlive))
                {
                    int memberXP = xp;

                    // Apply pet XP boost
                    if (member.Pet != null && member.Pet.Ability == PetAbility.ExperienceBoost)
                    {
                        memberXP = (int)(memberXP * 1.10); // +10% XP
                    }

                    // Apply skill XP bonus
                    if (member.SkillTree != null)
                    {
                        double xpBonusPercent = member.SkillTree.GetTotalXPBonusPercent();
                        if (xpBonusPercent > 0)
                        {
                            memberXP = (int)(memberXP * (1.0 + xpBonusPercent));
                        }
                    }

                    Console.WriteLine($"{member.Name} gains {memberXP} XP!");
                    member.GainExperience(memberXP);

                    // Pet gains experience too
                    if (member.Pet != null)
                    {
                        member.Pet.GainExperience(xp / 4); // Pets gain 25% of character XP
                    }

                    // Apply pet post-combat effects
                    if (member.Pet != null)
                    {
                        if (member.Pet.Ability == PetAbility.HealthRegen)
                        {
                            int healAmount = (int)(member.MaxHealth * 0.05);
                            member.Heal(healAmount);
                            Console.WriteLine($"🐾 {member.Pet.Name} helps {member.Name} recover {healAmount} HP!");
                        }
                        else if (member.Pet.Ability == PetAbility.ManaRegen)
                        {
                            // Restore mana for mana users, stamina for stamina users
                            if (member is Warrior || member is Rogue)
                            {
                                int staminaAmount = (int)(member.MaxStamina * 0.05);
                                member.RestoreStamina(staminaAmount);
                                Console.WriteLine($"🐾 {member.Pet.Name} helps {member.Name} recover {staminaAmount} Stamina!");
                            }
                            else if (member is Mage || member is Priest)
                            {
                                int manaAmount = (int)(member.MaxMana * 0.05);
                                member.RestoreMana(manaAmount);
                                Console.WriteLine($"🐾 {member.Pet.Name} helps {member.Name} recover {manaAmount} Mana!");
                            }
                        }
                    }
                }

                if (loot.Gold > 0)
                {
                    int totalGoldAmount = loot.Gold;

                    // Calculate bonuses from first alive member (party leader for bonuses)
                    var bonusReceiver = party.FirstOrDefault(p => p.IsAlive) ?? party[0];

                    // Apply pet loot bonus
                    if (bonusReceiver.Pet != null && bonusReceiver.Pet.Ability == PetAbility.LootBonus)
                    {
                        totalGoldAmount = (int)(totalGoldAmount * 1.20); // +20% gold
                    }

                    // Apply skill gold bonus
                    if (bonusReceiver.SkillTree != null)
                    {
                        double goldBonusPercent = bonusReceiver.SkillTree.GetTotalGoldBonusPercent();
                        if (goldBonusPercent > 0)
                        {
                            totalGoldAmount = (int)(totalGoldAmount * (1.0 + goldBonusPercent));
                        }
                    }

                    // Split gold evenly among all party members
                    int goldPerMember = totalGoldAmount / party.Count;
                    int remainder = totalGoldAmount % party.Count;

                    foreach (var member in party)
                    {
                        int memberGold = goldPerMember;
                        // Give remainder to first member
                        if (member == party[0] && remainder > 0)
                        {
                            memberGold += remainder;
                        }
                        member.Inventory.AddGold(memberGold);
                    }

                    Console.Write($"The party receives {totalGoldAmount} gold ({goldPerMember} gold per member).");
                }

                // Distribute items to party members in round-robin fashion
                int itemIndex = 0;
                foreach (var item in loot.Items)
                {
                    // Try to give item to party members in order, cycling through
                    bool itemAdded = false;
                    for (int attempt = 0; attempt < party.Count && !itemAdded; attempt++)
                    {
                        var receiver = party[(itemIndex + attempt) % party.Count];
                        if (receiver.Inventory.AddItem(item))
                        {
                            Console.Write($"{receiver.Name} finds {item.Name}.");
                            itemAdded = true;
                        }
                    }

                    if (!itemAdded)
                    {
                        Console.Write($"No space to pick up {item.Name}; it was left behind.");
                    }

                    itemIndex++;
                }
            }
            else
            {
                Console.Write("The party was defeated...");
            }

            // Clear all status effects after combat
            StatusEffectManager.ClearAllCombatEffects();

            return partyWon;
        }

        #endregion

        #region Special Ability Methods

        private static void UseSpecialAgainstMob(Character member, Mob mob, ref int mobHp, List<Character> party)
        {
            // Special abilities have unique effects based on class, but for simplicity we'll treat them as stronger attacks with some flavor text.
            Console.Write($"{member.Name} uses their special ability!");
            int roll = RollD20();
            // Priest special acts as a heal on allies
            if (member is Priest priest)
            {
                if (SpecialUsedLastTurn.TryGetValue(member.Name, out bool used) && used)
                {
                    Console.WriteLine($"{member.Name} cannot use special again so soon!");
                    return;
                }

                Console.WriteLine("\nChoose healing action:");
                Console.WriteLine("1) Heal single ally (12 Mana)");
                Console.WriteLine("2) Mass Heal all party members (25 Mana)");
                var healChoice = Console.ReadLine() ?? string.Empty;

                if (healChoice == "1")
                {
                    Console.WriteLine("Choose ally to heal:");
                    for (int i = 0; i < party.Count; i++) Console.WriteLine($"{i + 1}) {party[i].Name} - HP {party[i].Health}/{party[i].MaxHealth}");
                    var sel = Console.ReadLine() ?? string.Empty;
                    if (!int.TryParse(sel, out var idx) || idx < 1 || idx > party.Count) { Console.WriteLine("Invalid selection."); return; }
                    var target = party[idx - 1];
                    priest.HealAlly(target);
                    // Healing generates threat
                    priest.ThreatLevel += 10;
                }
                else if (healChoice == "2")
                {
                    priest.SpecialAbility(party);
                    // Mass heal generates more threat
                    priest.ThreatLevel += 25;
                }
                else
                {
                    Console.WriteLine("Invalid selection.");
                    return;
                }

                SpecialUsedLastTurn[member.Name] = true;
                return;
            }

            int targetDefense = -10 + (mobHp / 5); // Arbitrary difficulty for special ability
            int attackStat = GetAttackStat(member) + 5; // Special ability gets a bonus

            Console.WriteLine($"{member.Name} rolls d20: {roll} (+{attackStat}) against defense {targetDefense}");
            if (SpecialUsedLastTurn.TryGetValue(member.Name, out bool used2) && used2)
            {
                Console.WriteLine($"{member.Name} cannot use special again so soon!");
                return;
            }
            if (roll == 1)
            {
                Console.WriteLine($"{member.Name}'s special ability critically fails!");
            }

            int total = roll + attackStat;
            bool crit = roll == 20; // natural 20 always crits

            // Check for additional crit chance from skills
            if (!crit && member.SkillTree != null)
            {
                int critChance = member.SkillTree.GetTotalCritChanceBonus();
                if (critChance > 0 && _rng.Next(1, 101) <= critChance)
                {
                    crit = true;
                    Console.WriteLine(" [SKILL CRIT!]");
                }
            }

            if (crit || total > targetDefense)
            {
                int baseDamage = attackStat * 2; // Special ability does more damage
                if (member is Mage) baseDamage = member.GetTotalIntelligence() * 3;
                else if (member is Rogue) baseDamage = member.GetTotalAgility() * 3;
                else if (member is Warrior) baseDamage = member.GetTotalStrength() * 3;

                int dmg = crit ? baseDamage * 2 : baseDamage;

                // Apply skill damage bonus
                if (member.SkillTree != null)
                {
                    dmg += member.SkillTree.GetTotalDamageBonus();
                }

                // Apply life steal if character has the bonus
                if (member.SkillTree != null)
                {
                    double lifeStealPercent = member.SkillTree.GetTotalLifeStealPercent();
                    if (lifeStealPercent > 0)
                    {
                        int healAmount = (int)(dmg * lifeStealPercent);
                        if (healAmount > 0)
                        {
                            member.Heal(healAmount);
                            Console.WriteLine($" [Life Steal: +{healAmount} HP]");
                        }
                    }
                }

                // Apply mob armor rating reduction
                int finalDamage = dmg;
                if (mob.ArmorRating > 0)
                {
                    finalDamage = Math.Max(1, dmg - mob.ArmorRating); // Minimum 1 damage
                    if (finalDamage < dmg)
                    {
                        Console.WriteLine($" [Mob AR: {mob.ArmorRating}, {dmg} → {finalDamage}]");
                    }
                }

                mobHp = Math.Max(0, mobHp - finalDamage);
                Console.WriteLine($"{member.Name}'s special hits {mob.Name} for {finalDamage} damage (mob HP now {mobHp}).");

                // Generate threat based on damage dealt
                int threat = finalDamage;
                if (member is Warrior) threat = (int)(threat * 1.5); // Warriors generate 50% more threat
                member.ThreatLevel += threat;
            }
            else
            {
                Console.WriteLine($"{member.Name}'s special misses {mob.Name}.");
            }

            //set boolean to true for this member in dictionary to prevent spamming next turn
            SpecialUsedLastTurn[member.Name] = true;
        }

        #endregion

        #region Item Usage Methods

        private static void UseItemDuringCombat(Character member, List<Character> party, Mob mob, ref int mobHp)
        {
            Console.WriteLine($"\n{member.Name} tries to use an item during combat.");

            var slots = member.Inventory.Slots;
            if (slots.All(s => s == null))
            {
                Console.WriteLine("Inventory is empty!");
                return;
            }

            Console.WriteLine("Select slot number to use (0 to cancel):");
            for (int i = 0; i < slots.Count; i++)
            {
                var item = slots[i];
                if (item == null)
                {
                    Console.WriteLine($"{i + 1}) (empty)");
                }
                else
                {
                    Console.WriteLine($"{i + 1}) {item.Name}");
                }
            }

            Console.Write("Choice: ");
            var s = Console.ReadLine() ?? string.Empty;
            if (!int.TryParse(s, out var slotIdx) || slotIdx < 0 || slotIdx > slots.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }
            if (slotIdx == 0)
            {
                Console.WriteLine("Cancelled.");
                return;
            }

            var selectedItem = slots[slotIdx - 1];
            if (selectedItem == null)
            {
                Console.WriteLine("No item in that slot.");
                return;
            }

            // Check if item is usable in combat based on name
            var itemName = selectedItem.Name.ToLowerInvariant();

            // Healing items (potions, poultices, herbs, salves, tonics, etc.)
            if (itemName.Contains("healing") || itemName.Contains("potion of healing") || 
                itemName.Contains("poultice") || itemName.Contains("healing herb") || 
                itemName.Contains("restorative") || itemName.Contains("salve") || 
                itemName.Contains("tonic") || itemName.Contains("bandage"))
            {
                Console.WriteLine("Choose target to heal:");
                for (int i = 0; i < party.Count; i++)
                {
                    var status = party[i].IsAlive ? $"HP {party[i].Health}" : "(Down)";
                    Console.WriteLine($"{i + 1}) {party[i].Name} - {status}");
                }

                Console.Write("Target: ");
                var targetInput = Console.ReadLine() ?? string.Empty;
                if (!int.TryParse(targetInput, out var targetIdx) || targetIdx < 1 || targetIdx > party.Count)
                {
                    Console.WriteLine("Invalid target.");
                    return;
                }

                var target = party[targetIdx - 1];
                if (!target.IsAlive)
                {
                    Console.WriteLine($"{target.Name} is down and cannot be healed with this item.");
                    return;
                }

                // Base heal amount, scales with user's intelligence
                int healAmount = 30 + (member.Intelligence / 2);
                int oldHealth = target.Health;
                target.Heal(healAmount);
                int actualHealed = target.Health - oldHealth;
                member.Inventory.RemoveItem(slotIdx - 1);
                Console.WriteLine($"{member.Name} uses {selectedItem.Name} on {target.Name}, healing {actualHealed} HP! ({target.Health}/{target.MaxHealth})");
                return;
            }

            // Mana restoration items (mana potions, crystals, etc.)
            if (itemName.Contains("mana") || itemName.Contains("potion of mana") || 
                itemName.Contains("mana crystal") || itemName.Contains("arcane dust"))
            {
                int restore = 30 + (member.Intelligence / 2);
                int oldMana = member.Mana;
                member.RestoreMana(restore);
                int actualRestored = member.Mana - oldMana;
                member.Inventory.RemoveItem(slotIdx - 1);
                Console.WriteLine($"{member.Name} uses {selectedItem.Name}, restoring {actualRestored} Mana! ({member.Mana}/{member.MaxMana})");
                return;
            }

            // Stamina restoration items (stamina potions, etc.)
            if (itemName.Contains("stamina") || itemName.Contains("potion of stamina") ||
                itemName.Contains("endurance"))
            {
                int restore = 30 + (member.Strength / 2);
                int oldStamina = member.Stamina;
                member.RestoreStamina(restore);
                int actualRestored = member.Stamina - oldStamina;
                member.Inventory.RemoveItem(slotIdx - 1);
                Console.WriteLine($"{member.Name} uses {selectedItem.Name}, restoring {actualRestored} Stamina! ({member.Stamina}/{member.MaxStamina})");
                return;
            }

            // Antidotes and cleansing items
            if (itemName.Contains("antidote") || itemName.Contains("cleansing") || 
                itemName.Contains("purifying") || itemName.Contains("blessed water"))
            {
                Console.WriteLine($"{member.Name} uses {selectedItem.Name}. (Status effect removal not yet implemented)");
                member.Inventory.RemoveItem(slotIdx - 1);
                return;
            }

            // Offensive consumables (bombs, acids, poisons)
            if (itemName.Contains("bomb") || itemName.Contains("smoke bomb") || 
                itemName.Contains("vial of acid") || itemName.Contains("poison vial") || 
                itemName.Contains("oil flask"))
            {
                int roll = RollD20();
                int attackBonus = member.Agility / 2;
                int targetDefense = 10 + (mob.Agility / 2);

                Console.WriteLine($"{member.Name} throws {selectedItem.Name}! Roll: {roll} (+{attackBonus}) vs Defense {targetDefense}");

                if (roll == 1)
                {
                    Console.WriteLine("Critical failure! The item is wasted.");
                    member.Inventory.RemoveItem(slotIdx - 1);
                    return;
                }

                int total = roll + attackBonus;
                if (roll == 20 || total > targetDefense)
                {
                    int damage = 15 + (member.Intelligence / 2);
                    if (roll == 20) damage *= 2;
                    mobHp = Math.Max(0, mobHp - damage);
                    Console.WriteLine($"Hit! {selectedItem.Name} deals {damage} damage to {mob.Name}! (Mob HP now {mobHp})");
                }
                else
                {
                    Console.WriteLine($"Miss! The {selectedItem.Name} has no effect.");
                }

                member.Inventory.RemoveItem(slotIdx - 1);
                return;
            }

            // Item cannot be used in combat
            Console.WriteLine($"{selectedItem.Name} cannot be used in combat or has no effect.");
        }

        #endregion

        #region Stance Management

        private static void ChangeStanceMenu(Character character)
        {
            Console.WriteLine($"\n--- Combat Stance Selection for {character.Name} ---");
            Console.WriteLine($"Current: {CombatStanceModifiers.GetStanceDescription(character.CurrentStance)}");
            Console.WriteLine("\nAvailable Stances:");
            Console.WriteLine("1) ⚖️  BALANCED - No modifiers (default)");
            Console.WriteLine("2) ⚔️  AGGRESSIVE - +30% damage, -20% armor (glass cannon)");
            Console.WriteLine("3) 🛡️  DEFENSIVE - -20% damage, +40% armor (tank mode)");
            Console.WriteLine("0) Cancel");
            Console.Write("\nSelect stance: ");

            var choice = Console.ReadLine() ?? string.Empty;

            switch (choice.Trim())
            {
                case "1":
                    character.ChangeStance(CombatStance.Balanced);
                    break;
                case "2":
                    character.ChangeStance(CombatStance.Aggressive);
                    break;
                case "3":
                    character.ChangeStance(CombatStance.Defensive);
                    break;
                case "0":
                    Console.WriteLine("Stance unchanged.");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Stance unchanged.");
                    break;
            }
        }

        #endregion

        #region Helper Methods

        private static int GetAttackStat(Character c)
        {
            if (c is Mage) return c.GetTotalIntelligence();
            if (c is Rogue) return c.GetTotalAgility();
            if (c is Priest) return Math.Max(1, c.GetTotalIntelligence() / 2);
            return c.GetTotalStrength(); // Warrior and default
        }

        #endregion
    }
}
