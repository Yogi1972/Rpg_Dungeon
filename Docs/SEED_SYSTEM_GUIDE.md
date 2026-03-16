# World Seed Quick Reference

## What's New? 🌍

Your RPG Dungeon Crawler now has a **dynamic world generation system**! No two games are the same unless you use the same seed.

## Seed System Basics

### When Starting a New Game
```
╔══════════════════════════════════════════════════════════════════╗
║                      WORLD SEED SELECTION                        ║
╚══════════════════════════════════════════════════════════════════╝

  🎲 World seeds determine the layout of your adventure!
  🗺️  Same seed = Same world (towns, dungeons, camps)
  ✨ Leave blank for a random world

Enter world seed (or press Enter for random): _
```

### Seed Input Options
1. **Leave Blank**: Auto-generates a random seed
2. **Hex Number**: `A3F2D8C1`, `DEADBEEF`, `00001234`
3. **Text String**: `MyWorld`, `ChallengeRun`, `PlayerName`
4. **Numeric**: `12345`, `987654321`

### Viewing Your Current Seed
During gameplay, select **Option 17 (View World Info)** from the main menu to see:
- Your current world seed (hex format)
- Numeric seed value
- Instructions on how to reuse the seed

## What Gets Generated?

### Towns & Settlements
- **6-9 Major Towns**: Trading hubs with full services
- **10-15 Settlements**: Smaller locations with inns/blacksmiths
- **18-25 Camps**: Rest stops across the world
- **18-22 Enemy Camps**: Hostile locations to assault

### Areas & Dungeons
- **8-12 Exploration Areas**: Forests, mountains, deserts, etc.
- **2-4 Dungeons per Area**: Each with unique layouts
- **2-3 Quest Spots per Area**: Points of interest

### Dungeon Layouts
- Each dungeon has its own seed (based on name + level)
- **5-8 rooms per floor**
- Branching paths and hallways
- Room types: Empty, Combat, Elite, Treasure, Boss, Stairs

## Example Seeds

### Beginner-Friendly Seeds
- `00000001` - Balanced world with gradual progression
- `EASY1234` - Converts to a balanced seed

### Challenge Seeds
- `HARDCORE` - Random, could be tough!
- `DEADBEEF` - Classic hex seed

### Custom Seeds
- `JohnAdventure` - Personalized seed
- `2024Game` - Date-based seed
- `PartyOf4` - Theme-based seed

## Sharing Seeds

### Format for Sharing
```
World Seed: A3F2D8C1
- 8 Major Towns
- 13 Settlements  
- 22 Camps
- Starting Town: Havenbrook
```

### Multiplayer
Both players must use the **same seed** when starting a multiplayer game to ensure identical worlds.

## Technical Notes

### Seed Consistency
- ✅ Same seed = Same world layout
- ✅ Same seed = Same dungeon layouts
- ✅ Same seed = Same town locations
- ❌ Different seeds = Completely different worlds

### Seed Generation
- Random seeds use system timestamp
- Text seeds are hashed to numeric values
- Hex seeds are parsed directly
- All seeds are displayed in 8-digit hex format

## Tips & Tricks

1. **Screenshot Your Seed**: Keep a record of great worlds
2. **Test Seeds**: Try different seeds to find your favorite layout
3. **Community Seeds**: Share interesting seeds with friends
4. **Challenge Runs**: Compete using the same seed
5. **Speedruns**: Optimize routes on known seed layouts

## Future Features (Planned)
- 🔮 Seed presets (Easy, Medium, Hard, Chaotic)
- 📊 Seed statistics and analysis
- 🏆 Community seed leaderboards
- 💾 Seed saved with game saves
- 🎯 Seed modifiers (world size, density, difficulty)

---

**Enjoy your dynamically generated adventures!** 🎮⚔️🗺️
