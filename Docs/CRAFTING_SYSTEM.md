# 🛠️ CRAFTING & RESOURCE SYSTEM DOCUMENTATION

## Overview
The RPG now features a comprehensive crafting and resource gathering system with profession specializations, terrain-based resource nodes, and cross-profession crafting recipes.

---

## 🎓 CRAFTING PROFESSIONS

### Profession Selection
- Each character chooses **2 crafting professions** during character creation
- Professions unlock specific recipes and gathering abilities
- Mix and match for versatility or synergy

### Available Professions

#### 🔨 **BLACKSMITHING** (Production)
- **Crafts:** Metal weapons, heavy armor, shields, components
- **Requires:** Iron Ore, Mithril Ore, Obsidian from mountains/volcanic regions
- **Best For:** Warriors, tanky builds
- **Key Recipes:** Plate armor, greatswords, shields

#### 🧵 **LEATHERWORKING** (Production)
- **Crafts:** Light armor, leather accessories, bags, storage
- **Requires:** Wolf Pelt, Deer Hide, Fish Scales from forests/water/plains
- **Best For:** Rogues, agility-based characters
- **Key Recipes:** Studded leather, ranger armor, backpacks

#### ⚗️ **ALCHEMY** (Production)
- **Crafts:** Potions, elixirs, combat buffs, reagents
- **Requires:** Herbs, flowers, mushrooms from all terrains
- **Best For:** Mages, Priests (get bonus without profession!)
- **Key Recipes:** Healing/mana/stamina potions, combat buffs
- **Note:** Mages and Priests can use Alchemy without having the profession

#### ✨ **ENCHANTING** (Production)
- **Crafts:** Magical equipment, enhanced weapons, staffs
- **Requires:** Crystals, gems, arcane materials from mountains/desert
- **Best For:** Casters, hybrid builds
- **Key Recipes:** Enchanted armor, magical staffs, mystic essence

#### 💎 **JEWELCRAFTING** (Production)
- **Crafts:** Rings, necklaces, amulets, cut gems
- **Requires:** Gems, ores, crystals from mountains/water/desert
- **Best For:** All classes (stat-boosting jewelry)
- **Key Recipes:** Rings, amulets, elemental jewelry

#### 🌿 **HERBALISM** (Gathering)
- **Gathers:** Healing herbs, mana flowers, mushrooms, plant materials
- **From:** Forests (best), plains, deserts
- **Synergy With:** Alchemy (perfect pair!)
- **Benefits:** Gather ingredients for potions and reagents

---

## 🌍 RESOURCE GATHERING BY TERRAIN

### ⛰️ **MOUNTAINS** (Ironforge, Stormwatch regions)
**Common Resources (Anyone):**
- Iron Ore (65% success) - 1-3 units
- Silver Ore (40%) - 1-2 units
- Gold Nugget (20%) - 1 unit

**Profession Resources:**
- 🔨 Mithril Ore (Blacksmithing, 30%) - Premium metal
- 💎 Small Gem (Jewelcrafting, 35%) - For jewelry
- 💎 Large Gem (Jewelcrafting, 15%) - Rare gems
- ✨ Crystal Shard (Enchanting, 25%) - Magical crystals

**Gathering Tips:**
- Best for blacksmiths and jewelcrafters
- High-value ores and gems
- Mountain camps ideal for extended mining

---

### 🌲 **FORESTS** (Shadowkeep, Western regions)
**Common Resources:**
- Wood (75%) - 2-4 units - Universal crafting material
- Spider Silk (35%) - 1-2 units - Magical fabric

**Profession Resources:**
- 🌿 Healing Herb (Herbalism, 60%) - Alchemy ingredient
- 🌿 Mana Flower (Herbalism, 45%) - Mana potions
- 🌿 Mushroom Cap (Herbalism, 55%) - Combat potions
- 🌿 Rootvine (Herbalism, 40%) - Reagent material
- 🧵 Wolf Pelt (Leatherworking, 50%) - Leather armor
- 🧵 Deer Hide (Leatherworking, 40%) - Light armor

**Gathering Tips:**
- Paradise for herbalists and leatherworkers
- Abundant basic materials
- Best terrain for balanced gathering

---

### 🏜️ **DESERT** (Sunspire region)
**Common Resources:**
- Sandstone (70%) - 2-4 units - Building material

