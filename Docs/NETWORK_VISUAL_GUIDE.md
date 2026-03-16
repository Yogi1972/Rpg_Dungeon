# Visual Guide: Network Multiplayer Setup

## 🎯 Step-by-Step with Screenshots

### STEP 1: Launch Game & Access Menu

```
╔══════════════════════════════════════════════════════════════════╗
║                          MAIN MENU                               ║
╚══════════════════════════════════════════════════════════════════╝

  1) ⚔️  Start New Game
  2) 💾 Load Saved Game
  3) 👥 Local Multiplayer (Hotseat)
  4) 🌐 Network Multiplayer (LAN)     ← SELECT THIS!
  5) 📂 Load Multiplayer Save
  6) 📖 How to Play
  7) ℹ️  About
  8) 📋 Error Logs & Diagnostics
  9) 🔄 Check for Updates
  0) 🚪 Exit Game

Choose an option: 4
```

---

### STEP 2: Network Multiplayer Menu

```
╔══════════════════════════════════════════════════════════════════╗
║                   NETWORK MULTIPLAYER (LAN)                      ║
╚══════════════════════════════════════════════════════════════════╝

  🌐 Connect with friends over your local network!
  📡 One player hosts, others join using IP address

  1) 🏠 Host a Game (Create Server)
  2) 🔌 Join a Game (Connect to Server)
  3) 📋 Show My IP Address
  4) ❓ Network Multiplayer Help
  0) ⬅️  Return to Main Menu

Choose an option: _
```

**For Host**: Choose `1`  
**For Joining**: Choose `2`

---

## 🏠 HOST PATH (Player 1)

### STEP 3: Host Setup

```
╔══════════════════════════════════════════════════════════════════╗
║                    HOST NETWORK GAME                             ║
╚══════════════════════════════════════════════════════════════════╝

Enter port (default 7777): [Press Enter]

✅ Server started on port 7777
📡 Local IP: 192.168.1.100        ← SHARE THIS WITH FRIENDS!
⏳ Waiting for players to connect...
```

---

### STEP 4: Create Host Character

```
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
👤 HOST - Create Your Character
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

Character Name: Gandalf

Choose class:
1) Warrior
2) Mage
3) Rogue
4) Priest
Choice: 2

✅ Created Gandalf (Lv 1 Mage)
```

---

### STEP 5: Lobby Commands

```
⏳ Waiting for players to join...
Commands:
  'start' - Start the game
  'list' - Show connected players
  'kick [name]' - Kick a player
  'cancel' - Cancel and return to menu

> _
```

**When a player joins**:
```
🎮 Player connected from 192.168.1.105
```

**Check players**:
```
> list

📊 Connected Players:
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
1. Host - Gandalf (Mage)
2. Player - Aragorn (Warrior)

Total: 2/4
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
```

**Start game**:
```
> start

🎮 Starting game with network multiplayer...
```

---

## 🔌 JOIN PATH (Player 2+)

### STEP 3: Join Setup

```
╔══════════════════════════════════════════════════════════════════╗
║                    JOIN NETWORK GAME                             ║
╚══════════════════════════════════════════════════════════════════╝

Enter host IP address: 192.168.1.100     ← GET THIS FROM HOST!

Enter port (default 7777): [Press Enter]

🔌 Connecting to 192.168.1.100:7777...
✅ Connected successfully!
```

---

### STEP 4: Create Join Character

```
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
👤 Create Your Character
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

Character Name: Aragorn

Choose class:
1) Warrior
2) Mage
3) Rogue
4) Priest
Choice: 1

✅ Created Aragorn (Lv 1 Warrior)
```

---

### STEP 5: Wait for Game Start

```
⏳ Waiting for host to start the game...
(Host will start the game when ready)
```

**When host starts**:
```
🎮 Game starting!
```

---

## 🔍 Finding Your IP Address

### Option 1: In-Game

```
╔══════════════════════════════════════════════════════════════════╗
║                      YOUR IP ADDRESS                             ║
╚══════════════════════════════════════════════════════════════════╝

  📡 Your Local IP Address: 192.168.1.100

  💡 Share this IP with friends so they can join your game!

  ⚠️  NOTE: This only works on the same local network (LAN)
     - Same Wi-Fi network
     - Same wired network
     - Connected via router

Press Enter to return...
```

### Option 2: Windows Command Prompt

```
C:\> ipconfig

Ethernet adapter Local Area Connection:
   IPv4 Address. . . . . . . . . : 192.168.1.100     ← THIS ONE!
   Subnet Mask . . . . . . . . . : 255.255.255.0
   Default Gateway . . . . . . . : 192.168.1.1
```

Look for the **IPv4 Address** that starts with:
- `192.168.x.x`
- `10.0.x.x`
- `172.16.x.x` to `172.31.x.x`

---

## 📱 Quick Reference Card

### For Host
```
┌─────────────────────────────────────────┐
│  📋 HOST CHECKLIST                      │
├─────────────────────────────────────────┤
│ 1. Launch game                          │
│ 2. Select "Network Multiplayer"         │
│ 3. Choose "Host a Game"                 │
│ 4. Note your IP address                 │
│ 5. Share IP with friends                │
│ 6. Create your character                │
│ 7. Wait for players to join             │
│ 8. Type 'start' when ready              │
└─────────────────────────────────────────┘
```

