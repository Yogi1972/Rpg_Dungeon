# ⚔️ Step 1: Enhanced Combat System - Implementation Guide

**Status:** ✅ IN PROGRESS  
**Priority:** HIGH  
**Impact:** HIGH  
**Started:** 2025  
**Target Version:** v3.1

---

## 📋 Overview

This document tracks the implementation of Step 1 from the Game Improvement Roadmap: **Enhanced Combat System**. The goal is to transform combat from simple d20 rolls into a strategic, ability-based system with status effects, class-specific powers, and tactical decision-making.

---

## ✅ Completed Features

### 1. Combat Ability System ✅ COMPLETE
**File:** `Combat/CombatAbility.cs`

**Features Implemented:**
- ✅ `CombatAbility` class with full ability mechanics
- ✅ Resource types: Mana, Stamina, Health, None
- ✅ Target types: SingleEnemy, AllEnemies, SingleAlly, AllAllies, Self
- ✅ Cooldown system
- ✅ Damage calculation with multipliers
- ✅ Status effect integration
- ✅ Resource cost management
- ✅ Visual icons for abilities

**Ability Properties:**
```csharp
- Name: Display name
- Description: What the ability does
- ResourceType: Mana/Stamina/Health/None
- ResourceCost: How much it costs
- Cooldown: Turns before reuse
- TargetType: Who can be targeted
- BaseDamage: Base damage value
- DamageMultiplier: Damage scaling (1.0 = 100%)
- StatusEffect: Optional status to apply
- StatusDuration: How long status lasts
- StatusPotency: Strength of status effect
- Icon: Visual representation
```

### 2. Class-Specific Abilities ✅ COMPLETE
**File:** `Combat/CombatAbility.cs` (AbilityFactory class)

#### Warrior Abilities (4 total)
1. **Power Strike** 💥
   - Cost: 15 Stamina
   - Cooldown: 2 turns
   - Effect: 150% damage single-target attack
   
2. **Defensive Stance** 🛡️
   - Cost: 10 Stamina
   - Cooldown: 4 turns
   - Effect: Regeneration (5 HP/turn for 3 turns)
   
3. **Whirlwind Attack** 🌪️
   - Cost: 25 Stamina
   - Cooldown: 5 turns
   - Effect: 80% damage to all enemies
   
4. **Intimidating Shout** 😠
   - Cost: 20 Stamina
   - Cooldown: 6 turns
   - Effect: Weaken all enemies (-30% damage)

#### Mage Abilities (4 total)
1. **Fireball** 🔥
   - Cost: 20 Mana
   - Cooldown: 2 turns
   - Effect: 180% damage + Burning (5 dmg/turn for 3 turns)
   
2. **Ice Bolt** ❄️
   - Cost: 18 Mana
   - Cooldown: 2 turns
   - Effect: 160% damage + Freeze (2 turns)
   
3. **Mana Shield** ✨
   - Cost: 25 Mana
   - Cooldown: 6 turns
   - Effect: Regeneration (8 HP/turn for 4 turns)
   
4. **Lightning Storm** ⚡
   - Cost: 40 Mana
   - Cooldown: 7 turns
   - Effect: 120% damage to all + Stun (1 turn)

#### Rogue Abilities (4 total)
1. **Backstab** 🗡️
   - Cost: 18 Stamina
   - Cooldown: 3 turns
   - Effect: 200% damage + Bleed (4 dmg/turn for 4 turns)
   
2. **Poison Blade** ☠️
   - Cost: 15 Stamina
   - Cooldown: 4 turns
   - Effect: 130% damage + Poison (6 dmg/turn for 5 turns)
   
3. **Shadow Step** 👤
   - Cost: 20 Stamina
   - Cooldown: 5 turns
   - Effect: Regeneration (10 HP/turn for 2 turns)
   
4. **Fan of Knives** 🔪
   - Cost: 25 Stamina
   - Cooldown: 6 turns
   - Effect: 90% damage to all enemies

#### Priest Abilities (4 total)
1. **Holy Smite** ✝️
   - Cost: 15 Mana
   - Cooldown: 2 turns
   - Effect: 140% holy damage
   
2. **Divine Shield** 🛡️
   - Cost: 20 Mana
   - Cooldown: 5 turns
   - Effect: Regeneration on ally (7 HP/turn for 4 turns)
   