**Profession Resources:**
- 🌿 Cactus Flesh (Herbalism, 55%) - Water/stamina potions
- 🌿 Dried Herbs (Herbalism, 50%) - Preserved herbs
- ✨ Ancient Tablet (Enchanting, 15%) - Rare magical texts
- ✨ Sun Crystal (Jewelcrafting, 25%) - Lightning-element gems
- 🧵 Desert Chitin (Leatherworking, 45%) - Exotic armor material
- ⚗️ Scorpion Venom (Alchemy, 35%) - Agility potions

**Gathering Tips:**
- Unique exotic materials
- Best for enchanters seeking ancient knowledge
- Rare crystals and tablets

---

### 🌋 **VOLCANIC** (Emberpeak region)
**Common Resources:**
- Lava Rock (70%) - 2-3 units - Heat-resistant material

**Profession Resources:**
- 🔨 Obsidian (Blacksmithing, 55%) - Volcanic glass weapons
- ⚗️ Sulfur (Alchemy, 60%) - Explosive reagent
- ⚗️ Flame Essence (Alchemy, 25%) - Fire magic essence
- ⚗️ Ash Powder (Alchemy, 50%) - Crafting component
- ✨ Fire Crystal (Enchanting, 30%) - Fire enchantments
- 💎 Volcanic Glass (Jewelcrafting, 35%) - Dark jewelry

**Gathering Tips:**
- Fire-element specialization
- Obsidian gear is powerful
- Dangerous but rewarding

---

### 🌊 **WATER/COASTAL** (Mysthaven, Crystalshore)
**Common Resources:**
- Coral Fragment (45%) - 1-2 units - Decorative material

**Profession Resources:**
- 🌿 Seaweed (Herbalism, 65%) - Water plants
- 🌿 Kelp Strand (Herbalism, 60%) - Binding material
- 💎 Pearl (Jewelcrafting, 20%) - Precious gems
- ⚗️ Saltpeter (Alchemy, 50%) - Explosive powder
- ✨ Aqua Crystal (Enchanting, 30%) - Water/ice magic
- 🧵 Fish Scales (Leatherworking, 55%) - Scaled armor

**Gathering Tips:**
- Water-element resources
- Pearls are valuable but rare
- Great for magical crafting

---

### 🌾 **PLAINS** (Central regions, Havenbrook)
**Common Resources:**
- Cotton (60%) - 1-3 units - Fabric material
- Iron Ore (35%) - 1-2 units - Basic metal
- Copper Ore (45%) - 1-2 units - Common metal
- Clay (55%) - 1-3 units - Pottery material

**Profession Resources:**
- 🌿 Wildflowers (Herbalism, 70%) - Common herbs
- 🌿 Wheat Stalk (Herbalism, 65%) - Food/reagent
- 🧵 Rabbit Pelt (Leatherworking, 50%) - Small hides

**Gathering Tips:**
- Beginner-friendly resources
- High success rates
- Good for basic crafting

---

## 🔍 RESOURCE GATHERING MECHANICS

### Success Rate Calculation
```
Final Success Rate = (Base Rate + Agility Bonus) × Weather Modifier
- Base Rate: Resource-specific (15-75%)
- Agility Bonus: +1% per Agility point
- Weather Modifier: 0.5x (storm) to 1.2x (clear)
- Min: 5% | Max: 95%
```

### Weather Effects on Gathering
- ☀️ **Clear:** +20% success (1.2x multiplier)
- 🌧️ **Rainy:** -20% success (0.8x)
- ⛈️ **Stormy:** -50% success (0.5x)
- 🌫️ **Foggy:** -10% success (0.9x)
- ❄️ **Snowy:** -30% success (0.7x)
- 💨 **Windy:** -10% success (0.9x)

### Profession Benefits
- **With Profession:** Can gather profession-specific rare resources
- **Without Profession:** Limited to common resources only
- **Herbalism:** Unlocks all herb/plant gathering in any terrain
- **Gathering Professions:** Increase yields and access to rare nodes

---

## 🎨 CRAFTING RECIPES

### Cross-Profession Crafting
Many advanced recipes require materials from multiple professions:

#### Example: **Ranger's Armor** (Leatherworking)
- 4x Wolf Pelt (Leatherworking gathering)
- 2x Spider Silk (Common from forests)
- 1x Steel Chain (Blacksmithing component)
**Result:** Agi +8, Str +3, HP +25

#### Example: **Staff of the Archmage** (Enchanting)
- 1x Mithril Ore (Blacksmithing material)
- 5x Arcane Dust (Alchemy reagent)
- 2x Large Gem (Jewelcrafting material)
- 1x Ancient Tablet (Enchanting gathering)
**Result:** Intel +15, Mana +45, All Stats +3

#### Example: **Scaled Armor** (Leatherworking)
- 5x Fish Scales (Leatherworking from water terrain)
- 2x Wolf Pelt (Leatherworking from forests)
- 1x Aqua Crystal (Enchanting from water terrain)
**Result:** Agi +7, HP +30, Armor +5

### Material Processing Chains

#### **Magic Dust Chain** (Alchemy):
1. Gather: Mana Flower + Mushroom Cap (Herbalism)
2. Process: Magic Dust (makes 2x)
3. Upgrade: + Crystal Shard + Aqua Crystal → Arcane Dust
4. Use: Enchanting recipes, potion crafting

#### **Metal Component Chain** (Blacksmithing):
1. Gather: Iron Ore (Mountains)
2. Process: Iron Nails, Steel Chain, Metal Clasp
3. Use: Leatherworking armor, crossover crafting

#### **Gem Refinement Chain** (Jewelcrafting):
1. Gather: Small Gem, Large Gem (Mountains)
2. Cut: Polished Gem, Cut Large Gem
3. Enchant: + Arcane Dust → Enchanted Gem
4. Use: High-end jewelry and enchantments

---

## 💼 RECOMMENDED PROFESSION COMBINATIONS

### 🥇 **Best Starter Combos:**
1. **🌿 Herbalism + ⚗️ Alchemy** - Self-sufficient potion maker
2. **🔨 Blacksmithing + 💎 Jewelcrafting** - Gear and accessories
3. **🧵 Leatherworking + 🌿 Herbalism** - Leather gear and herbs

### 🏆 **Advanced Combos:**
1. **⚗️ Alchemy + ✨ Enchanting** - Magical powerhouse (reagents + enchantments)
2. **🔨 Blacksmithing + 🧵 Leatherworking** - Complete armor coverage
3. **💎 Jewelcrafting + ✨ Enchanting** - Luxury magical items

### 👥 **Party Synergy:**
- **4-Person Party Ideal Split:**
  - Character 1: Blacksmithing + Herbalism
  - Character 2: Leatherworking + Alchemy
  - Character 3: Enchanting + Jewelcrafting
  - Character 4: Alchemy + Herbalism
- **Covers:** All professions with overlap in gathering/production

---

## 🎯 CRAFTING STATIONS

### 🏕️ **Camp Crafting Menu**
1. Rest
2. Forage (legacy system - random items)
3. **Search for Resources** - NEW! Terrain-specific gathering
4. **Crafting Workshop** - NEW! Unified crafting hub
5. View party
6. Options

### 🛠️ **Crafting Workshop Sub-Menus**
- **Blacksmithing Forge:** Weapons, armor, shields, components
- **Leatherworking Station:** Light armor, accessories, bags
- **Alchemy Lab:** Potions, elixirs, reagents
- **Enchanting Table:** Magical enhancements, staffs
- **Jewelcrafting Bench:** Gems, rings, necklaces, amulets
- **Simple Crafting:** Basic items (torches, bandages, rope)
- **Recipe Book:** View all available recipes
- **Party Professions:** Check everyone's professions

---

## 📊 CRAFTING STATISTICS

### Total Recipes by Profession
- 🔨 **Blacksmithing:** 20+ recipes
- 🧵 **Leatherworking:** 18+ recipes
- ⚗️ **Alchemy:** 25+ recipes
- ✨ **Enchanting:** 15+ recipes
- 💎 **Jewelcrafting:** 16+ recipes
- 🔥 **Simple Crafting:** 5 recipes (no profession required)

### Resource Types
- **Total Unique Resources:** 50+ different materials
- **Common Resources:** 15 (available without professions)
- **Profession Resources:** 35+ (require specific professions)
- **Crafting Components:** 15+ intermediate materials

