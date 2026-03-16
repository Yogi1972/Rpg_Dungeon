# Network Multiplayer System

## Overview
The RPG Dungeon Crawler now supports **true LAN network multiplayer**, allowing multiple players on different computers to connect and play together over a local network.

## Features

### 🌐 **Network Connectivity**
- **TCP-based** client-server architecture
- **Host/Join** system for easy game setup
- **Real-time** message synchronization
- Support for up to **4 players** per game

### 🎮 **Multiplayer Modes**

#### 1. **Local Multiplayer (Hotseat)**
- Multiple players on the **same computer**
- Take turns controlling different characters
- No network required

#### 2. **Network Multiplayer (LAN)**
- Players connect over **local network**
- Each player has their own computer
- Host controls game flow
- Clients can join and leave dynamically

## Getting Started

### Hosting a Game

1. **Launch the game** and select `4) 🌐 Network Multiplayer (LAN)` from the main menu
2. Choose `1) 🏠 Host a Game`
3. Enter a **port number** (default: 7777) or press Enter
4. Your **IP address** will be displayed - share this with other players
5. **Create your character** (class, name, etc.)
6. Wait for players to join
7. Type `start` when ready to begin the adventure

### Joining a Game

1. **Launch the game** and select `4) 🌐 Network Multiplayer (LAN)` from the main menu
2. Choose `2) 🔌 Join a Game`
3. Enter the **host's IP address** (e.g., `192.168.1.100`)
4. Enter the **port number** (default: 7777)
5. **Create your character**
6. Wait for the host to start the game

### Finding Your IP Address

Use option `3) 📋 Show My IP Address` to display your local network IP address.

Example IP addresses:
- `192.168.1.xxx`
- `192.168.0.xxx`
- `10.0.0.xxx`

## Network Requirements

### ✅ **Required**
- All players must be on the **same local network** (LAN)
  - Same Wi-Fi network
  - Same wired network
  - Connected through the same router
- **Port 7777** must be available (or specify custom port)

### ⚠️ **Firewall Settings**
If players cannot connect:
1. Check **Windows Firewall** settings
2. Allow `Rpg_Dungeon.exe` through the firewall
3. Ensure **port 7777** (or your custom port) is not blocked

#### Windows Firewall - Quick Fix
```powershell
# Run as Administrator
New-NetFirewallRule -DisplayName "RPG Dungeon" -Direction Inbound -Program "C:\Path\To\Rpg_Dungeon.exe" -Action Allow
```

## Network Architecture

### Message Protocol
The system uses JSON-based message protocol over TCP:

```json
{
  "Type": "CombatAction",
  "SenderId": "player-guid",
  "Data": "{...}",
  "Timestamp": "2024-01-01T12:00:00Z"
}
```

### Message Types
- **PlayerJoin** - Player connects to lobby
- **PlayerLeave** - Player disconnects
- **LobbyUpdate** - Lobby state changes
- **GameStart** - Host starts the game
- **TurnStart** - Player's turn begins
- **TurnEnd** - Player's turn ends
- **CombatAction** - Player performs action
- **CombatResult** - Action results broadcast
- **GameStateSync** - Full game state update
- **ChatMessage** - Player chat
- **Ping/Pong** - Connection health check
- **Disconnect** - Clean disconnect

## Host Commands

While in the lobby, the host can use these commands:

| Command | Description |
|---------|-------------|
| `start` | Start the game with current players |
| `list` | Show all connected players |
| `kick [name]` | Remove a player from the lobby |
| `cancel` | Close server and return to menu |

## Troubleshooting

### Cannot Connect to Host

**Problem**: "Failed to connect to host"

**Solutions**:
1. Verify both computers are on the same network
2. Check the IP address is correct
3. Ensure port 7777 is not blocked by firewall
4. Try temporarily disabling antivirus
5. Verify host has actually started the server

### Connection Drops During Game

**Problem**: Player gets disconnected mid-game

**Solutions**:
1. Check network stability (Wi-Fi signal strength)
2. Ensure both computers remain on the network
3. Check for network interference
4. Try using wired connection instead of Wi-Fi