3. **Healing Prayer** 🙏
   - Cost: 30 Mana
   - Cooldown: 4 turns
   - Effect: Regeneration on all allies (12 HP/turn for 3 turns)
   
4. **Wrath** ⚡
   - Cost: 35 Mana
   - Cooldown: 7 turns
   - Effect: 130% damage to all + Vulnerable (+30% damage taken)

### 3. Status Effect System ✅ ENHANCED
**File:** `Combat/StatusEffect.cs`

**Status Effect Types:**
- 🩸 **Bleeding:** Damage over time (physical)
- 💫 **Stunned:** Cannot act
- ☠️ **Poisoned:** Strong damage over time (nature)
- 🔥 **Burning:** Damage over time (fire)
- ❄️ **Frozen:** Reduced actions
- ⬇️ **Weakened:** -30% damage output
- 🎯 **Vulnerable:** +30% damage taken
- 💚 **Regenerating:** Healing over time

**Features:**
- ✅ Duration tracking
- ✅ Potency (strength of effect)
- ✅ Source tracking (who applied it)
- ✅ Color-coded display
- ✅ Icon representation
- ✅ Automatic expiration
- ✅ Stacking rules (stronger replaces weaker)

### 4. Character Integration ✅ COMPLETE
**File:** `Characters/Character.cs`

**Added Properties:**
```csharp
public List<CombatAbility> Abilities { get; set; }
public List<StatusEffect> ActiveStatusEffects { get; set; }
```

**Added Methods:**
```csharp
// Resource management
public void ModifyMana(int amount)
public void ModifyStamina(int amount)

// Ability management
public void InitializeAbilities()
public bool UseAbility(CombatAbility ability, Character? target = null)
public void ReduceAbilityCooldowns()

// Status effect management
public void AddStatusEffect(StatusEffect effect)
public void ProcessStatusEffects()
public bool HasStatusEffect(StatusEffectType type)
public void ClearStatusEffects()
```

---

## 🔨 In Progress Features

### 5. Combat System Integration ✅ COMPLETE
**File:** `Systems/Combat.cs`

**Features Implemented:**
- ✅ Enhanced combat menu with ability display
- ✅ Ability quick-select (A1-A4 hotkeys)
- ✅ Visual ability display with cooldowns and costs
- ✅ Status effect display in combat UI
- ✅ Ability usage integration
- ✅ Target selection for single-target abilities
- ✅ Damage calculation with ability multipliers
- ✅ Status effect application from abilities
- ✅ Cooldown reduction per turn
- ✅ Resource cost validation
- ✅ Enhanced turn display with better formatting

**UI Improvements:**
```
╔═══════════════════════════════════════════════════════════════════╗
║  Character's Turn (Lv 5)
╚═══════════════════════════════════════════════════════════════════╝
💚 HP: 85/100 | ⚡ Stamina: 40/50 | 🎯 Threat: 25
Stance: ⚔️ Aggressive | Status: 🔥Burning(2)

🎯 Enemy: Goblin (Lv 3) | HP: 40/60

⚔️  ABILITIES:
  A1. ✓ 💥 Power Strike [15 Stamina]
  A2. ✗ 🛡️ Defensive Stance [10 Stamina] (CD: 2)
  A3. ✓ 🌪️ Whirlwind Attack [25 Stamina]
  A4. ✓ 😠 Intimidating Shout [20 Stamina]

📋 ACTIONS:
1. ⚔️  Attack
2. ✨ Special (Legacy)
3. 🛡️  Taunt
4. 🔄 Change Stance
5. 💼 Use Item
6. ⏭️  Pass

💫 Quick Abilities: A1-A4

Action: _
```

**Integration Points:**
- Combat menu displays all abilities with status
- Players can use abilities via A1-A4 hotkeys
- Abilities consume resources properly
- Cooldowns track and reduce each turn
- Status effects apply from abilities
- Visual feedback for ability usage
- Threat generation from abilities

### 6. Character Ability Initialization ✅ COMPLETE
**File:** `Systems/GameInitializer.cs`

**Features Implemented:**
- ✅ Auto-initialize abilities on character creation
- ✅ Display learned abilities to player
- ✅ Abilities ready for immediate use

---

## ⏳ Pending Features

### 7. Turn Order System 🔨 TODO
**File:** `Combat/TurnOrderManager.cs` (NEW)

