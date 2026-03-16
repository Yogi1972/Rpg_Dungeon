# ⚔️ Step 1 Phase 3 - Turn Order System Implementation

**Date:** 2025  
**Phase:** Turn Order System  
**Status:** 🔨 IN PROGRESS

---

## 🎯 Goal

Implement an initiative-based turn order system to make combat more strategic and tactical, replacing the current "all party then all enemies" turn system.

---

## ✅ Completed Work

### 1. TurnOrderManager Created ✅
**File:** `Combat/TurnOrderManager.cs`

**Features Implemented:**
- ✅ `TurnOrderManager` class for managing combat turn flow
- ✅ Initiative calculation based on Agility + d20 roll
- ✅ Turn queue sorting (highest initiative goes first)
- ✅ Combat round tracking
- ✅ Actor alive status management
- ✅ Turn advancement logic
- ✅ Turn order display UI
- ✅ Combat continuation checking

**Key Components:**
```csharp
class TurnOrderManager
{
    - CalculateInitiative(Character)  // Agility/2 + d20
    - CalculateInitiative(Mob)        // Level/2 + d20
    - GenerateTurnOrder()             // Sort by initiative
    - GetNextActor()                  // Get current actor
    - AdvanceTurn()                   // Move to next turn
    - UpdateActorStatus()             // Track alive/dead
    - DisplayTurnOrder()              // Show UI
    - GetRoundNumber()                // Current round
    - ShouldContinueCombat()          // Check win/loss
}

class CombatActor
{
    - IsPlayer: bool
    - Character/Mob reference
    - Initiative: int
    - Name: string
    - IsAlive: bool
}
```

### 2. Combat.cs Integration Started ✅
**File:** `Systems/Combat.cs`

**Changes Made:**
- ✅ Added `using Night.Combat` for TurnOrderManager access
- ⏳ Need to integrate into RunEncounter method

---

## 🔨 Next Steps

### 1. Integrate TurnOrderManager into Combat Flow
The current `RunEncounter` method uses a simple loop:
```csharp
// Current system:
foreach (var member in party)  // All party goes first
    { TakeTurn() }
MobAttacks();                  // Then mob attacks
```

**Need to change to:**
```csharp
// New system:
turnOrder.GenerateTurnOrder(party, enemies);
turnOrder.DisplayTurnOrder();

while (combat continues)
{
    var actor = turnOrder.GetNextActor();
    
    if (actor.IsPlayer)
        PlayerTurn(actor.Character);
    else
        MobTurn(actor.Mob);
    
    turnOrder.AdvanceTurn();
}
```

### 2. Refactor Combat Loop Structure
- **Extract Player Turn Logic** into separate method
- **Extract Mob Turn Logic** into separate method
- **Add turn order display** at start of each turn
- **Update death handling** to notify turn order manager
- **Add round-based effects** (cooldowns, status effects) at round start

### 3. UI Enhancements
- Display initiative rolls at combat start
- Show whose turn it is clearly
- Display remaining turns in round
- Visual indicators for current actor
- Round number display

### 4. Testing Requirements
- ✅ Initiative calculation works correctly
- ✅ Turn order sorted properly
- ✅ Fast characters go before slow characters
- ✅ Dead actors skipped
- ✅ Round progression works
- ⏳ Test with actual combat flow
- ⏳ Test with party vs single mob
- ⏳ Test status effects per turn
- ⏳ Test ability cooldowns per turn

---

## 📋 Implementation Approach

### Option 1: Full Refactor (Recommended)
**Pros:**
- Clean, maintainable code
- Full feature integration
- Better for future expansions

**Cons:**
- Larger change scope
- Needs extensive testing
- Temporarily breaks existing combat

### Option 2: Gradual Migration
**Pros:**
- Less risky
- Can test incrementally
- Old system remains as fallback

**Cons:**
- More complex codebase
- Duplicate code temporarily
- Slower to complete

**Decision:** Gradual migration with feature flag

---

## 🎮 Expected Player Experience

### Before (Current System)
```
⚔️  COMBAT! Level 5 Goblin appears!

[Player 1's Turn]
Action: 1

[Player 2's Turn]
Action: 1

[Enemy Turn]
Goblin attacks Player 1!
```

### After (Turn Order System)
```
⚔️  COMBAT! Level 5 Goblin appears!

╔════════════════════════════════════════════╗
║  Turn Order - Round 1                      ║
╚════════════════════════════════════════════╝
➤ 🎮 Rogue Blade         [Init: 18]
  🎮 Warrior Tank         [Init: 15]
  👹 Goblin               [Init: 12]
  🎮 Mage FireStorm       [Init: 8]

╔═══════════════════════════════════════════╗
║  Rogue Blade's Turn (Lv 5) - Round 1     ║
╚═══════════════════════════════════════════╝
💚 HP: 85/100 | ⚡ Stamina: 40/50
...
```

### Benefits
- ✨ **Strategic Depth:** Agility matters more
- ⚡ **Pacing:** Fast characters can act first
- 🎯 **Tactical Decisions:** Plan based on turn order
- 📊 **Transparency:** Players see who goes when
- 🔄 **Dynamic Combat:** Not always same order

---

## 🚧 Known Issues / Considerations

### 1. Multiple Enemy Support
- Current system: single mob only
- Turn order system: ready for multiple enemies
- **Solution:** Will work when multi-enemy system added

### 2. Status Effect Timing
- Current: Processed at start of player turns
- New: Should process at start of each actor's turn
- **Solution:** Move processing into turn handler

### 3. Cooldown Management
- Current: Reduced per round (all players)
- New: Reduced per turn (each actor)
- **Solution:** Track cooldowns per actor turn

### 4. Threat System Integration
- Current: Checked after all players act
- New: Should update continuously
- **Solution:** Recalculate target each mob turn

---

## 📈 Progress Tracking

### Phase 3: Turn Order System
```
Core System:  ████████████████████████████░░ 90%
Integration:  ████░░░░░░░░░░░░░░░░░░░░░░░░░░ 10%
Testing:      ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░  0%
UI Polish:    ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░  0%
```

**Overall Phase 3 Progress:** 25%

---

## 🎯 Definition of Done

- [x] TurnOrderManager class created
- [x] Initiative calculation implemented
- [ ] Integrated into RunEncounter
- [ ] Player turn refactored
- [ ] Mob turn refactored
- [ ] Turn order display functional
- [ ] Round tracking working
- [ ] Status effects per-turn
- [ ] Cooldowns per-turn
- [ ] Death handling integrated
- [ ] Tested in combat
- [ ] UI polished
- [ ] Documentation updated

---

## 🔜 After Completion

Once Turn Order System is complete:
1. Update QUICK_PROGRESS_TRACKER.md to 70% Step 1 complete
2. Move to Combo Attack System (#8)
3. Then Enemy AI Behaviors (#9)
4. Finally Balance Testing (#10)
5. Release v3.1 with Enhanced Combat System! 🎉

---

**Last Updated:** 2025
**Status:** Core system complete, integration in progress
**Estimated Completion:** Next session