### Recipe Complexity Tiers
- **Tier 1 (Simple):** 1-2 materials, common resources
- **Tier 2 (Moderate):** 3-4 materials, mix of common + profession
- **Tier 3 (Advanced):** 5-6 materials, mostly profession-specific
- **Tier 4 (Master):** 7+ materials, cross-profession requirements, processed materials

---

## 🎮 GAMEPLAY INTEGRATION

### Character Creation Flow
1. Choose Name
2. Select Race → Apply racial bonuses
3. Select Class → Set base stats
4. **NEW:** Select 2 Crafting Professions
5. Receive starting gold
6. Begin adventure!

### Resource Gathering Flow
1. Set up camp in any location
2. Choose "Search for Resources"
3. System determines terrain from location
4. View available resources for that terrain
5. Select party member to gather
6. Success based on: Agility + Weather + Profession
7. Collect multiple resources per search

### Crafting Flow
1. Access Crafting Workshop at camp
2. Choose profession station (or simple crafting)
3. Select recipe category
4. View material requirements
5. Select crafter from party
6. Check materials and profession
7. Craft item if requirements met

---

## 🗺️ TERRAIN-BASED RESOURCE LOCATIONS

### Where to Find Key Materials

#### **Metal Ores**
- **Iron Ore:** Mountains (65%), Plains (35%)
- **Copper Ore:** Plains (45%)
- **Silver Ore:** Mountains (40%)
- **Gold Nugget:** Mountains (20%)
- **Mithril Ore:** Mountains (30%, requires Blacksmithing)

#### **Leather Materials**
- **Wolf Pelt:** Forests (50%, Leatherworking)
- **Deer Hide:** Forests (40%, Leatherworking)
- **Desert Chitin:** Desert (45%, Leatherworking)
- **Fish Scales:** Water (55%, Leatherworking)
- **Rabbit Pelt:** Plains (50%, Leatherworking)

#### **Herbs & Plants**
- **Healing Herb:** Forests (60%, Herbalism)
- **Mana Flower:** Forests (45%, Herbalism)
- **Mushroom Cap:** Forests (55%, Herbalism)
- **Cactus Flesh:** Desert (55%, Herbalism)
- **Dried Herbs:** Desert (50%, Herbalism)
- **Seaweed:** Water (65%, Herbalism)
- **Kelp Strand:** Water (60%, Herbalism)
- **Wildflowers:** Plains (70%, Herbalism)
- **Rootvine:** Forests (40%, Herbalism)

#### **Magical Materials**
- **Crystal Shard:** Mountains (25%, Enchanting)
- **Fire Crystal:** Volcanic (30%, Enchanting)
- **Aqua Crystal:** Water (30%, Enchanting)
- **Ancient Tablet:** Desert (15%, Enchanting)

#### **Gems**
- **Small Gem:** Mountains (35%, Jewelcrafting)
- **Large Gem:** Mountains (15%, Jewelcrafting)
- **Pearl:** Water (20%, Jewelcrafting)
- **Sun Crystal:** Desert (25%, Jewelcrafting)
- **Volcanic Glass:** Volcanic (35%, Jewelcrafting)

#### **Alchemical Reagents**
- **Sulfur:** Volcanic (60%, Alchemy)
- **Flame Essence:** Volcanic (25%, Alchemy)
- **Ash Powder:** Volcanic (50%, Alchemy)
- **Scorpion Venom:** Desert (35%, Alchemy)
- **Saltpeter:** Water (50%, Alchemy)

#### **Basic Materials**
- **Wood:** Forests (75%)
- **Cotton:** Plains (60%)
- **Spider Silk:** Forests (35%)
- **Coral Fragment:** Water (45%)
- **Sandstone:** Desert (70%)
- **Lava Rock:** Volcanic (70%)

---

## 🔄 CRAFTING MATERIAL DEPENDENCIES

### Component Crafting Required

#### **Blacksmith Components** (used by Leatherworking)
- Iron Nails → Used in studded leather, hide bracers
- Steel Chain → Used in ranger's armor, travel packs
- Metal Clasp → Used in leather accessories, bags
- Reinforced Frame → Used in backpacks

