# RPG Dungeon Crawler

A text-based RPG dungeon crawler adventure game built with .NET 10.0.

[![.NET Version](https://img.shields.io/badge/.NET-10.0-blue)](https://dotnet.microsoft.com/)
[![Version](https://img.shields.io/badge/version-3.0.0-green)](https://github.com/Yogi1972/Rpg_Dungeon/releases)
[![License](https://img.shields.io/badge/license-Open%20Source-orange)](https://github.com/Yogi1972/Rpg_Dungeon)

## 🎮 For Players - Quick Start

### Download & Play (No coding required!)

**👉 [Download Latest Release (v3.0.0)](https://github.com/Yogi1972/Rpg_Dungeon/releases/latest)**

1. Download `RPG_Dungeon_v3.0.0_Windows.zip`
2. Extract the ZIP file to a folder
3. Read `WINDOWS_DEFENDER_HELP.md` (Important!)
4. Run `ConsoleApplication.exe`
5. Start your adventure!

### ⚠️ Windows Defender Warning

Windows may show a security warning. **This is a false positive.**

**Quick Fix:**
- Click "**More info**" → "**Run anyway**"

See `WINDOWS_DEFENDER_HELP.md` in the download for detailed instructions.

---

## 🎯 Game Features

### Character Classes
Choose from 4 main classes, each with 3 advanced specializations:
- **Warrior** → Paladin, Berserker, Guardian
- **Mage** → Archmage, Necromancer, Elementalist
- **Rogue** → Assassin, Ranger, Shadowblade
- **Priest** → Templar, Druid, Oracle

### Gameplay Features
- **Dynamic Combat System**: Turn-based combat with strategic decisions
- **Loot & Equipment**: 150+ items, weapons, armor, and accessories
- **World Generation**: Seed-based procedural world generation
- **Dungeon Crawling**: Explore procedurally generated dungeons
- **Enemy Variety**: 30+ unique monster types with scaling difficulty
- **Party Management**: Build and manage your adventuring party
- **Character Progression**: Level up and unlock advanced classes

---

## 💻 For Developers

### Requirements
- Visual Studio 2026 (or Visual Studio 2022 with .NET 10 SDK)
- .NET 10.0 SDK
- Windows 10/11 (64-bit)

### Building from Source

```bash
# Clone the repository
git clone https://github.com/Yogi1972/Rpg_Dungeon.git
cd Rpg_Dungeon

# Build and run
dotnet build
dotnet run --project Night.csproj

# Or open Night.csproj in Visual Studio
```

### Project Structure

```
Rpg_Dungeon/
├── Characters/        # Character classes and progression
├── Combat/           # Combat system and mob generation
├── World/            # World generation and dungeons
├── Systems/          # Core game systems
├── Docs/             # Documentation
└── Night.csproj      # Main project file
```

### Publishing Standalone Build

```bash
dotnet publish Night.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -p:EnableCompressionInSingleFile=true -o publish/v3.0.0
```

---

## 📚 Documentation

- [Loot System Enhancement](Docs/LOOT_SYSTEM_ENHANCEMENT.md)
- [Dynamic World System](Docs/DYNAMIC_WORLD_SYSTEM.md)
- [Seed System Guide](Docs/SEED_SYSTEM_GUIDE.md)
- [NPC Directory](Docs/NPC_DIRECTORY.md)
- [Encounter System](Docs/ENCOUNTER_SYSTEM_README.md)
- [Error Logging](Docs/ERROR_LOGGING_README.md)

---

## 🐛 Bug Reports & Feature Requests

Found a bug or have a suggestion? Please [open an issue](https://github.com/Yogi1972/Rpg_Dungeon/issues).

---

## 📝 Version History

- **v3.0.0** (Latest) - Dynamic loot, world generation, enhanced content
- **v2.0.0** - Major features and improvements
- **v1.1.0** - Beta release
- **v1.0.0** - Initial alpha release

---

## 🙏 Credits

Developed by Yogi

---

**Happy adventuring!** ⚔️🛡️🗡️
