# Quick Start Guide: Network Multiplayer

## 🚀 5-Minute Setup

### For the Host (Player 1)

1. **Start the game**
2. Main Menu → `4) 🌐 Network Multiplayer (LAN)`
3. Choose `1) 🏠 Host a Game`
4. Press **Enter** to use default port (7777)
5. **Write down your IP address** (shown on screen)
6. Create your character
7. Share your IP with friends
8. When everyone has joined, type `start`

**Example**:
```
📡 Your IP Address: 192.168.1.100
📡 Port: 7777
```

### For Players Joining (Player 2, 3, 4)

1. **Start the game**
2. Main Menu → `4) 🌐 Network Multiplayer (LAN)`
3. Choose `2) 🔌 Join a Game`
4. Enter host's IP address: `192.168.1.100`
5. Press **Enter** for default port (7777)
6. Create your character
7. Wait for host to start

## 📝 Common Scenarios

### Scenario 1: Playing at Home (2 PCs, same Wi-Fi)
✅ **This works!**
- Host on PC #1
- Join from PC #2
- Both connected to same Wi-Fi router

### Scenario 2: Playing with Friends (LAN Party)
✅ **This works!**
- All PCs connected to same router (wired or wireless)
- One person hosts
- Others join using host's IP

### Scenario 3: Playing over Internet
❌ **Not supported directly**
- Use VPN software (Hamachi, ZeroTier, etc.)
- Or wait for future internet support

## 🐛 Quick Fixes

### "Failed to connect"
1. Check if both computers are on same network
2. Verify IP address is correct
3. Windows Firewall → Allow Rpg_Dungeon.exe

### "Cannot start server"
1. Port 7777 might be in use
2. Try a different port (e.g., 7778)
3. Check firewall settings

### "Don't know my IP"
1. In-game: `3) 📋 Show My IP Address`
2. Windows Command Prompt: `ipconfig`
3. Look for `192.168.x.x` or `10.0.x.x`

## 💡 Tips

- **Use wired connection** for best stability
- **Host should have strong network connection**
- **Coordinate via voice chat** (Discord, etc.) while playing
- **Test with 2 players first** before adding more

## ⚡ Quick Test (Same Computer)

Want to test network multiplayer on one PC?

**Host**:
- Start game → Host → Use IP `127.0.0.1`

**Join**:
- Open second game instance
- Join → Enter `127.0.0.1`

This uses "localhost" to connect to yourself!

---

**Ready to play? Start with option 4 in the main menu!** 🎮