**Features to Implement:**
- [ ] Initiative-based turn calculation using Agility
- [ ] Turn queue display
- [ ] Speed-based multiple turns per round
- [ ] Turn indicator showing current actor
- [ ] Action economy (Move, Action, Bonus Action)

**Design:**
```csharp
internal class TurnOrderManager
{
    - CalculateInitiative() // Agility + d20 roll
    - GenerateTurnOrder() // Sort by initiative
    - GetNextActor() // Who goes next
    - DisplayTurnOrder() // Show UI
}
```

### 7. Combo Attack System 🔨 TODO
**File:** `Combat/ComboSystem.cs` (NEW)

**Features to Implement:**
- [ ] Detect ability combinations
- [ ] Track combo counter
- [ ] Apply combo bonuses
- [ ] Special combo effects
- [ ] Visual combo indicators

**Combo Examples:**
```
Warrior Power Strike + Rogue Backstab = "Coordinated Strike" (+50% damage)
Mage Fireball + Mage Ice Bolt = "Steam Explosion" (AoE damage)
Priest Holy Smite + Any Attack = "Blessed Strike" (+20% damage)
Multiple DoT effects = "Suffering" (stacks for bonus damage)
```

### 8. Enemy AI Behaviors 🔨 TODO
**File:** `Combat/Mob.cs`

**AI Types to Implement:**
- [ ] **Aggressive:** Always targets lowest HP character
- [ ] **Defensive:** Uses defensive abilities, high armor priority
- [ ] **Support:** Heals/buffs other enemies
- [ ] **Tactical:** Prioritizes mages and healers
- [ ] **Berserker:** Gains power when below 50% HP

**Implementation:**
```csharp
internal enum MobAI
{
    Aggressive,
    Defensive,
    Support,
    Tactical,
    Berserker
}

// Add to Mob class
public MobAI AIType { get; set; }
public Character SelectTarget(List<Character> party) // AI-based targeting
```

---

## ⏳ Pending Features

### 9. Enhanced Combat UI
- [ ] Health bars (visual representation)
- [ ] Status effect icons next to names
- [ ] Ability hotkeys (1-4 for abilities)
- [ ] Detailed combat log
- [ ] Damage numbers with colors
- [ ] Critical hit animations

### 10. Advanced Status Effects
- [ ] Status effect immunity (e.g., ice immunity)
- [ ] Status effect synergies (burn + poison = extra damage)
- [ ] Cleansing abilities (remove negative effects)
- [ ] Status effect spreading (burn can spread to adjacent enemies)

### 11. Champion Class Abilities
Extend abilities for specialized champion classes:
- [ ] Paladin: Divine abilities with healing
- [ ] Berserker: Rage-based high damage
- [ ] Archmage: Powerful elemental spells
- [ ] Assassin: Stealth and instant kill abilities
- [ ] Druid: Nature and shapeshifting abilities

---

## 🧪 Testing Checklist

### Unit Testing
- [ ] Test ability cooldown system
- [ ] Test resource consumption
- [ ] Test status effect application
- [ ] Test status effect stacking rules
- [ ] Test status effect expiration
- [ ] Test ability damage calculation

### Integration Testing
- [ ] Test abilities in actual combat
- [ ] Test multi-target abilities
- [ ] Test ally-targeting abilities
- [ ] Test status effects on mobs
- [ ] Test status effects on characters
- [ ] Test ability + status effect combos

### Balance Testing
- [ ] Ensure abilities aren't too powerful
- [ ] Verify cooldowns are fair
- [ ] Check resource costs are balanced
- [ ] Test combat duration (not too long/short)
- [ ] Verify status effects aren't overpowered

### User Experience Testing
- [ ] Abilities are easy to understand
- [ ] UI is clear and readable
- [ ] Icons make sense
- [ ] Combat feels strategic
- [ ] Player has meaningful choices

---

## 📊 Success Metrics

### Quantitative Metrics
- ✅ **Abilities Created:** 16/16 (4 per class)
- ✅ **Status Effects:** 8/8 types
- ✅ **Combat Integration:** 100% complete ⭐
- ⏳ **Turn System:** 0% complete
- ⏳ **Combo System:** 0% complete
- ⏳ **AI Behaviors:** 0% complete

### Qualitative Goals
- [ ] Combat feels strategic (not just "attack" spam)
- [ ] Abilities are used 60%+ of the time
- [ ] Status effects appear in 80%+ of combats
- [ ] Players report combat is "more fun"
- [ ] Each class feels unique in combat

