# Dynamic World Generation System

## Overview
The game now features a **procedurally generated world system** with **seed-based generation**, replacing the static world. Each new game generates a unique world layout while allowing players to recreate specific worlds using seeds.

## Key Features

### 🌍 World Seed System
- **Auto-Generated Seeds**: Every new game automatically generates a unique seed
- **Custom Seeds**: Players can enter their own seed for specific world layouts
- **Seed Sharing**: Share seeds with friends to play in the same world
- **Seed Format**: Seeds are displayed in hexadecimal format (e.g., `A3F2D8C1`)

### 🏗️ Dynamic World Generation

#### Major Towns (6-9 towns per world)
- Randomly selected from 12 possible town templates
- Level requirements vary slightly per generation
- Always includes a starting town (level 1)
- Towns are ordered by difficulty level

#### Settlements (10-15 settlements)
- Medium-sized locations with varied services
- Randomized inn and blacksmith availability
- Level requirements adjusted dynamically

#### Camps (18-25 camps)
- Small rest stops across the world
- Multiple types: Roadside, Forest, Mountain, Desert, Riverside, Ruins
- Randomly selected from 29 possible camp templates

#### Enemy Camps (18-22 camps)
- Hostile locations to assault
- Difficulty tiers: Weak, Normal, Strong, Dangerous, Deadly, Elite
- Varied camp types: Goblin, Bandit, Undead, Orc, Cultist, Demon, Dragon, Beast, Spider, Elemental

### 🗺️ Dynamic Area System (8-12 areas)
- Exploration areas selected from 15 possible templates
- Each area contains:
  - **2-4 procedurally generated dungeons** with unique names
  - **2-3 quest spots** with varied objectives
  - Dynamic level requirements (±2 levels from base)

### 🏰 Procedural Dungeon Generation
- Each dungeon has a unique seed based on its name and level
- **Dynamic floor generation** (5-8 rooms per floor)
- **Branching paths** and optional hallways
- **Room variety**: Empty, Combat, Elite, Treasure, Boss, Stairs
- **Atmospheric descriptions** vary per room type
- Same dungeon = Same layout (consistent seeds)

## How to Use

### Starting a New Game with Seeds

1. **Random World (Default)**:
   ```
   New Game → Press Enter at seed prompt → Random world generated
   ```

2. **Custom Seed**:
   ```
   New Game → Enter seed (e.g., "A3F2D8C1" or "MyWorld") → World generated
   ```

3. **View Current Seed**:
   ```
   Main Menu → Option 17 (View World Info) → See your world seed
   ```

### Seed Examples
- `00000001` - Simple seed
- `DEADBEEF` - Hexadecimal seed
- `MyAdventure` - Text seed (converted to numeric)
- `12345` - Numeric seed

## Technical Implementation

### Classes Added
1. **WorldGenerator.cs** (`World/`)
   - Generates major towns, settlements, camps, and enemy camps
   - Creates areas with dungeons and quest spots
   - Calculates distances between locations
   - Seed-based random number generation

2. **DungeonGenerator.cs** (`World/`)
   - Procedural dungeon floor generation
   - Room type determination
   - Room connection and branching
   - Dynamic descriptions for rooms and hallways

### Classes Modified
1. **GameState** (Location.cs)
   - Added `WorldSeed` property

2. **GameInitializer.cs**
   - Added seed prompt for new games
   - Added `PromptForWorldSeed()` method
   - Both single-player and multiplayer support seeds

3. **GameLoopManager.cs**
   - Added world seed parameter to `Run()` method
   - Added `DisplayWorldInfo()` method
   - Added menu option 17 for world info

4. **WorldMap.cs**
   - Uses WorldGenerator for dynamic world creation
   - Removed static initialization methods
   - Constructor accepts optional seed parameter

5. **Map.cs**
   - Uses WorldGenerator for dynamic area creation
   - Removed static area initialization
   - Constructor accepts optional seed parameter

6. **Dungeon.cs**
   - Uses DungeonGenerator for floor generation
   - Accepts seed parameter in constructor
   - Delegated description generation to generator

7. **Area.cs**
   - Updated `SelectAndExploreDungeon()` to pass dungeon seed

8. **DungeonLocation** (Area.cs)
   - Added `Seed` property
   - Generates consistent seed from name and level

## Benefits

### For Players
- ✅ **Infinite Replayability**: Every game is different
- ✅ **Seed Sharing**: Play the same world as friends
- ✅ **Challenge Runs**: Race friends on identical worlds
- ✅ **Favorite Worlds**: Save seeds of worlds you enjoyed

### For Development
- ✅ **Deterministic**: Same seed = Same world (reproducible bugs)
- ✅ **Scalable**: Easy to add more templates
- ✅ **Maintainable**: Centralized generation logic
- ✅ **Testable**: Test specific seeds for balance

## Future Enhancements
- Save/load system integration with seeds
- Seed-based quest generation
- Seed-based NPC placement
- World generation options (world size, difficulty modifiers)
- Seed leaderboards and community challenges