### Cannot Find IP Address

**Problem**: Host IP address shows as "127.0.0.1"

**Solutions**:
1. Use command prompt: `ipconfig` (Windows) or `ifconfig` (Mac/Linux)
2. Look for "IPv4 Address" under your active network adapter
3. Should look like `192.168.x.x` or `10.0.x.x`

### Firewall Blocking Connection

**Problem**: Firewall prevents connection

**Solutions**:
1. Open **Windows Defender Firewall**
2. Click **Allow an app through firewall**
3. Click **Change settings** → **Allow another app**
4. Browse and add `Rpg_Dungeon.exe`
5. Enable for **Private networks**

## Technical Details

### Network Classes

#### `NetworkManager`
- Handles TCP connections
- Manages client-server communication
- Event-driven message handling

#### `NetworkMessage`
- JSON serializable message format
- Type-safe message protocol
- Timestamp tracking

#### `NetworkLobby`
- Pre-game lobby management
- Player list synchronization
- Game start coordination

#### `NetworkMultiplayerManager`
- High-level game coordination
- Character creation for network play
- Lobby command handling

### Default Settings

| Setting | Value |
|---------|-------|
| Default Port | 7777 |
| Max Players | 4 |
| Buffer Size | 4096 bytes |
| Connection Timeout | 30 seconds |
| Message Format | JSON (UTF-8) |

## Future Enhancements

### Planned Features
- [ ] **Full combat synchronization** - Real-time combat state sync
- [ ] **Turn-based coordination** - Managed turn order across network
- [ ] **Chat system** - In-game text chat
- [ ] **Player reconnection** - Rejoin after disconnect
- [ ] **Save/Load network games** - Resume network sessions
- [ ] **Spectator mode** - Watch games in progress
- [ ] **Internet play** - Connect over WAN (with port forwarding)
- [ ] **Dedicated server** - Headless server mode
- [ ] **Latency compensation** - Smooth gameplay over slower connections

### Experimental Features (Not Yet Implemented)
- Voice chat integration
- Cross-platform play
- Persistent online lobbies
- Matchmaking system

## Security Considerations

⚠️ **Important**: This network system is designed for **trusted local networks only**.

### Current Security Status
- ✅ Local network only (LAN)
- ✅ No sensitive data transmission
- ⚠️ No encryption (plaintext TCP)
- ⚠️ No authentication system
- ⚠️ Not suitable for public internet

### Best Practices
1. Only play with **trusted friends**
2. Use on **private networks** only
3. Do NOT expose to public internet without VPN
4. Keep game version updated

## FAQ

**Q: Can I play over the internet?**
A: Not directly. The system is designed for local networks only. You could use a VPN (like Hamachi or ZeroTier) to create a virtual LAN.

**Q: How many players can join?**
A: Up to 4 players maximum (configurable in code).

**Q: Can I save network multiplayer games?**
A: Not yet - this feature is planned for future updates.

**Q: What happens if a player disconnects?**
A: Currently, the game continues without them. Reconnection is a planned feature.

**Q: Can I change the port?**
A: Yes, when hosting you can specify a custom port instead of the default 7777.

**Q: Do I need a special network setup?**
A: No, as long as all players are on the same Wi-Fi or wired network, it should work.

## Support

### Reporting Issues
If you encounter network issues:
1. Use **Error Logs & Diagnostics** menu (option 8)
2. Check firewall settings
3. Verify network connectivity
4. Create error report and contact developer

### Getting Help
- Check the **Network Multiplayer Help** in-game (option 4)
- Read this documentation thoroughly
- Test with local host (127.0.0.1) first
- Verify single-player works correctly

## Credits

**Network Multiplayer System** developed for RPG Dungeon Crawler
- TCP/IP networking via .NET Socket library
- JSON serialization for message protocol
- Event-driven architecture for scalability

---

**Version**: 1.0.0  
**Last Updated**: 2024  
**Tested On**: Windows 10/11, .NET 10
