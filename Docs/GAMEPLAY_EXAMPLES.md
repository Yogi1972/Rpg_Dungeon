# GAMEPLAY EXAMPLES

## Example 1: Starting the Main Story

```
PROGRAM STARTING...
Please wait...
UTF-8 encoding set.
Showing title screen...

[After character creation]

╔════════════════════════════════════════════════════════════════════╗
║                      THE CALL TO ADVENTURE                        ║
╚════════════════════════════════════════════════════════════════════╝

The realm is in peril. Darkness spreads from an unknown source,
corrupting the land and its creatures. Strange occurrences plague
the towns and villages across the world.

Eight major cities hold pieces of the puzzle:

  🏰 Havenbrook - The trade hub where rumors gather
  ⚒️  Ironforge Citadel - The mountain fortress of master craftsmen
  🔮 Mysthaven - The mystical port city of arcane knowledge
  ☀️  Sunspire - The desert city guarding ancient secrets
  🌑 Shadowkeep - The dark citadel where truth lies hidden
  💎 Crystalshore - The crystalline coast of master jewelers
  🔥 Emberpeak - The volcanic city of legendary alchemists
  ⚡ Stormwatch - The sky fortress of storm mages

Your journey will take you to all corners of the realm.
Only by visiting each city and uncovering their secrets
can you hope to stop the coming darkness.

Your adventure begins in Havenbrook...

Press Enter to begin your quest...
```

## Example 2: Meeting an NPC in Havenbrook

```
╔══════════════════════════════════════════════╗
║        Havenbrook                          ║
╚══════════════════════════════════════════════╝
🏰 A bustling trade city at the crossroads of major routes. The starting point for many adventurers.
   Specialty: Trade and Commerce
   👥 NPCs in town: 8

--- Town Services ---
1) Visit Inn
2) Visit Shops
3) Visit Quest Board
4) Visit Training Hall
5) Visit Bank
6) Explore Town Districts
7) Talk to NPCs 👥
0) Leave Town
Choice: 7

╔══════════════════════════════════════════════╗
║        NPCs in Havenbrook                   ║
╚══════════════════════════════════════════════╝
1) Elder Morris - Elder 📖
2) Captain Aldric - Guard
3) Sara the Trader - Merchant
4) Tom Brightwood - Questgiver ❗
5) Mysterious Hooded Figure - Informant
6) Wandering Bard - Traveler
7) Mary Cooper - Citizen
8) Blacksmith's Son - Citizen
0) Back

Talk to whom? 1

╔══════════════════════════════════════════════════════════════════╗
║  👴 Elder Morris                                                  ║
╚══════════════════════════════════════════════════════════════════╝
The wise elder of Havenbrook, keeper of ancient knowledge.

✨ [STORY NPC] - This person seems important to your quest.

--- Conversation Options ---
1) Share your wisdom.
2) Tell me about the old days.
3) What secrets do you know?
4) Tell me about the ancient prophecy.
5) Farewell.

Your choice: 4

Elder Morris: 
"Long ago, a prophecy foretold of darkness returning to our realm.
Only heroes who unite the five cities can hope to seal it away..."

[Story progresses to Chapter 2]
```

## Example 3: Accepting a Quest from an NPC

```
Talk to whom? 4

╔══════════════════════════════════════════════════════════════════╗
║  ❗ Tom Brightwood                                                ║
╚══════════════════════════════════════════════════════════════════╝
A local farmer who often needs help with problems.

--- Conversation Options ---
1) Do you need any help?
2) What troubles do you face?
3) Tell me about this place.
4) Farewell.
Q) View Available Quests (1)

Your choice: Q

Tom Brightwood has the following quests available:

1) Farmland Protection [Easy]
   Wolves have been attacking my livestock. Please deal with 3 wolves.
   Objective: Wolf (0/3)
   Rewards: 40 gold, 75 XP

Accept which quest? (0 to cancel): 1

✅ Quest 'Farmland Protection' accepted!
```

## Example 4: Using the Fog of War Map