#### **Alchemy Reagents** (used by Enchanting/Jewelcrafting)
- Magic Dust → Used in jewelry, enchantments
- Arcane Dust → Used in high-end enchanted items
- Blessed Water → Used in healing recipes, vestments
- Essence Extract → Used in superior potions

#### **Enchanting Materials** (used by all professions)
- Enchanted Thread → Used in archmage robes
- Enchanted Gem → Used in phoenix elixir, dragon rings
- Mystic Essence → Used in ultimate gear

#### **Leatherworking Materials** (used by Blacksmithing)
- Leather Scraps → Used in plate armor, bucklers
- Tanning Oil → Used in processing
- Cloth → Used in armor padding, vestments

---

## 🎁 EXAMPLE CRAFTING PROGRESSION

### Early Game (Level 1-10)
1. **Gather** common resources (Wood, Cotton, Iron Ore)
2. **Craft** Basic Torch, Leather Vest, Iron Sword
3. **Process** Leather Scraps, Iron Nails
4. **Brew** Basic Healing Potions

### Mid Game (Level 10-20)
1. **Travel** to diverse terrains (Mountains, Desert, Coast)
2. **Gather** profession-specific resources
3. **Craft** Studded Leather, Chainmail, Greater Potions
4. **Process** Magic Dust, Steel Chain, Cut Gems
5. **Enhance** gear with basic enchantments

### Late Game (Level 20-30)
1. **Access** all terrain types
2. **Gather** rare materials (Mithril, Ancient Tablets, Pearls)
3. **Craft** Obsidian Greatswords, Archmage Robes
4. **Process** Arcane Dust, Mystic Essence, Enchanted Gems
5. **Create** ultimate gear (Dragon Ring, Staff of Archmage)

---

## 🏆 CRAFTING ACHIEVEMENTS & GOALS

### Profession Mastery
- Craft 10 items in a profession
- Craft 50 items in a profession
- Craft all recipes in a profession

### Resource Collector
- Gather 100 total resources
- Gather resources from all terrain types
- Gather all profession-specific resources

### Master Crafter
- Have 2 characters with all professions covered
- Craft an item requiring materials from 3+ professions
- Create ultimate items (Dragon Ring, Staff of Archmage, etc.)

---

## 💡 TIPS & STRATEGIES

### Profession Selection Strategy
1. **Solo Player:** Choose 1 gathering + 1 production for self-sufficiency
2. **Party Play:** Distribute professions across party members
3. **Class Synergy:** 
   - Warriors → Blacksmithing + Leatherworking
   - Mages → Enchanting + Alchemy (get Alchemy bonus!)
   - Rogues → Leatherworking + Jewelcrafting
   - Priests → Alchemy + Herbalism (get Alchemy bonus!)

### Efficient Resource Gathering
1. **Plan Routes:** Visit multiple terrains in one journey
2. **Weather Watch:** Wait for clear weather before gathering
3. **High Agility:** Train agility for better success rates
4. **Profession Coverage:** Ensure party covers all professions

### Crafting Economics
1. **Components First:** Process raw materials into components
2. **Bulk Crafting:** Some recipes make multiple items (Iron Nails: x3)
3. **Trade Materials:** Share resources between party members
4. **Sell Excess:** Unneeded materials can be sold in towns

### Advanced Crafting
1. **Plan Ahead:** Check recipes before gathering trips
2. **Material Stockpile:** Keep common materials in stock
3. **Cross-Training:** Having Herbalism helps all Alchemists
4. **Upgrade Path:** Craft basic → enhance → enchant → ultimate

---

## 🔮 UNIQUE RECIPE HIGHLIGHTS

### 🔨 Blacksmithing Highlights
- **Obsidian Greatsword:** Volcanic glass weapon (Str +18)
- **Volcanic Bulwark:** Fire-themed shield (HP +40, Str +5)
- **Mithril Blade:** Light yet strong (Str +16, Agi +5)

### 🧵 Leatherworking Highlights
- **Shadow Leather:** Desert exotic (Agi +10, Intel +4)
- **Scaled Armor:** Aquatic armor (Agi +7, HP +30, Armor +5)
- **Backpack:** +15 inventory slots

### ⚗️ Alchemy Highlights
- **Phoenix Elixir:** Revive fallen allies
- **Elixir of the Titan:** All stats +3 buff
- **Elixir of the Gods:** Ultimate power (requires Arcane Amulet!)

