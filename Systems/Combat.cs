using Night.Characters;
using Night.Combat;
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

            Console.WriteLine();
            if (mobRank.Contains("BOSS") || mobRank.Contains("ELITE"))
            {
                VisualEffects.WriteDanger($"⚔️  COMBAT! A Level {mob.Level} {mob.Name} {mobRank} appears! (HP {mobHp})\n");
            }
            else
            {
                VisualEffects.WriteLineColored($"⚔️  Encounter begins! A Level {mob.Level} {mob.Name} {mobRank} appears! (HP {mobHp})", ConsoleColor.Yellow);
            }
            Console.WriteLine();

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

                    // Reduce ability cooldowns
                    if (member.Abilities != null && member.Abilities.Count > 0)
                    {
                        member.ReduceAbilityCooldowns();
                    }

                    // Process character's own status effects
                    if (member.ActiveStatusEffects != null && member.ActiveStatusEffects.Count > 0)
                    {
                        member.ProcessStatusEffects();
                    }

                    // Check if stunned - skip turn
                    if (StatusEffectManager.IsStunned(member))
                    {
                        Console.WriteLine($"\n💫 {member.Name} is stunned and skips this turn!");
                        continue;
                    }

                    // Check character's own status for stun
                    if (member.HasStatusEffect(StatusEffectType.Stunned))
                    {
                        Console.WriteLine($"\n💫 {member.Name} is stunned and skips this turn!");
                        continue;
                    }

                    // Prompt player for action
                    Console.WriteLine($"\n╔═══════════════════════════════════════════════════════════════════╗");
                    Console.WriteLine($"║  {member.Name}'s Turn (Lv {member.Level})");
                    Console.WriteLine($"╚═══════════════════════════════════════════════════════════════════╝");

                    // Display stats
                    Console.Write($"💚 HP: {member.Health}/{member.MaxHealth}");
                    if (member is Warrior || member is Rogue)
                        Console.Write($" | ⚡ Stamina: {member.Stamina}/{member.MaxStamina}");
                    else if (member is Mage || member is Priest)
                        Console.Write($" | 🔮 Mana: {member.Mana}/{member.MaxMana}");

                    Console.Write($" | 🎯 Threat: {member.ThreatLevel}");
                    if (member is Warrior w && w.IsTaunting)
                    {
                        Console.Write($" | 🛡️ [TAUNTING:{w.TauntDuration}]");
                    }
                    Console.WriteLine();

                    // Display stance
                    Console.Write($"Stance: {CombatStanceModifiers.GetStanceIcon(member.CurrentStance)} {member.CurrentStance}");

                    // Display active status effects
                    if (member.ActiveStatusEffects.Any())
                    {
                        Console.Write(" | Status: ");
                        foreach (var effect in member.ActiveStatusEffects)
                        {
                            Console.Write($"{effect.GetIcon()}{effect.Type}({effect.Duration}) ");
                        }
                    }
                    Console.WriteLine();

                    // Display enemy status
                    Console.WriteLine($"\n🎯 Enemy: {mob.Name} (Lv {mob.Level}) | HP: {mobHp}/{mob.Health}");

                    // Display abilities if initialized
                    if (member.Abilities != null && member.Abilities.Count > 0)
                    {
                        Console.WriteLine("\n⚔️  ABILITIES:");
                        DisplayAbilities(member);
                    }

                    Console.WriteLine("\n📋 ACTIONS:");
                    Console.Write($"1. ⚔️  Attack ");
                    Console.Write($"\n2. ✨ Special (Legacy) ");

                    if (member is Warrior warrior)
                    {
                        Console.Write($"\n3. 🛡️  Taunt (Draw aggro) ");
                        Console.Write($"\n4. 🔄 Change Stance ");
                        Console.Write($"\n5. 💼 Use Item ");
                        Console.Write($"\n6. ⏭️  Pass ");
                    }
                    else
                    {
                        Console.Write($"\n3. 🔄 Change Stance ");
                        Console.Write($"\n4. 💼 Use Item ");
                        Console.Write($"\n5. ⏭️  Pass ");
                    }

                    // Add ability quick-select if available
                    if (member.Abilities != null && member.Abilities.Count > 0)
                    {
                        Console.Write($"\n\n💫 Quick Abilities: A1-A{member.Abilities.Count}");
                    }

                    Console.Write("\n\nAction: ");

                    // This is to get the users input for their action, in a real game this would be more robust with error handling and possibly a UI instead of console.
                    String act = Console.ReadLine() ?? string.Empty;
                    act = act.Trim().ToUpper();

                    // Handle ability quick-select (A1, A2, A3, A4)
                    if (act.StartsWith("A") && act.Length >= 2 && member.Abilities != null && member.Abilities.Count > 0)
                    {
                        if (int.TryParse(act.Substring(1), out int abilityIndex) && abilityIndex >= 1 && abilityIndex <= member.Abilities.Count)
                        {
                            var ability = member.Abilities[abilityIndex - 1];
                            UseAbilityInCombat(member, ability, mob, ref mobHp, party);
                            SpecialUsedLastTurn[member.Name] = false;
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Invalid ability selection!");
                            continue;
                        }
                    }

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
                            VisualEffects.WriteDanger($"\n❌ {member.Name} critically misses! {VisualEffects.GetRandomMissMessage()}\n");
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
                                VisualEffects.WriteColored(" [SKILL CRIT!]", ConsoleColor.Yellow);
                            }
                        }

                        if (crit || total > targetDefense)
                        {
                            if (crit)
                            {
                                VisualEffects.ShowCriticalHitEffect();
                                Console.WriteLine($" {VisualEffects.GetRandomCritMessage()}");
                            }

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
                            VisualEffects.WriteDamage($"{member.Name} hits {mob.Name} for {finalDamage} damage! ");
                            VisualEffects.WriteInfo($"(mob HP: {mobHp}/{mob.Health})\n");

                            // Generate threat based on damage dealt
                            int threat = finalDamage;
                            if (member is Warrior) threat = (int)(threat * 1.5); // Warriors generate 50% more threat
                            member.ThreatLevel += threat;
                        }
                        else
                        {
                            VisualEffects.WriteInfo($"{member.Name} misses {mob.Name}. {VisualEffects.GetRandomMissMessage()}\n");
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
                Console.WriteLine();
                VisualEffects.ShowVictoryBanner();
                VisualEffects.WriteSuccess($"💀 {mob.Name} was {VisualEffects.GetRandomKillMessage()}\n");

                var loot = mob.DropLoot(_rng);

                // Check for legendary drop (very rare!)
                var leader = party.FirstOrDefault(p => p.IsAlive);
                if (leader != null)
                {
                    var legendaryItem = LegendaryItemSystem.TryGenerateLegendaryDrop(leader.Level);
                    if (legendaryItem != null)
                    {
                        LegendaryItemSystem.AnnounceItemFound(legendaryItem);
                        leader.Inventory.AddItem(legendaryItem);
                        VisualEffects.WriteSuccess($"✨ {leader.Name} received: {legendaryItem.Name}!\n");
                    }
                }

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

                    // Show XP progress bar
                    VisualEffects.DrawProgressBarLine(
                        member.Experience,
                        member.ExperienceToNextLevel,
                        20,
                        $"  {member.Name}'s XP"
                    );

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

                    VisualEffects.WriteSuccess($"💰 The party receives {totalGoldAmount} gold ({goldPerMember} per member)!\n");
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
                            VisualEffects.WriteSuccess($"🎁 {receiver.Name} finds {item.Name}!\n");
                            itemAdded = true;
                        }
                    }

                    if (!itemAdded)
                    {
                        VisualEffects.WriteInfo($"❌ No space to pick up {item.Name}; it was left behind.\n");
                    }

                    itemIndex++;
                }
            }
            else
            {
                VisualEffects.ShowDefeatBanner();
                VisualEffects.WriteDanger("💀 The party was defeated...\n");
            }

            // Clear all status effects after combat
            StatusEffectManager.ClearAllCombatEffects();

            return partyWon;
        }

        /// <summary>
        /// Run an encounter with initiative-based turn order system
        /// </summary>
        public static bool RunEncounterWithTurnOrder(List<Character> party, Mob mob)
        {
            if (party == null || party.Count == 0 || mob == null) return false;

            int mobHp = mob.Health;

            // Calculate party average level
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

            // Display combat start banner
            Console.WriteLine();
            if (mobRank.Contains("BOSS") || mobRank.Contains("ELITE"))
            {
                VisualEffects.WriteDanger($"⚔️  COMBAT! A Level {mob.Level} {mob.Name} {mobRank} appears! (HP {mobHp})\n");
            }
            else
            {
                VisualEffects.WriteLineColored($"⚔️  Encounter begins! A Level {mob.Level} {mob.Name} {mobRank} appears! (HP {mobHp})", ConsoleColor.Yellow);
            }
            Console.WriteLine();

            // Reset threat levels at combat start
            foreach (var p in party)
            {
                p.ThreatLevel = 0;
                if (p is Warrior warrior)
                {
                    warrior.IsTaunting = false;
                    warrior.TauntDuration = 0;
                }
            }

            // Initialize turn order system
            var turnOrderManager = new TurnOrderManager();
            var enemies = new List<Mob> { mob };

            // For single player, just use first character
            var player = party.FirstOrDefault(p => p.IsAlive);
            if (player == null) return false;

            turnOrderManager.GenerateTurnOrder(player, enemies);

            // Display initiative rolls
            Console.WriteLine("🎲 Initiative Rolls:");
            foreach (var p in party.Where(x => x.IsAlive))
            {
                Console.WriteLine($"  {p.Name}: Agility {p.Agility} → Initiative calculated");
            }
            Console.WriteLine($"  {mob.Name}: Level {mob.Level} → Initiative calculated");
            Console.WriteLine();

            // Display turn order
            turnOrderManager.DisplayTurnOrder();

            // Main combat loop with turn order
            while (turnOrderManager.ShouldContinueCombat() && mobHp > 0)
            {
                var actor = turnOrderManager.GetNextActor();

                if (actor.IsPlayer && actor.Character != null)
                {
                    // Player turn
                    var member = actor.Character;

                    if (!member.IsAlive)
                    {
                        turnOrderManager.AdvanceTurn();
                        continue;
                    }

                    // Process status effects at start of turn
                    StatusEffectManager.ProcessEffects(member);

                    if (member.Abilities != null && member.Abilities.Count > 0)
                    {
                        member.ReduceAbilityCooldowns();
                    }

                    if (member.ActiveStatusEffects != null && member.ActiveStatusEffects.Count > 0)
                    {
                        member.ProcessStatusEffects();
                    }

                    // Check if stunned
                    if (StatusEffectManager.IsStunned(member) || member.HasStatusEffect(StatusEffectType.Stunned))
                    {
                        Console.WriteLine($"\n💫 {member.Name} is stunned and skips this turn!");
                        turnOrderManager.AdvanceTurn();
                        continue;
                    }

                    // Display turn header
                    Console.WriteLine($"\n╔═══════════════════════════════════════════════════════════════════╗");
                    Console.WriteLine($"║  {member.Name}'s Turn (Lv {member.Level}) - Round {turnOrderManager.GetRoundNumber()}");
                    Console.WriteLine($"╚═══════════════════════════════════════════════════════════════════╝");

                    // Display stats
                    Console.Write($"💚 HP: {member.Health}/{member.MaxHealth}");
                    if (member is Warrior || member is Rogue)
                        Console.Write($" | ⚡ Stamina: {member.Stamina}/{member.MaxStamina}");
                    else if (member is Mage || member is Priest)
                        Console.Write($" | 🔮 Mana: {member.Mana}/{member.MaxMana}");

                    Console.Write($" | 🎯 Threat: {member.ThreatLevel}");
                    if (member is Warrior w && w.IsTaunting)
                    {
                        Console.Write($" | 🛡️ [TAUNTING:{w.TauntDuration}]");
                    }
                    Console.WriteLine();

                    // Display stance and status
                    Console.Write($"Stance: {CombatStanceModifiers.GetStanceIcon(member.CurrentStance)} {member.CurrentStance}");
                    if (member.ActiveStatusEffects.Any())
                    {
                        Console.Write(" | Status: ");
                        foreach (var effect in member.ActiveStatusEffects)
                        {
                            Console.Write($"{effect.GetIcon()}{effect.Type}({effect.Duration}) ");
                        }
                    }
                    Console.WriteLine();

                    // Display enemy status
                    Console.WriteLine($"\n🎯 Enemy: {mob.Name} (Lv {mob.Level}) | HP: {mobHp}/{mob.Health}");

                    // Display abilities
                    if (member.Abilities != null && member.Abilities.Count > 0)
                    {
                        Console.WriteLine("\n⚔️  ABILITIES:");
                        DisplayAbilities(member);
                    }

                    // Display actions
                    Console.WriteLine("\n📋 ACTIONS:");
                    Console.Write($"1. ⚔️  Attack ");
                    Console.Write($"\n2. ✨ Special (Legacy) ");

                    if (member is Warrior warrior)
                    {
                        Console.Write($"\n3. 🛡️  Taunt (Draw aggro) ");
                        Console.Write($"\n4. 🔄 Change Stance ");
                        Console.Write($"\n5. 💼 Use Item ");
                        Console.Write($"\n6. ⏭️  Pass ");
                    }
                    else
                    {
                        Console.Write($"\n3. 🔄 Change Stance ");
                        Console.Write($"\n4. 💼 Use Item ");
                        Console.Write($"\n5. ⏭️  Pass ");
                    }

                    if (member.Abilities != null && member.Abilities.Count > 0)
                    {
                        Console.Write($"\n\n💫 Quick Abilities: A1-A{member.Abilities.Count}");
                    }

                    Console.Write("\n\nAction: ");
                    string act = Console.ReadLine()?.Trim().ToUpper() ?? string.Empty;

                    // Handle ability quick-select
                    if (act.StartsWith("A") && act.Length >= 2 && member.Abilities != null && member.Abilities.Count > 0)
                    {
                        if (int.TryParse(act.Substring(1), out int abilityIndex) && abilityIndex >= 1 && abilityIndex <= member.Abilities.Count)
                        {
                            var ability = member.Abilities[abilityIndex - 1];
                            UseAbilityInCombat(member, ability, mob, ref mobHp, party);
                            SpecialUsedLastTurn[member.Name] = false;
                        }
                        else
                        {
                            Console.WriteLine("Invalid ability selection!");
                        }
                    }
                    else if (act == "1")
                    {
                        // Attack
                        SpecialUsedLastTurn[member.Name] = false;
                        PerformBasicAttack(member, mob, ref mobHp);
                    }
                    else if (act == "2")
                    {
                        UseSpecialAgainstMob(member, mob, ref mobHp, party);
                    }
                    else if (member is Warrior && act == "3")
                    {
                        SpecialUsedLastTurn[member.Name] = false;
                        ((Warrior)member).Taunt();
                    }
                    else if (member is Warrior && act == "4")
                    {
                        ChangeStanceMenu(member);
                    }
                    else if (member is Warrior && act == "5")
                    {
                        SpecialUsedLastTurn[member.Name] = false;
                        UseItemDuringCombat(member, party, mob, ref mobHp);
                    }
                    else if (!(member is Warrior) && act == "3")
                    {
                        ChangeStanceMenu(member);
                    }
                    else if (!(member is Warrior) && act == "4")
                    {
                        SpecialUsedLastTurn[member.Name] = false;
                        UseItemDuringCombat(member, party, mob, ref mobHp);
                    }
                    else
                    {
                        Console.WriteLine($"{member.Name} passes.");
                    }

                    // Update actor status
                    turnOrderManager.UpdateActorStatus(true, member.Name, member.IsAlive);
                }
                else if (!actor.IsPlayer && actor.Mob != null)
                {
                    // Mob turn
                    if (mobHp <= 0)
                    {
                        turnOrderManager.UpdateActorStatus(false, actor.Mob.Name, false);
                        turnOrderManager.AdvanceTurn();
                        continue;
                    }

                    Console.WriteLine($"\n╔═══════════════════════════════════════════╗");
                    Console.WriteLine($"║  {actor.Mob.Name}'s Turn - Round {turnOrderManager.GetRoundNumber()}");
                    Console.WriteLine($"╚═══════════════════════════════════════════╝");

                    // Decrement taunt duration for warriors
                    foreach (var member in party.Where(p => p is Warrior))
                    {
                        ((Warrior)member).DecrementTaunt();
                    }

                    // Find target based on threat
                    var alive = party.Where(p => p.IsAlive).ToList();
                    if (alive.Count == 0) break;

                    Character target;
                    var taunters = alive.Where(p => p is Warrior warrior && warrior.IsTaunting).ToList();
                    if (taunters.Any())
                    {
                        target = taunters.OrderByDescending(p => p.ThreatLevel).First();
                        Console.WriteLine($"🎯 {target.Name}'s taunt forces {actor.Mob.Name} to attack!");
                    }
                    else
                    {
                        target = alive.OrderByDescending(p => p.ThreatLevel).First();
                        if (target.ThreatLevel > 0)
                        {
                            Console.WriteLine($"🎯 {actor.Mob.Name} targets {target.Name} (Threat: {target.ThreatLevel})!");
                        }
                    }

                    Attack(actor.Mob, target);

                    // Update actor status
                    turnOrderManager.UpdateActorStatus(false, actor.Mob.Name, mobHp > 0);
                }

                // Check if combat should end
                if (mobHp <= 0)
                {
                    turnOrderManager.UpdateActorStatus(false, mob.Name, false);
                    break;
                }

                // Advance to next turn
                turnOrderManager.AdvanceTurn();

                // Optional: Show turn order every few turns
                if (turnOrderManager.GetRoundNumber() > 1 && actor.IsPlayer)
                {
                    Console.WriteLine("\n[Press Enter to continue]");
                    Console.ReadLine();
                }
            }

            // Combat ended - determine winner and handle rewards
            bool partyWon = mobHp <= 0;

            if (partyWon)
            {
                Console.WriteLine();
                VisualEffects.ShowVictoryBanner();
                VisualEffects.WriteSuccess($"💀 {mob.Name} was {VisualEffects.GetRandomKillMessage()}\n");

                var loot = mob.DropLoot(_rng);

                // Check for legendary drop
                var leader = party.FirstOrDefault(p => p.IsAlive);
                if (leader != null)
                {
                    var legendaryItem = LegendaryItemSystem.TryGenerateLegendaryDrop(leader.Level);
                    if (legendaryItem != null)
                    {
                        LegendaryItemSystem.AnnounceItemFound(legendaryItem);
                        leader.Inventory.AddItem(legendaryItem);
                        VisualEffects.WriteSuccess($"✨ {leader.Name} received: {legendaryItem.Name}!\n");
                    }
                }

                // Award experience
                int xp = Playerleveling.CalculateXPReward(mob, party.Count);

                foreach (var member in party.Where(p => p.IsAlive))
                {
                    int memberXP = xp;

                    if (member.Pet != null && member.Pet.Ability == PetAbility.ExperienceBoost)
                    {
                        memberXP = (int)(memberXP * 1.10);
                    }

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

                    VisualEffects.DrawProgressBarLine(
                        member.Experience,
                        member.ExperienceToNextLevel,
                        20,
                        $"  {member.Name}'s XP"
                    );

                    if (member.Pet != null)
                    {
                        member.Pet.GainExperience(xp / 4);
                    }

                    // Pet post-combat effects
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

                // Distribute gold
                if (loot.Gold > 0)
                {
                    int totalGoldAmount = loot.Gold;
                    var bonusReceiver = party.FirstOrDefault(p => p.IsAlive) ?? party[0];

                    if (bonusReceiver.Pet != null && bonusReceiver.Pet.Ability == PetAbility.LootBonus)
                    {
                        totalGoldAmount = (int)(totalGoldAmount * 1.20);
                    }

                    if (bonusReceiver.SkillTree != null)
                    {
                        double goldBonusPercent = bonusReceiver.SkillTree.GetTotalGoldBonusPercent();
                        if (goldBonusPercent > 0)
                        {
                            totalGoldAmount = (int)(totalGoldAmount * (1.0 + goldBonusPercent));
                        }
                    }

                    int goldPerMember = totalGoldAmount / party.Count;
                    int remainder = totalGoldAmount % party.Count;

                    foreach (var member in party)
                    {
                        int memberGold = goldPerMember;
                        if (member == party[0] && remainder > 0)
                        {
                            memberGold += remainder;
                        }
                        member.Inventory.AddGold(memberGold);
                    }

                    VisualEffects.WriteSuccess($"💰 The party receives {totalGoldAmount} gold ({goldPerMember} per member)!\n");
                }

                // Distribute items
                int itemIndex = 0;
                foreach (var item in loot.Items)
                {
                    bool itemAdded = false;
                    for (int attempt = 0; attempt < party.Count && !itemAdded; attempt++)
                    {
                        var receiver = party[(itemIndex + attempt) % party.Count];
                        if (receiver.Inventory.AddItem(item))
                        {
                            VisualEffects.WriteSuccess($"🎁 {receiver.Name} finds {item.Name}!\n");
                            itemAdded = true;
                        }
                    }

                    if (!itemAdded)
                    {
                        VisualEffects.WriteInfo($"❌ No space to pick up {item.Name}; it was left behind.\n");
                    }

                    itemIndex++;
                }
            }
            else
            {
                VisualEffects.ShowDefeatBanner();
                VisualEffects.WriteDanger("💀 The party was defeated...\n");
            }

            // Clear all status effects after combat
            StatusEffectManager.ClearAllCombatEffects();

            return partyWon;
        }

        /// <summary>
        /// Perform a basic attack (extracted for reuse)
        /// </summary>
        private static void PerformBasicAttack(Character member, Mob mob, ref int mobHp)
        {
            int roll = RollD20();
            int attackStat = GetAttackStat(member);
            int targetDefense = 10 + (mob.Agility / 2);

            Console.Write($"{member.Name} rolls d20: {roll} (+{attackStat}) against defense {targetDefense}");

            if (roll == 1)
            {
                VisualEffects.WriteDanger($"\n❌ {member.Name} critically misses! {VisualEffects.GetRandomMissMessage()}\n");
                return;
            }

            int total = roll + attackStat;
            bool crit = roll == 20;

            // Check for additional crit chance from skills
            if (!crit && member.SkillTree != null)
            {
                int critChance = member.SkillTree.GetTotalCritChanceBonus();
                if (critChance > 0 && _rng.Next(1, 101) <= critChance)
                {
                    crit = true;
                    VisualEffects.WriteColored(" [SKILL CRIT!]", ConsoleColor.Yellow);
                }
            }

            if (crit || total > targetDefense)
            {
                if (crit)
                {
                    VisualEffects.ShowCriticalHitEffect();
                    Console.WriteLine($" {VisualEffects.GetRandomCritMessage()}");
                }

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

                // Apply pet damage boost
                if (member.Pet != null && member.Pet.Ability == PetAbility.DamageBoost)
                {
                    dmg = (int)(dmg * 1.05);
                }

                // Apply life steal
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
                    finalDamage = Math.Max(1, dmg - mob.ArmorRating);
                    if (finalDamage < dmg)
                    {
                        Console.Write($" [Mob AR: {mob.ArmorRating}, {dmg} → {finalDamage}]");
                    }
                }

                mobHp = Math.Max(0, mobHp - finalDamage);
                VisualEffects.WriteDamage($"{member.Name} hits {mob.Name} for {finalDamage} damage! ");
                VisualEffects.WriteInfo($"(mob HP: {mobHp}/{mob.Health})\n");

                // Generate threat
                int threat = finalDamage;
                if (member is Warrior) threat = (int)(threat * 1.5);
                member.ThreatLevel += threat;
            }
            else
            {
                VisualEffects.WriteInfo($"{member.Name} misses {mob.Name}. {VisualEffects.GetRandomMissMessage()}\n");
            }
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

        #region Ability System

        /// <summary>
        /// Display character's available abilities with cooldowns and costs
        /// </summary>
        private static void DisplayAbilities(Character character)
        {
            if (character.Abilities == null || character.Abilities.Count == 0)
            {
                Console.WriteLine("  No abilities available.");
                return;
            }

            for (int i = 0; i < character.Abilities.Count; i++)
            {
                var ability = character.Abilities[i];
                Console.ForegroundColor = ability.CanUse(character) ? ConsoleColor.Cyan : ConsoleColor.DarkGray;

                string status = ability.CanUse(character) ? "✓" : "✗";
                string cooldownText = ability.CurrentCooldown > 0 ? $" (CD: {ability.CurrentCooldown})" : "";
                string costText = ability.ResourceCost > 0 ? $" [{ability.ResourceCost} {ability.ResourceType}]" : "";

                Console.WriteLine($"  A{i + 1}. {status} {ability.Icon} {ability.Name}{costText}{cooldownText}");

                if (!ability.CanUse(character) && ability.CurrentCooldown == 0)
                {
                    Console.WriteLine($"      ⚠️  Insufficient {ability.ResourceType}");
                }

                Console.ResetColor();
            }
        }

        /// <summary>
        /// Use an ability in combat
        /// </summary>
        private static void UseAbilityInCombat(Character user, CombatAbility ability, Mob target, ref int mobHp, List<Character> party)
        {
            if (!ability.CanUse(user))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n❌ {user.Name} cannot use {ability.Name}!");
                Console.ResetColor();
                return;
            }

            // Consume resources
            ability.ConsumeResource(user);

            // Display ability usage
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"╔═══════════════════════════════════════════╗");
            Console.WriteLine($"║  {ability.Icon} {ability.Name.ToUpper()}");
            Console.WriteLine($"╚═══════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine($"{user.Name} uses {ability.Name}!");

            // Apply ability effects based on target type
            switch (ability.TargetType)
            {
                case TargetType.SingleEnemy:
                    ApplyAbilityToEnemy(user, ability, target, ref mobHp);
                    break;

                case TargetType.AllEnemies:
                    // For single mob encounters, just apply to the mob
                    ApplyAbilityToEnemy(user, ability, target, ref mobHp);
                    Console.WriteLine($"💥 (Would hit all enemies in group combat)");
                    break;

                case TargetType.Self:
                    ApplyAbilityToAlly(user, ability, user);
                    break;

                case TargetType.SingleAlly:
                    // Prompt for ally selection
                    var selectedAlly = SelectAllyTarget(party);
                    if (selectedAlly != null)
                    {
                        ApplyAbilityToAlly(user, ability, selectedAlly);
                    }
                    break;

                case TargetType.AllAllies:
                    foreach (var ally in party.Where(p => p.IsAlive))
                    {
                        ApplyAbilityToAlly(user, ability, ally);
                    }
                    break;
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Apply ability effects to an enemy
        /// </summary>
        private static void ApplyAbilityToEnemy(Character user, CombatAbility ability, Mob target, ref int mobHp)
        {
            if (ability.BaseDamage > 0 || ability.DamageMultiplier > 1.0)
            {
                int damage = ability.CalculateDamage(user);

                // Apply combat stance modifier
                double stanceMultiplier = CombatStanceModifiers.GetDamageMultiplier(user.CurrentStance);
                if (stanceMultiplier != 1.0)
                {
                    int oldDamage = damage;
                    damage = (int)(damage * stanceMultiplier);
                    Console.WriteLine($"  {CombatStanceModifiers.GetStanceIcon(user.CurrentStance)} Stance Bonus: {oldDamage} → {damage}");
                }

                // Apply mob armor
                int finalDamage = Math.Max(1, damage - target.ArmorRating);
                if (finalDamage < damage)
                {
                    Console.WriteLine($"  🛡️  Armor Reduction: {damage} → {finalDamage}");
                }

                mobHp = Math.Max(0, mobHp - finalDamage);

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  💥 {finalDamage} damage dealt to {target.Name}!");
                Console.ResetColor();
                Console.WriteLine($"  📊 {target.Name} HP: {mobHp}/{target.Health}");

                // Generate threat
                int threat = finalDamage;
                if (user is Warrior) threat = (int)(threat * 1.5);
                user.ThreatLevel += threat;
            }

            // Apply status effect if ability has one
            if (ability.StatusEffect.HasValue)
            {
                var statusEffect = new StatusEffect(
                    ability.StatusEffect.Value,
                    ability.StatusDuration,
                    ability.StatusPotency,
                    user.Name
                );

                // For now, display that status would be applied (mob status system needs enhancement)
                Console.ForegroundColor = statusEffect.GetColor();
                Console.WriteLine($"  {statusEffect.GetIcon()} {statusEffect.Type} applied! ({ability.StatusDuration} turns, {ability.StatusPotency} potency)");
                Console.ResetColor();

                // TODO: Apply to mob when mob status effect system is implemented
            }
        }

        /// <summary>
        /// Apply ability effects to an ally
        /// </summary>
        private static void ApplyAbilityToAlly(Character user, CombatAbility ability, Character target)
        {
            // Apply status effect if ability has one
            if (ability.StatusEffect.HasValue)
            {
                var statusEffect = new StatusEffect(
                    ability.StatusEffect.Value,
                    ability.StatusDuration,
                    ability.StatusPotency,
                    user.Name
                );

                target.AddStatusEffect(statusEffect);

                Console.ForegroundColor = statusEffect.GetColor();
                Console.WriteLine($"  {statusEffect.GetIcon()} {statusEffect.Type} applied to {target.Name}! ({ability.StatusDuration} turns)");
                Console.ResetColor();

                // Generate small amount of threat for support abilities
                user.ThreatLevel += 5;
            }
        }

        /// <summary>
        /// Let player select an ally target
        /// </summary>
        private static Character? SelectAllyTarget(List<Character> party)
        {
            Console.WriteLine("\nSelect target ally:");
            var aliveAllies = party.Where(p => p.IsAlive).ToList();

            for (int i = 0; i < aliveAllies.Count; i++)
            {
                var ally = aliveAllies[i];
                Console.WriteLine($"  {i + 1}. {ally.Name} - HP: {ally.Health}/{ally.MaxHealth}");
            }

            Console.Write("Select (1-{0}): ", aliveAllies.Count);
            string input = Console.ReadLine() ?? "";

            if (int.TryParse(input, out int choice) && choice >= 1 && choice <= aliveAllies.Count)
            {
                return aliveAllies[choice - 1];
            }

            Console.WriteLine("Invalid selection!");
            return null;
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
