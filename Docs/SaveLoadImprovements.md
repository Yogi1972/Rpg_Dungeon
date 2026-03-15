# Save/Load System Improvements

## Overview
Enhanced the save file selection experience at the title screen to show detailed information about available saves before loading.

## Changes Made

### 1. Program.cs - Enhanced Load Game Options

#### Case 2: Load Saved Game
- **Before**: Called `Options.ShowOptions()` directly
- **After**: 
  - Scans for all `save_*.json` files in current directory
  - Displays formatted list with:
    - File number for easy selection
    - File name
    - Date and time saved (formatted as yyyy-MM-dd HH:mm:ss)
    - Party information (size and average level)
  - User can select by number (1-N) or press 0 to cancel
  - Shows helpful error messages if no saves found
  - Calls new `LoadSpecificSaveFile()` method

#### Case 4: Load Multiplayer Save
- **Before**: Called `Options.ShowOptions()` directly
- **After**: Same enhanced UI as Case 2
  - Shows "LOAD MULTIPLAYER SAVED GAME" header
  - Same formatted save file list with metadata
  - Provides multiplayer-specific tips after loading

### 2. Added Helper Methods

#### `LoadSpecificSaveFile(string filePath)`
- Loads a specific save file by path
- Extracts save loading logic from Options.cs
- Returns `List<Character>?` (null if failed)
- Handles:
  - Character creation and stat restoration
  - Pet restoration (level, experience, loyalty)
  - Skill tree restoration (points and learned skills)
  - Inventory restoration (items, gold, slots)
  - Equipment restoration (all 6 slots with durability)
- Provides user-friendly error messages

#### `CreateEquipmentFromData(Options.ItemData data)`
- Helper to create Equipment objects from serialized data
- Restores durability correctly
- Used by LoadSpecificSaveFile for all equipment slots

### 3. Systems/Options.cs - Accessibility Changes

Changed save data classes from `private` to `internal`:
- `SaveFile`
- `CharacterData`
- `InventoryData`
- `ItemData`
- `PetData`
- `SkillTreeData`
- `LearnedSkillData`

This allows Program.cs to deserialize save files directly without going through Options menu.

### 4. Program.cs - Added Using Directives

Added required namespaces:
```csharp
using System.IO;
using System.Text;
using System.Text.Json;
```

## User Experience Improvements

### Before
```
Main Menu:
...
2) 💾 Load Saved Game

[User presses 2]

Options:
1) Set up camp
2) Save game (requires camp)
3) Load game
4) Exit game (requires camp)
5) Return
Choose an option: [User presses 3]

Enter save filename to load (or press enter to list saves):
[User presses Enter]

1) save_20240115_143022.json
2) save_20240116_092145.json
Choose save number to load:
```

### After
```
Main Menu:
...
2) 💾 Load Saved Game

[User presses 2]

╔════════════════════════════════════════════════════════════════╗
║                    LOAD SAVED GAME                             ║
╚════════════════════════════════════════════════════════════════╝

📁 Found 2 saved game(s):

  1) save_20240115_143022.json
      📅 2024-01-15 14:30:22 - 4 heroes, Avg Lv 8

  2) save_20240116_092145.json
      📅 2024-01-16 09:21:45 - 2 heroes, Avg Lv 12

0) Cancel and return to menu

Choose save file to load (1-2):
```

## Benefits

1. **Faster Access**: Direct save selection from main menu (removed intermediate Options screen)
2. **Better Information**: See party size and average level before loading
3. **Clearer Navigation**: Formatted headers and numbered options
4. **Error Handling**: Helpful messages when no saves exist
5. **Consistent UX**: Same experience for both single-player and multiplayer saves
6. **User-Friendly**: Easy cancellation option (0)
7. **Professional Look**: Box-drawing characters and emojis

## Technical Notes

- Save files are still stored as JSON in the working directory with `save_*.json` pattern
- No changes to save format or Options.cs save logic
- Load logic fully duplicated in Program.cs for direct access
- All error cases handled gracefully with user feedback
- Build successful with no warnings

## Testing Checklist

- [x] Build compiles successfully
- [ ] Load game from title screen (option 2)
- [ ] Load multiplayer save from title screen (option 4)
- [ ] Verify party info displays correctly
- [ ] Test with no save files present
- [ ] Test with corrupted save file
- [ ] Test cancel option (0)
- [ ] Verify loaded game runs correctly
- [ ] Check that old Options.cs load still works from in-game menu

## Future Enhancements

Potential improvements for future iterations:

1. Show character names in save file preview
2. Display last location/area played
3. Show total playtime
4. Add save file deletion option
5. Support for save file naming/descriptions
6. Cloud save integration
7. Auto-save feature with recovery
8. Save file backup/restore