### ✨ Enchanting Highlights
- **Staff of the Archmage:** Best caster weapon
- **Archmage Robes:** Ultimate mage armor
- **Elemental Cloak:** All stats +4 balanced armor

### 💎 Jewelcrafting Highlights
- **Dragon Ring:** All stats +7, HP +20 (ultimate ring)
- **Arcane Amulet:** Intel +12, Mana +35, All +4
- **Elemental Amulet:** Balanced all stats +8, Mana +20

---

## 🐛 TROUBLESHOOTING

### "No party members have X profession!"
- Solution: Not all characters selected that profession during creation
- Workaround: Use party members with appropriate professions
- Note: Mages/Priests can use Alchemy without the profession

### "Missing materials!"
- Solution: Search for resources in appropriate terrain
- Check: Resource Gathering Guide for terrain types
- Farm: Revisit terrains to gather more materials

### "Inventory full!"
- Solution: Equip better bags (craft with Leatherworking)
- Sell: Excess materials in towns
- Drop: Unneeded items to make space

### Resources not appearing when searching
- Check: Are you in the right terrain?
- Check: Does your searcher have the required profession?
- Weather: Bad weather reduces success rates
- Agility: Low agility = lower success rates

---

## 🚀 FUTURE ENHANCEMENTS

### Planned Features
- **Profession Skill Levels:** Gain expertise, unlock better recipes
- **Rare Resource Nodes:** Guaranteed spawns in specific locations
- **Crafting Bonuses:** Intelligence affects enchanting, Strength affects blacksmithing
- **Recipe Discovery:** Find hidden recipes in dungeons
- **Crafting Quests:** NPCs request specific crafted items
- **Material Storage:** Dedicated crafting material bank
- **Quick Craft:** Craft multiple items at once
- **Salvaging:** Break down equipment for materials

---

## 📝 DEVELOPER NOTES

### Adding New Resources
1. Add to `ResourceDatabase` in `ResourceGathering.cs`
2. Specify: Name, Terrain, Profession, Success Rate, Amount Range
3. Update: Resource Gathering Guide display

### Adding New Recipes
1. Add method in appropriate section (Blacksmithing, Leatherworking, etc.)
2. Specify: Materials, Result item, Stats, Profession requirement
3. Update: Recipe display methods
4. Test: Material checking and inventory management

### Profession System
- Defined in: `Systems/CraftingProfession.cs`
- Character properties: `PrimaryProfession`, `SecondaryProfession`
- Selection: During character creation in `GameInitializer.cs`
- Validation: `ProfessionManager.CanCraftWithProfession()`

---

## 🎉 CONCLUSION

The expanded crafting system adds:
- ✅ **6 Distinct Professions** with unique recipes
- ✅ **50+ Unique Resources** tied to terrain types
- ✅ **100+ Crafting Recipes** across all professions
- ✅ **Cross-Profession Dependencies** for advanced items
- ✅ **Terrain-Based Gathering** with weather effects
- ✅ **Strategic Profession Selection** during character creation

**Status:** ✅ Fully implemented and integrated
**Balance:** ⚖️ Designed for party synergy
**Expandability:** 🚀 Easy to add new professions and recipes

Happy crafting! 🛠️✨

---

## 📚 QUICK REFERENCE

### Profession Icons
- 🔨 Blacksmithing
- 🧵 Leatherworking
- ⚗️ Alchemy
- ✨ Enchanting
- 💎 Jewelcrafting
- 🌿 Herbalism

### Terrain Icons
- ⛰️ Mountains (^)
- 🌲 Forests (░)
- 🏜️ Desert (≋)
- 🌋 Volcanic (▒)
- 🌊 Water (~)
- 🌾 Plains (□ or space)

### Best Resource Locations
- **Need Ores?** → Mountains (Ironforge, Stormwatch)
- **Need Herbs?** → Forests (Shadowkeep, Pinewood)
- **Need Leather?** → Forests (Wolf Pelt, Deer Hide)
- **Need Exotic?** → Desert (Sunspire, Oasis Rest)
- **Need Fire Items?** → Volcanic (Emberpeak)
- **Need Water Items?** → Coast (Mysthaven, Crystalshore)
- **Need Basics?** → Plains (Havenbrook, Central areas)