### Performance Targets
- [ ] Combat turn processing < 500ms
- [ ] No lag when displaying abilities
- [ ] Status effect processing < 100ms
- [ ] Overall combat responsiveness maintained

---

## 🐛 Known Issues

None yet - implementation just started!

---

## 📝 Implementation Notes

### Design Decisions

1. **Why separate CombatAbility from SpecialAbility?**
   - CombatAbility is the new system with cooldowns, resources, targeting
   - SpecialAbility is the legacy system (will eventually deprecate)
   - Allows gradual migration without breaking existing code

2. **Why store abilities in Character rather than a separate manager?**
   - Each character has their own unique set of abilities
   - Easier to save/load character state
   - More intuitive for ability management
   - Follows object-oriented principles

3. **Why use factories for ability creation?**
   - Centralized ability definitions
   - Easy to modify ability stats
   - Supports future ability unlocking/upgrading
   - Cleaner than defining in character constructors

4. **Status effect stacking rules:**
   - Same type doesn't stack (stronger replaces weaker)
   - Different types can coexist
   - Duration refreshes if new effect is stronger
   - Prevents status effect spam

### Future Enhancements

Consider for v3.2+:
- Ability upgrades/talents
- Passive abilities
- Ultimate abilities (charge-based)
- Equipment that grants abilities
- Consumable items that grant temporary abilities
- PvP-specific ability balancing

### Performance Considerations

- Abilities list is small (4-6 per character)
- Status effects limited to prevent spam
- Cooldowns prevent ability spam
- Efficient status effect processing (reverse iteration)

---

## 🔄 Version History

| Version | Date | Changes |
|---------|------|---------|
| 0.1 | 2025-01-XX | Initial system design |
| 0.2 | 2025-01-XX | CombatAbility class created |
| 0.3 | 2025-01-XX | All class abilities implemented |
| 0.4 | 2025-01-XX | Character integration complete |
| 0.5 | 2025-01-XX | Status effect enhancement |
| 0.6 | 2025-01-XX | Combat menu integration complete ⭐ |
| 0.7 | 2025-01-XX | Ability display and usage complete ⭐ |
| 1.0 | TBD | Turn order system & Combo system |

---

## 📞 Next Steps

### Immediate Tasks (This Week)
1. ✅ Create CombatAbility system
2. ✅ Define all class abilities
3. ✅ Integrate with Character class
4. ⏳ Implement combat menu integration
5. ⏳ Add ability display in combat

### Short-term (Next 2 Weeks)
1. Complete combat system integration
2. Add visual effects for abilities
3. Implement turn order system
4. Begin combo system design
5. Start AI behavior implementation

### Long-term (By Month End)
1. Full combat overhaul complete
2. All systems tested and balanced
3. Champion class abilities added
4. Documentation complete
5. Ready for v3.1 release

---

## 🎓 Learning Resources

For anyone continuing this work:

### Key Concepts
- **Cooldowns:** Prevent ability spam, add strategy
- **Resource Management:** Mana/Stamina adds another layer of decisions
- **Status Effects:** Make combat dynamic and reactive
- **Targeting:** Different abilities need different targets
- **Damage Scaling:** Stats should affect ability power

### Code Patterns Used
- **Factory Pattern:** AbilityFactory creates abilities
- **Strategy Pattern:** Different AI behaviors
- **Observer Pattern:** Status effects notify on application
- **Composition:** Characters have abilities, not inheritance

### Testing Strategy
1. Unit test individual systems first
2. Integration test with actual combat
3. Balance test with playtesting
4. User experience test with feedback

---

## 📸 Before/After Comparison

### Before (v3.0)
```
Combat Options:
1. Attack
2. Special Ability
3. Change Stance
4. Use Item
5. Flee

> You attack the Goblin for 12 damage!
```

### After (v3.1) - Target State
```
Combat Options:
1. Attack
2. 💥 Power Strike [15 Stamina]
3. 🛡️ Defensive Stance [10 Stamina]
4. 🌪️ Whirlwind Attack [25 Stamina] (CD: 2)
5. Change Stance
6. Use Item
7. Flee

Status: 🔥 Burning (3 turns)

> You use Power Strike on the Goblin for 28 damage!
> Goblin takes 5 burning damage! 🔥
```

---

**Last Updated:** 2025  
**Current Phase:** Implementation  
**Progress:** 40% Complete (4/10 features)