```
Main Menu:
15) View Fog of War Map 🗺️
Choose: 15

╔════════════════════════════════════════════════════════════════════════════════╗
║                            🗺️  WORLD MAP                                      ║
╚════════════════════════════════════════════════════════════════════════════════╝

┌────────────────────────────────────────────────────────────────────────────────────────────────────┐
│                                                                                                    │
│  ^^^^^^^^^^^            ~~~~~~                    ^^^                          ~~~~~~~~           │
│  ^^  ■ ^^^^^^^          ~~~~~~                   ^^^^                         ~~~~~~~~~           │
│ ^^^     ▲   ^^^^        ~~~~~~                  ^^^^^                        ~~~~~~~~~~           │
│^^^  ◈      ^^^ ░        ~~~~ ◈                 ^^^^^^                        ~~~~~~~~~~~          │
│^^  ▲  ■     ░░░         ~~~~                  ^^^^^^ ▲                      ~~~~~~~~~~~~          │
│^    ░░░░                                     ^^^^^^^                        ~~~ ◈ ~~~~~~          │
│  ░░░░░                                      ^^^^^^^^  ■                    ~~~~~~~~~~~~           │
│   ░░░    ▲                                 ^^^^^^^^                       ~~~~~~~~~~~~~           │
│  ░░░  ■                                   ^^^^^^^^                       ~~~~~~~~~~~~~~           │
│    ░                                     ^^^^^^^^ ▲                      ~~~~~~~~~~~~~~~          │
│  ▲                                      ^^^^^^^                         ~~~~~~~~~~~~~~~~          │
│     ■   ░░                            ███████                          ~~~~~~~~~~~~~~~~~~~~~~~~~  │
│  ░░░░░      ▲                       ███████                           ~~~~~~~~ ■ ~~~~~~~~~~ ◈ ~~  │
│ ░░░░░░                            ███████                            ~~~~~~~~~~~~~~~~~~~~~~~~~~   │
│   ░░░   ■                        ██████                             ~~~~~~~~~~~~~ ▲ ~~~~~~~~~~   │
│  ▲  ░                          ██████                              ~~~~~~~~~~~~~~~~~~~~~~~~~~~    │
│                  ▲           ██████                               ~~~~~ ■ ~~~~~~~~~~~~~~~~~~~~    │
│     □        ★                ███████                            ~~~~~~~~~~~~~~ ▲ ~~~~~~~~~~~ ■   │
│       ▲ ■ ▲    ░░            █████                              ~~~~~~~~~~~~~~~~~~~~~~~~~~~       │
│    ░░░░░░                   █████                               ~~~~~~ ◈ ~~~~~~~~~~~~~~~~~~       │
│   ░░░░░   ■               █████                               ≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋        │
│    ▲                     █████                                ≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋         │
│  ■                      █████                                 ≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋          │
│    ░░░                 █████                                  ≋≋≋≋≋≋  ■ ≋≋≋≋≋≋≋≋≋≋≋≋≋≋            │
│  ░░░░░                █████                                   ≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋ ▲              │
│   ░░░ ▲             ████                                       ≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋                │
│  ░░                ███                                          ≋≋≋≋≋≋≋≋≋ ◈ ≋≋≋≋≋≋                │
│  ▲ ░░           ████                                            ≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋                  │
│   ░░░░        ████  ■                                            ≋≋≋≋▲≋≋≋≋≋≋≋≋                    │
│   ░░░░     ████                                                   ≋≋≋≋≋≋≋≋≋≋                      │
│    ░░░   ████  ▒▒▒▒▒▒                                             ≋≋≋≋≋≋≋                        │
│      ░  ████ ▒▒▒▒▒▒▒▒▒                                                ▲                          │
│        ███ ▒▒▒▒▒▒▒▒▒▒▒                                                                            │
│  ◈    ▲  ▒▒▒▒ ◈ ▒▒▒▒▒▒                                                                            │
│   ████  ▒▒▒▒▒▒▒▒▒▒▒▒▒                                                                             │
│    ████ ▒▒▒▒▒▒▒▒▒▒▒                                                                               │
│      ████▒▒▒▒▒▒▒▒▒                                                                                │
│        █████▒▒▒                                                                                   │
└────────────────────────────────────────────────────────────────────────────────────────────────────┘

📍 Legend:
  ★ = Your Location  |  ◈ = Major Town  |  ■ = Settlement  |  ▲ = Camp
  ~ = Water  |  ^ = Mountains  |  ≋ = Desert  |  ░ = Forest  |  □ = Plains  |  █ = Undiscovered

📍 Current Location: Havenbrook
🗺️  Exploration: 15/44 locations discovered (34%)

--- Options ---
L) List Discovered Locations
S) View Story Objectives
T) Tips for Exploration
0) Close Map

Choice: L

╔════════════════════════════════════════════════════════════════════╗
║                    DISCOVERED LOCATIONS                           ║
╚════════════════════════════════════════════════════════════════════╝

🏰 Major Towns:
   ◈ Havenbrook (Lv 1) ⬅ YOU ARE HERE
   ◈ Ironforge Citadel (Lv 10)
   ◈ Crystalshore (Lv 12)

🏘️  Settlements:
   ■ Willowdale (Lv 1)
   ■ Crossroads Keep (Lv 3)
   ■ Stonebridge (Lv 8)
   ■ Silvermist (Lv 13)

⛺ Camps:
   ▲ Traveler's Rest
   ▲ Wagon Circle
   ▲ Milestone Camp
   ▲ Guard Post
   ▲ Hunter's Clearing
   ▲ Woodcutter's Site
   ▲ Cave Shelter
   ▲ Seaside Camp

Press Enter to continue...
```

