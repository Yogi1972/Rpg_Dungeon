# Map System Documentation

## Overview

The new map system provides a rich, interconnected world with multiple types of locations for players to explore. The world consists of **5 major towns**, **10 settlements**, and **20 camps**, each offering different services and experiences.

---

## Location Types

### 🏰 Major Towns (5 Total)
Major towns are the largest locations with full services and specializations.

#### 1. **Havenbrook** (Starting Town - Level 1)
- **Specialty**: Trade and Commerce
- **Description**: A bustling trade city at the crossroads of major routes. The starting point for many adventurers.
- **Services**: All services available (Inn, Shops, Quest Board, Training Hall, Bank, Multiple Districts)
- **Status**: Discovered by default

#### 2. **Ironforge Citadel** (Level 10)
- **Specialty**: Blacksmithing and Armor Crafting
- **Description**: A fortress city built into the mountain, renowned for its master craftsmen and legendary smiths.
- **Services**: All services with focus on weapon/armor crafting

#### 3. **Mysthaven** (Level 15)
- **Specialty**: Magic and Enchanting
- **Description**: A mysterious port city shrouded in mist, home to powerful mages and arcane academies.
- **Services**: All services with focus on magic items and enchantments

#### 4. **Sunspire** (Level 20)
- **Specialty**: Exotic Goods and Artifacts
- **Description**: A golden city in the desert, famous for exotic goods and ancient treasures from tomb raiders.
- **Services**: All services with rare and exotic items

#### 5. **Shadowkeep** (Level 25)
- **Specialty**: Elite Training and Dark Arts
- **Description**: A dark gothic city for experienced adventurers, offering elite training and rare dark artifacts.
- **Services**: All services with high-level and elite features

---

### 🏘️ Settlements (10 Total)
Settlements are medium-sized locations with limited but essential services. **All settlements have an inn** for resting and healing.

1. **Willowdale** (Level 1) - Has Shop, Has Quest Board
   - A peaceful farming village surrounded by wheat fields.

2. **Crossroads Keep** (Level 3) - Has Shop, Has Quest Board
   - A fortified waystation where three roads meet.

3. **Pinewood** (Level 5) - Has Shop
   - A logging community deep in the forest.

4. **Riverside** (Level 5) - Has Shop, Has Quest Board
   - A quiet fishing village by the great river.

5. **Stonebridge** (Level 8) - Has Quest Board
   - A settlement built around an ancient stone bridge.

6. **Frosthollow** (Level 10) - Has Shop
   - A cold mountain settlement of hardy folk.

7. **Oasis Rest** (Level 12) - Has Shop, Has Quest Board
   - A desert settlement around a precious water source.

8. **Moonwell** (Level 15)
   - A mystical settlement near a magical spring.

9. **Thornwall** (Level 18) - Has Shop, Has Quest Board
   - A walled settlement on the dangerous frontier.

10. **Ghostlight** (Level 20) - Has Quest Board
    - An eerie settlement near the haunted lands.

#### Settlement Services

**Inn Services (Available at ALL settlements):**
- **Rest for the Night** (50 gold per person): Fully restores HP, Mana, and Stamina
- **Quick Meal** (15 gold per person): Restores 50% HP
- **Ale and Stories** (5 gold): Hear local rumors and lore

**Shop Services** (Where available):
- Basic potions
- Torches
- Travel supplies

**Quest Board** (Where available):
- Accept local quests
- Check quest progress

---

### ⛺ Camps (20 Total)
Camps are small locations offering basic survival needs. They're free to use and scattered throughout the world.

#### Roadside Camps (5):
- **Traveler's Rest**: A common stop for merchants and adventurers.
- **Wagon Circle**: A defensive camp formation used by caravans.
- **Milestone Camp**: A camp marked by an ancient stone milestone.
- **Crossroads Camp**: A busy camp where multiple paths converge.
- **Guard Post**: An abandoned guard post turned makeshift camp.

#### Forest Camps (4):
- **Hunter's Clearing**: A camp used by local hunters.
- **Woodcutter's Site**: A clearing with stumps and a fire pit.
- **Druid Circle**: A sacred grove with ancient standing stones.
- **Ranger Outpost**: A hidden camp used by forest rangers.

#### Mountain Camps (3):
- **Eagle's Nest**: A high altitude camp with a commanding view.
- **Cave Shelter**: A natural cave offering protection from elements.
- **Mountain Pass Camp**: A camp at the highest point of the pass.

#### Desert Camps (3):
- **Dune Hollow**: A depression in the sand providing shelter from wind.
- **Nomad Circle**: A traditional circular camp of desert nomads.
- **Rock Shelter**: A camp sheltered by large desert rocks.

#### Riverside Camps (3):
- **Fisher's Camp**: A camp by the water with drying racks.
- **Ferry Landing**: A camp near an old ferry crossing.
- **Beaver Dam**: A camp near a large beaver dam.

#### Ruins Camps (2):
- **Temple Steps**: A camp among crumbling temple ruins.
- **Old Fort**: A camp within the walls of an ancient fort.

