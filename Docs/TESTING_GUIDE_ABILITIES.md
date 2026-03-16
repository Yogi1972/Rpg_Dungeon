# 🧪 Combat Abilities Testing Guide

**Version:** 3.1 (Step 1 Phase 2)  
**Purpose:** Guide for testing the new combat ability system  
**Status:** Ready for Testing ✅

---

## 🎯 Quick Start Testing

### 1. Start a New Game
```
1. Run the game
2. Select "New Game"
3. Create a party (recommend 1-2 characters for initial testing)
4. Choose different classes (Warrior, Mage, Rogue, Priest)
5. Note: Abilities are automatically learned on creation!
```

### 2. Get Into Combat
```
1. Go to Town menu
2. Select "Explore" or "Dungeon"
3. Encounter an enemy
4. Combat screen will appear
```

### 3. Use Abilities
```
Combat Options:
- Type "A1" for first ability
- Type "A2" for second ability  
- Type "A3" for third ability
- Type "A4" for fourth ability

OR use traditional menu:
- "1" for basic attack
- "2" for legacy special
```

---

## 📋 Test Checklist

### Basic Functionality
- [ ] Abilities display in combat menu
- [ ] Abilities show correct icons
- [ ] Cooldowns display correctly
- [ ] Resource costs display correctly
- [ ] Unavailable abilities show as grayed out (✗)
- [ ] Available abilities show as green (✓)
- [ ] Status effects display next to character name

### Resource Management
- [ ] Mana abilities consume mana (Mage/Priest)
- [ ] Stamina abilities consume stamina (Warrior/Rogue)
- [ ] Cannot use ability without enough resources
- [ ] Resource display updates after ability use

### Cooldowns
- [ ] Using ability puts it on cooldown
- [ ] Cooldown displays turn count
- [ ] Cannot use ability while on cooldown
- [ ] Cooldown reduces by 1 each turn
- [ ] Can use ability again when cooldown reaches 0

### Damage Abilities
- [ ] Single-target abilities damage enemy
- [ ] Damage is calculated with stat bonuses
- [ ] Critical hits work with abilities
- [ ] Stance modifiers affect ability damage
- [ ] Armor reduction applies to abilities

### Status Effects
- [ ] Abilities apply status effects
- [ ] Status effect icons display
- [ ] Status effect duration counts down
- [ ] Damage-over-time effects deal damage
- [ ] Buff effects provide benefits
- [ ] Status effects expire after duration

### Support Abilities
- [ ] Healing abilities restore HP
- [ ] Buff abilities apply positive effects
- [ ] Can target allies with single-target abilities
- [ ] All-ally abilities affect entire party

---

## 🗂️ Class-Specific Tests

### Warrior Testing
**Abilities:** Power Strike, Defensive Stance, Whirlwind, Intimidating Shout

**Test Cases:**
1. **Power Strike**
   - Use when at full stamina (15 cost)
   - Should deal 150% damage
   - Cooldown: 2 turns
   - Expected: High single-target damage

2. **Defensive Stance**
   - Use when health is low (10 stamina)
   - Should apply Regeneration status
   - Cooldown: 4 turns
   - Expected: Heal 5 HP per turn for 3 turns

3. **Whirlwind Attack**
   - Use against enemies (25 stamina)
   - Should hit all enemies (note: shows message for group combat)
   - Cooldown: 5 turns
   - Expected: Moderate AoE damage

4. **Intimidating Shout**
   - Use to debuff enemies (20 stamina)
   - Should apply Weakened status to all
   - Cooldown: 6 turns
   - Expected: Enemies deal -30% damage

### Mage Testing
**Abilities:** Fireball, Ice Bolt, Mana Shield, Lightning Storm

**Test Cases:**
1. **Fireball**
   - Use when at full mana (20 cost)
   - Should deal 180% damage + Burning
   - Cooldown: 2 turns
   - Expected: High damage + DoT (5 dmg/turn × 3)

2. **Ice Bolt**
   - Use against enemy (18 mana)
   - Should deal 160% damage + Freeze
   - Cooldown: 2 turns
   - Expected: Moderate damage + crowd control

3. **Mana Shield**
   - Use when health is low (25 mana)
   - Should apply strong Regeneration
   - Cooldown: 6 turns
   - Expected: Heal 8 HP per turn for 4 turns

4. **Lightning Storm**
   - Use ultimate ability (40 mana)
   - Should damage all + Stun
   - Cooldown: 7 turns
   - Expected: AoE damage + skip enemy turn

### Rogue Testing
**Abilities:** Backstab, Poison Blade, Shadow Step, Fan of Knives

**Test Cases:**
1. **Backstab**
   - Use for massive damage (18 stamina)
   - Should deal 200% damage + Bleed
   - Cooldown: 3 turns
   - Expected: Highest single-target damage + DoT

2. **Poison Blade**
   - Use for strong DoT (15 stamina)
   - Should deal 130% damage + Poison
   - Cooldown: 4 turns
   - Expected: Poison deals 6 dmg/turn × 5

3. **Shadow Step**
   - Use defensive ability (20 stamina)
   - Should apply Regeneration
   - Cooldown: 5 turns
   - Expected: Heal 10 HP per turn for 2 turns