### For Joining
```
┌─────────────────────────────────────────┐
│  📋 JOIN CHECKLIST                      │
├─────────────────────────────────────────┤
│ 1. Get host's IP address               │
│ 2. Launch game                          │
│ 3. Select "Network Multiplayer"         │
│ 4. Choose "Join a Game"                 │
│ 5. Enter host's IP                      │
│ 6. Create your character                │
│ 7. Wait for host to start               │
└─────────────────────────────────────────┘
```

---

## 🆘 Troubleshooting Visual Guide

### ❌ Problem: "Failed to connect"

```
❌ Failed to connect to host!
Press Enter to return to menu...
```

**Fix**:
```
✓ Verify same network
✓ Check IP address: 192.168.1.100 (not 127.0.0.1)
✓ Allow firewall access
✓ Ensure host started server
```

---

### ❌ Problem: "Cannot start server"

```
❌ Failed to start server!
Press Enter to return to menu...
```

**Fix**:
```
✓ Port 7777 might be in use
✓ Try different port (7778, 7779)
✓ Check firewall settings
✓ Close other network programs
```

---

### ❌ Problem: "IP shows 127.0.0.1"

```
📡 Your Local IP Address: 127.0.0.1     ← WRONG!
```

**Fix**:
Use Windows Command Prompt:
```
C:\> ipconfig
```

Look for actual network IP (192.168.x.x)

---

## 🎮 Example Session

### Timeline View

```
┌──────────────┐                           ┌──────────────┐
│   HOST PC    │                           │  CLIENT PC   │
│ 192.168.1.100│                           │ 192.168.1.105│
└──────────────┘                           └──────────────┘
       │                                          │
       │ 1. Start server                         │
       ├─────────────────────────────────────────┤
       │ (Server listening on port 7777)         │
       │                                          │
       │                            2. Connect to│
       │                               192.168.1.100:7777
       │◄─────────────────────────────────────────┤
       │                                          │
       │ 3. Accept connection                     │
       ├─────────────────────────────────────────►│
       │    "Player connected"                    │
       │                                          │
       │ 4. Send LobbyUpdate                      │
       ├─────────────────────────────────────────►│
       │                                          │
       │                    5. Both create chars  │
       │                                          │
       │ 6. Type 'start'                          │
       ├─────────────────────────────────────────►│
       │    Broadcast GameStart                   │
       │                                          │
       │ 7. Game begins!                          │
       │                                          │
```

---

## 📊 Network Topology Diagram

```
                    ┌─────────────┐
                    │   ROUTER    │
                    │ 192.168.1.1 │
                    └─────┬───────┘
                          │
          ┌───────────────┼───────────────┐
          │               │               │
    ┌─────▼─────┐   ┌────▼────┐   ┌─────▼─────┐
    │  HOST PC  │   │CLIENT #1│   │CLIENT #2  │
    │  (Server) │   │         │   │           │
    │192.168.1. │   │192.168. │   │192.168.   │
    │   .100    │   │ 1.105   │   │  1.110    │
    │           │   │         │   │           │
    │🏠 Hosting │   │🔌 Joined│   │🔌 Joined  │
    └───────────┘   └─────────┘   └───────────┘
         │                │               │
         └────────────────┴───────────────┘
                All connected via LAN
```

---

## 🎯 Quick Win Scenarios

### Scenario: 2 Players, Same House

```
Living Room PC (Host)          Bedroom PC (Join)
     📺                              💻
192.168.1.100                  192.168.1.105
     │                              │
     └──────────────────────────────┘
        Same Wi-Fi: "HomeNetwork"

✅ WORKS GREAT!
```

### Scenario: LAN Party (4 Players)

```
       Host         Player 2      Player 3      Player 4
        💻            💻            💻            💻
    192.168.1.100 .105         .110          .115
         │           │            │            │
         └───────────┴────────────┴────────────┘
                All wired to same router

✅ PERFECT SETUP!
```

### Scenario: Over Internet ❌

```
    Your House                  Friend's House
        💻                           💻
  Your router                  Their router
        │                            │
        └──────INTERNET──────────────┘
            (Different networks)

❌ WON'T WORK (yet)
💡 Use VPN: Hamachi, ZeroTier, etc.
```

---

## ✅ Success Indicators

### You'll Know It's Working When:

**Host Screen**:
```
✅ Server started on port 7777
📡 Local IP: 192.168.1.100
🎮 Player connected from 192.168.1.105
📊 Total players in session: 2
```

**Join Screen**:
```
✅ Connected successfully!
📊 Lobby updated
⏳ Waiting for host to start the game...
```

**Both Screens When Starting**:
```
🎮 Game starting!
```

---

## 📞 Support Quick Access

**In-Game Help**:
- Main Menu → 4 (Network Multiplayer) → 4 (Help)

**Documentation**:
- `Docs/NETWORK_QUICKSTART.md`
- `Docs/NETWORK_MULTIPLAYER.md`

**Diagnostics**:
- Main Menu → 8 (Error Logs & Diagnostics)

---

**Remember**: Have fun and enjoy adventuring with friends! 🎮⚔️