## Example 5: NPC at a Settlement

```
╔══════════════════════════════════════════════╗
║        Welcome to Crossroads Keep          ║
╚══════════════════════════════════════════════╝
🏘️  A fortified waystation where three roads meet.
   👥 NPCs present: 2

Weather: Partly Cloudy
Time: Afternoon

--- Settlement Services ---
1) Visit Inn (Rest & Heal)
2) Visit Shop
3) Check Quest Board
4) Talk to NPCs 👥
0) Leave Settlement
Choice: 4

╔══════════════════════════════════════════════╗
║        NPCs in Crossroads Keep              ║
╚══════════════════════════════════════════════╝
1) Crossroads Keep Innkeeper - Citizen
2) Local Guide - Questgiver ❗
0) Back

Talk to whom? 2

╔══════════════════════════════════════════════════════════════════╗
║  ❗ Local Guide                                                   ║
╚══════════════════════════════════════════════════════════════════╝
A local resident who might have tasks for you.

--- Conversation Options ---
1) Do you need any help?
2) What troubles do you face?
3) Tell me about this place.
Q) View Available Quests (1)
4) Farewell.

Your choice: Q

Local Guide has the following quests available:

1) Pest Control [Easy]
   Local creatures are causing trouble. Eliminate 4 of them.
   Objective: Pest (0/4)
   Rewards: 60 gold, 100 XP

Accept which quest? (0 to cancel): 1

✅ Quest 'Pest Control' accepted!
```

## Example 6: NPC at a Camp

```
╔══════════════════════════════════════════════╗
║        Hunter's Clearing                    ║
╚══════════════════════════════════════════════╝
⛺ A camp used by local hunters.
   Camp Type: Forest
   👤 Someone is at this camp

--- Camp Options ---
1) Rest by the Fire (Free) - Restore 25% HP/Stamina
2) Forage for Food - Restore some HP
3) Check Surroundings
4) Talk to Person at Camp 👤
0) Leave Camp
Choice: 4

👤 You see: Fellow Adventurer (Has Quest)
Talk to them? (y/n): y

╔══════════════════════════════════════════════════════════════════╗
║  ❗ Fellow Adventurer                                             ║
╚══════════════════════════════════════════════════════════════════╝
An adventurer seeking help at Hunter's Clearing.

--- Conversation Options ---
1) Do you need any help?
2) What troubles do you face?
3) Tell me about this place.
Q) View Available Quests (1)
4) Farewell.

Your choice: 1

Fellow Adventurer: 
"Actually, yes! I have a task that might interest you."

[Quest can be accepted via Q option]
```

## 🎮 MAIN MENU UPDATES

New Options Added:
- **Option 15**: View Fog of War Map 🗺️
- **Option 16**: View Story Progress 📖

Story Objective Display:
```
╔════════════════════════════════════════════════════════════════════╗
║  📖 MAIN QUEST: Chapter 1 - The Awakening                          ║
╚════════════════════════════════════════════════════════════════════╝
🎯 Current Objective: Speak with Elder Morris in Havenbrook
📍 Location: Havenbrook
⭐ Required Level: 1

Main Menu:
[options...]
```

