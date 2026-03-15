# Encounter System Documentation

## Overview

The `Encounter` class is a smart combat encounter system that automatically scales enemy difficulty and numbers based on your party composition. It makes combat more fun and balanced by analyzing party size, level, and power.

## Key Features

### 🎯 Intelligent Scaling
- **Party Size Awareness**: Adjusts enemy count based on 1-4 party members
- **Level Matching**: Scales enemy levels to party average
- **Power Analysis**: Considers stats and equipment when balancing

### 📊 Difficulty Levels

| Difficulty | Enemy Count | Enemy Level | Best For |
|------------|-------------|-------------|----------|
| **Easy** | Party - 1 | -1 to -2 levels | Warming up, low on health |
| **Normal** | ~Party size | ±1 level | Standard exploration |
| **Hard** | Party + 1-2 | +1 to +2 levels | Challenge seekers |
| **Elite** | Party / 2 | +2 to +4 levels | Miniboss fights |
| **Boss** | 1 Boss + minions | +3 to +5 levels | Floor/dungeon bosses |

## Usage Examples

### Example 1: Quick Encounter (One-Liner)
```csharp
// Generate and execute a normal encounter instantly
bool victory = Encounter.QuickEncounter(party, EncounterDifficulty.Normal);
```

### Example 2: Custom Encounter with Control
```csharp
// Create encounter with more control
var encounter = new Encounter();
encounter.GenerateEncounter(party, EncounterDifficulty.Hard);
bool victory = encounter.StartEncounter(party);
```

### Example 3: Boss Encounter
```csharp
// Generate an epic boss fight
var encounter = new Encounter();
encounter.GenerateBossEncounter(party, "Ancient Dragon Lord");
bool victory = encounter.StartEncounter(party);
```

### Example 4: Random Encounter (For Exploration)
```csharp
// Random difficulty (60% normal, 25% easy, 15% hard)
bool victory = Encounter.RandomEncounter(party, areaLevel);
```

## Party Size Scaling Examples

### Solo Player (Party of 1)
```
Easy:    1 enemy (level - 1)
Normal:  1 enemy (same level)
Hard:    2 enemies (level + 1)
Elite:   1 enemy (level + 3)
Boss:    1 boss + 1 minion
```

### Small Party (Party of 2)
```
Easy:    1 enemy
Normal:  2 enemies
Hard:    3 enemies
Elite:   1 enemy (very strong)
Boss:    1 boss + 1 minion
```

### Full Party (Party of 4)
```
Easy:    3 enemies
Normal:  4 enemies
Hard:    5-6 enemies
Elite:   2 enemies (very strong)
Boss:    1 boss + 2 minions
```

## Integration with Existing Systems

### Works With:
- ✅ Existing `Combat` class and combat loop
- ✅ `Mob` and `MobFactory` for enemy generation
- ✅ `Character` stats and equipment system
- ✅ Pet abilities and bonuses
- ✅ Skill tree bonuses
- ✅ Loot distribution system
- ✅ XP and leveling system

### In Dungeons
```csharp
private bool HandleRoomEncounter(List<Character> party, RoomType roomType)
{
    var encounter = new Encounter();
    
    switch (roomType)
    {
        case RoomType.Combat:
            encounter.GenerateEncounter(party, EncounterDifficulty.Normal);
            break;
        case RoomType.Elite:
            encounter.GenerateEncounter(party, EncounterDifficulty.Elite);
            break;
        case RoomType.Boss:
            encounter.GenerateBossEncounter(party);
            break;
        default:
            return true;
    }
    
    return encounter.StartEncounter(party);
}
```

### In World Areas
```csharp
// Random encounters while traveling
if (Random.Next(100) < encounterChance)
{
    Encounter.RandomEncounter(party, area.RecommendedLevel);
}
```

### In Quest Events
```csharp
// Scripted quest encounter
var encounter = new Encounter();
encounter.GenerateEncounter(party, quest.Difficulty);
bool victory = encounter.StartEncounter(party);
```

## Properties and Methods

### Properties
- `Enemies` - List of mobs in the encounter
- `Difficulty` - The difficulty level of the encounter
- `EncounterDescription` - Flavor text describing the encounter
- `TotalEnemyCount` - Number of enemies
- `IsActive` - Whether encounter has active enemies

### Key Methods

#### GenerateEncounter
```csharp
encounter.GenerateEncounter(party, EncounterDifficulty.Normal);
```
Generates a balanced encounter with random enemies.

#### GenerateBossEncounter
```csharp
encounter.GenerateBossEncounter(party, "Dragon King");
```
Generates a boss fight with one powerful boss and minions.

#### StartEncounter
```csharp
bool victory = encounter.StartEncounter(party);
```
Executes the combat encounter. Returns true if party wins.

#### QuickEncounter (Static)
```csharp
Encounter.QuickEncounter(party, EncounterDifficulty.Hard);
```
One-line method to generate and execute an encounter.

#### RandomEncounter (Static)
```csharp
Encounter.RandomEncounter(party, areaLevel);
```
Generates a random difficulty encounter suitable for exploration.

## Testing the System

In the main game menu, select option **14) Test Encounter System** to try different encounter types and see how they scale with your party!

## Design Philosophy

The Encounter class follows these principles:

1. **Fun First**: Ensures combat is challenging but fair
2. **Automatic Balancing**: No manual tweaking needed
3. **Party Aware**: Scales perfectly for solo or group play
4. **Variety**: Mixes enemy counts and levels for variety
5. **Integration**: Works seamlessly with existing combat

## Tips for Best Results

- Use **Easy** encounters when party is wounded
- Use **Normal** for standard dungeon rooms
- Use **Hard** for special rooms or challenges
- Use **Elite** for miniboss encounters
- Use **Boss** for floor/dungeon final battles
- Use **RandomEncounter** for world exploration

## Future Enhancements

Potential improvements:
- Enemy type preferences (undead, beasts, humanoids)
- Environmental modifiers (weather, time of day)
- Ambush mechanics (enemies attack first)
- Wave-based encounters
- Tactical positioning
- Combo attacks from grouped enemies

---

**Created for:** RPG Dungeon Crawler
**Compatible with:** .NET 10, C# 14.0
