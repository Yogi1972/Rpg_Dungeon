# Enhanced Loot System Documentation

## Overview
The enemy camp loot system has been upgraded from a simple static system to a dynamic, engaging loot system with multiple tiers and camp-specific items.

## Key Improvements

### 1. **Rarity System**
Loot now comes in five rarity tiers with visual indicators:
- ⚪ **Common** (60% base chance) - Basic items and materials
- 🟢 **Uncommon** (25% base chance) - Improved gear and valuables
- 🔵 **Rare** (12% base chance) - Powerful equipment and rare materials
- 🟣 **Epic** (3% base chance) - Elite gear with strong bonuses
- 🟠 **Legendary** (<1%, boss only) - Unique, powerful items

### 2. **Camp-Specific Loot Tables**
Each camp type now has unique, thematic items:

#### Bandit Hideout
- Stolen goods, lockpicks, concealed armor
- Boss loot: Bandit Lord's Blade

#### Goblin Warcamp
- Scrap metal, crude weapons, tribal gear
- Boss loot: Goblin King's Crown

#### Orc Stronghold
- Orcish iron, heavy weapons, spiked armor
- Boss loot: Orcish Doom Blade

#### Undead Graveyard
- Bones, grave dust, spectral essence, cursed items
- Boss loot: Lich's Phylactery Fragment

#### Beast Den
- Pelts, claws, fangs, primal armor
- Boss loot: Savage Fang Necklace

#### Cultist Shrine
- Dark candles, ritual items, forbidden tomes
- Boss loot: High Priest's Staff of Shadows

#### Dragon Lair
- Dragon scales, teeth, claws, dragonhide
- Boss loot: Dragonforged Sword

#### Demon Portal
- Demonic ash, sulfur, hellfire shards
- Boss loot: Demon Commander's Insignia

#### Elemental Nexus
- Elemental crystals, elemental essences, mana stones
- Boss loot: Nexus Core

#### Spider Nest
- Spider silk, poison glands, chitin, webweave armor
- Boss loot: Brood Mother's Carapace

### 3. **Multi-Source Loot Distribution**

#### Wave Loot
- Each cleared wave has a 30% chance to grant a loot roll
- Loot rolls accumulate throughout the assault
- Final distribution happens after boss is defeated

#### Boss Loot
- Guaranteed 2-4 items with improved rarity chances:
  - 5% Legendary
  - 15% Epic (vs 3% normally)
  - 25% Rare (vs 12% normally)
  - 30% Uncommon
  - 25% Common

### 4. **Difficulty Scaling**
Equipment stats scale with camp difficulty:
- Weak: +0 bonus
- Normal: +1 bonus
- Strong: +2 bonus
- Dangerous: +3 bonus
- Deadly: +4 bonus
- Elite: +5 bonus

### 5. **Smart Item Distribution**
- Items distributed to party members using round-robin
- Visual rarity indicators show item quality
- Clear feedback for items that couldn't be carried
- Equipment items now have meaningful stat bonuses

### 6. **Enhanced Cleared Camp Searching**
- Can still find items when searching cleared camps
- Uses same loot table for consistency
- Lower chance (25%) to balance rewards

## Technical Implementation

### New Files
- `Combat\LootTable.cs` - Contains the new loot generation system
  - `LootRarity` enum - Defines item rarity tiers
  - `LootEntry` class - Encapsulates loot items with rarity and factory
  - `CampLootTable` class - Generates camp-specific loot dynamically

### Modified Files
- `World\EnemyCamp.cs` - Updated to use new loot system
  - Added `_lootTable` field for loot generation
  - Added `_totalLootRolls` to track wave loot accumulation
  - Completely rewrote `DistributeRewards()` for dynamic loot
  - Enhanced `SearchForLoot()` to use loot tables
  - Added `DetermineItemRarity()` helper method
  - Updated UI text to reflect new loot system

## Benefits
1. **More Variety** - Camp-specific items make each location feel unique
2. **Excitement** - Rarity system creates anticipation for rare drops
3. **Progression** - Difficulty scaling ensures rewards remain relevant
4. **Replayability** - Dynamic generation makes each run different
5. **Immersion** - Thematic items match camp types
6. **Balance** - Boss fights now guaranteed to reward players appropriately

## Future Enhancement Ideas
- Add set items that provide bonuses when multiple pieces are equipped
- Implement luck/magic find stats that increase rare drop chances
- Add salvage system to break down unwanted loot for materials
- Create legendary weapons with unique names and special abilities
- Add crafting recipes that require camp-specific materials