#### Camp Services (Free):
- **Rest by Fire**: Restore 25% HP and Stamina
- **Forage for Food**: Chance to find berries and restore HP
- **Check Surroundings**: Get atmospheric descriptions and lore

---

## Map System Features

### Discovery System
- Locations start as **undiscovered** (except Havenbrook)
- Discover new locations by:
  - Traveling to nearby areas
  - Following quest objectives
  - Exploring the world map
  - Reaching level requirements

### Level Requirements
- Each location has a **minimum level requirement**
- Players below the requirement can see the location but cannot travel there
- This creates natural progression through the world

### Distance System
- Each location has calculated distances to other locations
- Travel time depends on:
  - Distance between locations
  - Current weather conditions
  - Time of day (30% slower at night)
  - Random travel events

### World Map Interface
The world map provides several views:

1. **Show All Discovered Locations**: Complete list of everywhere you've been
2. **Show Major Towns**: Filter to see only towns
3. **Show Settlements**: Filter to see only settlements
4. **Show Camps**: Filter to see only camps
5. **Travel to Location**: Select a destination and travel there

---

## Using the Map System

### Integration with Game
To use the map system in your game:

```csharp
// Create the world map
var worldMap = new WorldMap(weather, timeTracker);

// Set game state references
worldMap.SetGameStateReferences(questBoard, bountyBoard, achievementTracker, journal);

// Show the map to the player
worldMap.ShowWorldMap(party);

// Discover new locations
worldMap.DiscoverLocation("Ironforge Citadel");

// Check progress
int discovered = worldMap.GetDiscoveredCount();
int total = worldMap.GetTotalLocationCount();
```

### Travel Mechanics
When traveling between locations:
1. Select destination from world map
2. Check if level requirement is met
3. Calculate travel time based on distance and conditions
4. Experience random travel events
5. Arrive at destination
6. Enter location and access services

---

## Location Class Hierarchy

```
Location (Abstract Base Class)
├── MajorTown
│   └── Full services with specialization
├── Settlement
│   └── Limited services + mandatory inn
└── Camp
    └── Basic survival services
```

### Key Properties
All locations share these properties:
- **Name**: Location name
- **Description**: Flavor text
- **Type**: LocationCategory (Town/Settlement/Camp)
- **RequiredLevel**: Minimum level to access
- **IsDiscovered**: Whether player has found it
- **DistancesToOtherLocations**: Dictionary of distances

---

## Progression Path

### Early Game (Levels 1-5)
- Start in **Havenbrook**
- Explore **Willowdale**, **Crossroads Keep**, **Pinewood**, **Riverside**
- Use roadside and forest camps

### Mid Game (Levels 5-15)
- Travel to **Stonebridge**, **Frosthollow**
- Unlock **Ironforge Citadel** (Level 10)
- Reach **Mysthaven** (Level 15)
- Explore **Oasis Rest** and **Moonwell**
- Use mountain and desert camps

### Late Game (Levels 15-25+)
- Access **Thornwall**, **Ghostlight**
- Unlock **Sunspire** (Level 20)
- Reach **Shadowkeep** (Level 25)
- Master all camp types
- Complete exploration achievements

---

## Design Philosophy

### Balance
- **Towns**: Full services, expensive, rare
- **Settlements**: Essential services (inn), moderate cost, common
- **Camps**: Basic services, free, abundant

### Immersion
- Each location has unique atmosphere and lore
- Weather and time of day affect travel
- Random events and rumors add variety
- Camps provide survival flavor

### Progression
- Level gates create sense of advancement
- Discovery encourages exploration
- Distance system makes world feel large
- Specializations give each town identity

---

## Future Expansion Ideas

- **Dynamic Events**: Locations can be attacked or change over time
- **Reputation System**: Prices and services vary by faction standing
- **Fast Travel**: Unlock fast travel between discovered major towns
- **Location Quests**: Each settlement has unique quest chains
- **Camp Upgrades**: Players can improve camps they visit frequently
- **Random Encounters**: More varied travel events between locations
- **Seasonal Changes**: Locations change appearance/services by season

---

## File Structure

```
World/
├── Location.cs          # Base location class and LocationCategory enum
├── MajorTown.cs        # Major town implementation
├── Settlement.cs       # Settlement and Inn classes
├── Camp.cs             # Camp and CampType enum
└── WorldMap.cs         # World map manager
```

---

## Tips for Players

1. **Always carry gold** for inn services in settlements
2. **Use camps** when low on funds - they're free
3. **Check level requirements** before long journeys
4. **Travel during day** to save time
5. **Visit inns before tough quests** to start at full strength
6. **Explore settlements** for quest boards and shops
7. **Listen to rumors** in inns for lore and hints
8. **Discover everything** for completion achievements

---

## Summary

The map system provides a rich, layered world with 35 total locations:
- **5 Major Towns** with full services and specializations
- **10 Settlements** with inns and limited services
- **20 Camps** for basic survival needs

Each location type serves a specific purpose in the game economy and progression, creating a balanced and immersive exploration experience.