## 💡 PLAYER BENEFITS

1. **Clear Direction**: Main story guides players to all locations
2. **Rich Interactions**: Every location has NPCs to talk to
3. **Extra Rewards**: NPC quests provide additional gold and XP
4. **Exploration Tracking**: Fog of war map shows discovery progress
5. **Immersive World**: NPCs make the world feel alive
6. **Flexible Progression**: Can do story or side content
7. **Contextual Dialogue**: NPC responses change based on story progress

---

## 🌍 EXPANDED WORLD - ALL LOCATIONS

### 🏰 Major Towns (8)

1. **Havenbrook** (Lv 1) - Trade and Commerce
   - Starting location, bustling crossroads city

2. **Ironforge Citadel** (Lv 10) - Blacksmithing and Armor Crafting
   - Mountain fortress with legendary smiths

3. **Crystalshore** (Lv 12) ⭐ NEW - Jewelry and Gemcraft
   - Crystalline coastal city famous for gem cutting

4. **Mysthaven** (Lv 15) - Magic and Enchanting
   - Mysterious mist-shrouded port with arcane academies

5. **Emberpeak** (Lv 18) ⭐ NEW - Alchemy and Fire Crafts
   - Volcanic city where alchemists harness geothermal power

6. **Sunspire** (Lv 20) - Exotic Goods and Artifacts
   - Golden desert city with ancient treasures

7. **Stormwatch** (Lv 22) ⭐ NEW - Navigation and Storm Magic
   - Cliff-top fortress of storm mages and navigators

8. **Shadowkeep** (Lv 25) - Elite Training and Dark Arts
   - Dark gothic city for elite adventurers

### 🏘️ Settlements (13)

Original 10:
- Willowdale (Lv 1), Crossroads Keep (Lv 3), Pinewood (Lv 5)
- Riverside (Lv 5), Stonebridge (Lv 8), Frosthollow (Lv 10)
- Oasis Rest (Lv 12), Moonwell (Lv 15), Thornwall (Lv 18)
- Ghostlight (Lv 20)

New Additions:
- **Silvermist** (Lv 13) ⭐ NEW - Foggy coastal village with sailor tales
- **Copperhill** (Lv 16) ⭐ NEW - Copper mining settlement in the hills
- **Ravencrest** (Lv 24) ⭐ NEW - Fortified rocky outcrop watching post

### ⛺ Camps (23)

Original 20 + New Additions:
- **Windswept Ridge** ⭐ NEW (Mountain) - Windy ridge with spectacular views
- **Seaside Camp** ⭐ NEW (Riverside/Coastal) - Coastal camp with crashing waves
- **Merchant's Rest** ⭐ NEW (Ruins) - Ruins of an old trading post

### 🗺️ Terrain Features (Unsettled Areas)

The expanded map now includes diverse terrain types:

- **^ = Mountains** - Northwest (Ironforge area) and North (Stormwatch cliffs)
- **~ = Water** - Northeast coast (Mysthaven, Crystalshore) and Eastern sea
- **≋ = Desert** - Southeast region (Sunspire territory)
- **░ = Forest** - Western and central forests, Southwest dark woods (Shadowkeep)
- **▒ = Volcanic** - South-central region (Emberpeak territory)
- **□ = Plains** - Central grasslands connecting major routes
- **█ = Undiscovered** - Areas yet to be revealed

### 📊 World Statistics

- **Total Locations**: 44 (increased from 35)
- **Major Towns**: 8 (increased from 5)
- **Settlements**: 13 (increased from 10)
- **Camps**: 23 (increased from 20)
- **Map Size**: 100x40 (expanded from 80x30)

### 🎯 Exploration Features

**Terrain-Based Gameplay:**
- Mountainous regions have harder encounters
- Coastal areas feature water-themed content
- Desert areas require preparation for harsh conditions
- Volcanic regions offer unique fire-based challenges
- Forests provide stealth and hunting opportunities

**Progressive Discovery:**
- Locations reveal based on proximity
- Major towns unlock larger reveal radius (15)
- Settlements unlock moderate radius (8)
- Natural terrain shows realm geography

Enjoy your enhanced RPG adventure! 🎮✨