4. **Fan of Knives**
   - Use AoE ability (25 stamina)
   - Should hit all enemies
   - Cooldown: 6 turns
   - Expected: Moderate AoE damage

### Priest Testing
**Abilities:** Holy Smite, Divine Shield, Healing Prayer, Wrath

**Test Cases:**
1. **Holy Smite**
   - Use offensive spell (15 mana)
   - Should deal 140% holy damage
   - Cooldown: 2 turns
   - Expected: Decent damage for support class

2. **Divine Shield**
   - Use to protect ally (20 mana)
   - Should prompt for target selection
   - Should apply Regeneration to target
   - Cooldown: 5 turns
   - Expected: Heal 7 HP per turn for 4 turns

3. **Healing Prayer**
   - Use mass heal (30 mana)
   - Should heal all party members
   - Cooldown: 4 turns
   - Expected: All allies gain Regeneration (12/turn × 3)

4. **Wrath**
   - Use offensive ultimate (35 mana)
   - Should damage all + Vulnerable
   - Cooldown: 7 turns
   - Expected: AoE damage + enemies take +30% damage

---

## 🐛 Known Issues to Watch For

### Potential Bugs
- [ ] Status effects not displaying
- [ ] Cooldowns not reducing
- [ ] Resources not consuming
- [ ] Damage not calculating correctly
- [ ] Abilities usable when they shouldn't be
- [ ] Status effects not expiring
- [ ] Target selection not working

### Edge Cases
- [ ] Using ability with exactly enough resources
- [ ] Using ability at 1 HP/Mana/Stamina
- [ ] Multiple status effects of same type
- [ ] Status effects expiring mid-turn
- [ ] Character death while status effect active
- [ ] Cooldown at 0 but still showing

---

## 📊 Performance Checks

### Response Time
- Combat menu should display instantly
- Ability usage should process in < 1 second
- Status effects should not cause lag
- Turn transitions should be smooth

### Visual Clarity
- Icons should be clear and meaningful
- Colors should enhance readability
- Status displays should not clutter screen
- Ability descriptions should be understandable

### Balance
- Abilities should feel powerful but not overpowered
- Cooldowns should create meaningful choices
- Resource costs should be significant but fair
- Status effects should be impactful

---

## 🎯 Success Criteria

### Must Pass
- ✅ All 16 abilities are usable
- ✅ Cooldowns work correctly
- ✅ Resources consumed properly
- ✅ Status effects apply and expire
- ✅ Damage calculated correctly
- ✅ No crashes or errors

### Should Pass
- ✅ Abilities feel strategic
- ✅ UI is intuitive
- ✅ Feedback is clear
- ✅ Combat is engaging
- ✅ Each class feels unique

### Nice to Have
- ✅ Abilities combo well together
- ✅ Status effects are visible and clear
- ✅ Combat feels "epic"
- ✅ Players use abilities frequently

---

## 📝 Bug Report Template

If you find bugs, document them like this:

```
Bug: [Short description]
Class: [Warrior/Mage/Rogue/Priest]
Ability: [Ability name]
Steps to Reproduce:
1. [Step 1]
2. [Step 2]
3. [Step 3]

Expected: [What should happen]
Actual: [What actually happened]
Severity: [Low/Medium/High/Critical]
```

---

## 🔄 Testing Workflow

### Initial Test (30 minutes)
1. Create one character of each class
2. Test each ability once
3. Verify basic functionality
4. Document any crashes

### Thorough Test (2 hours)
1. Test all abilities multiple times
2. Test edge cases
3. Test resource management
4. Test cooldown system
5. Test status effects
6. Test in different combats

### Balance Test (1 hour)
1. Use abilities in real scenarios
2. Note which feel too strong/weak
3. Check resource consumption rates
4. Evaluate cooldown lengths
5. Assess status effect durations

---

## 💡 Testing Tips

1. **Use Debug Mode** (if available)
   - Faster testing
   - Quick combat access
   - Resource cheats

2. **Test Systematically**
   - One class at a time
   - One ability at a time
   - Document as you go

3. **Test Edge Cases**
   - Use abilities with 1 resource
   - Use abilities at low HP
   - Use abilities repeatedly
   - Mix abilities with items

4. **Test UI Clarity**
   - Can you find abilities easily?
   - Are cooldowns obvious?
   - Do status icons make sense?
   - Is feedback clear?

5. **Test Balance**
   - Do you use abilities often?
   - Do basic attacks feel inferior?
   - Are resources manageable?
   - Does combat feel strategic?

---

## 📈 Expected Results

### After Testing
- All abilities confirmed working
- Bugs documented and prioritized
- Balance issues identified
- UI/UX feedback collected
- Ready for refinement phase

### Next Steps After Testing
1. Fix critical bugs
2. Balance pass on abilities
3. UI polish based on feedback
4. Implement turn order system
5. Begin combo system

---

## 🎮 Have Fun Testing!

Remember: This is a major new feature. Breaking things is expected and helpful!

**Report all issues, no matter how small.**

**Most Important Test:**
> "Does combat feel more fun and strategic than before?"

If YES → Step 1 is successful! ✅  
If NO → Document why and what would improve it.

---

**Last Updated:** 2025  
**Testing Phase:** Active  
**Feedback Needed:** YES - Document everything!

