# Windows Defender / SmartScreen Warning - How to Run RPG Dungeon Crawler

## ⚠️ Why does Windows flag this game?

**This is a FALSE POSITIVE** - Your game is completely safe!

Windows Defender SmartScreen flags the game because:
1. ✅ The executable is **not digitally signed** (code signing certificates cost $300+/year)
2. ✅ It's a **new release** without enough download history
3. ✅ Self-contained executables are often flagged by default

**This is open-source software** - You can view all the code at:  
🔗 https://github.com/Yogi1972/Rpg_Dungeon

---

## 🛡️ How to Run the Game Safely

### Method 1: Windows SmartScreen Bypass (Recommended)

When you try to run `ConsoleApplication.exe`, Windows will show a warning:

1. Click "**More info**" on the SmartScreen warning
2. Click "**Run anyway**" button
3. The game will start normally

**Screenshot Guide:**
```
┌─────────────────────────────────────────┐
│  Windows protected your PC              │
│                                         │
│  Windows Defender SmartScreen prevented │
│  an unrecognized app from starting.     │
│                                         │
│  [More info]                            │  ← Click this!
└─────────────────────────────────────────┘

Then:
┌─────────────────────────────────────────┐
│  App: ConsoleApplication.exe            │
│  Publisher: Unknown publisher           │
│                                         │
│  [Run anyway]                           │  ← Click this!
│  [Don't run]                            │
└─────────────────────────────────────────┘
```

### Method 2: Exclude from Windows Defender

1. Open **Windows Security** (Search in Start menu)
2. Go to **Virus & threat protection**
3. Click "**Manage settings**" under Virus & threat protection settings
4. Scroll to "**Exclusions**" and click "**Add or remove exclusions**"
5. Click "**Add an exclusion**" → Choose "**Folder**"
6. Select the folder where you extracted the game
7. Try running the game again

### Method 3: Unblock the File

1. Right-click `ConsoleApplication.exe`
2. Select "**Properties**"
3. At the bottom, check "**Unblock**" if present
4. Click "**Apply**" then "**OK**"
5. Run the game

### Method 4: Use Framework-Dependent Version (Coming Soon)

We're working on a framework-dependent version that requires .NET 10 installed but is less likely to be flagged.

---

## 🔒 Is This Really Safe?

**YES!** Here's why you can trust this:

✅ **Open Source** - All code is publicly visible on GitHub  
✅ **No Network Activity** - Game runs completely offline (except optional update checks)  
✅ **No Administrator Required** - Doesn't need elevated permissions  
✅ **Clean Code** - No malware, spyware, or suspicious behavior  
✅ **Scan It Yourself** - Upload to VirusTotal.com to verify  

The warnings are due to Microsoft's publisher trust system, not actual threats.

---

## 💡 For Developers: Why This Happens

This game is distributed as a **self-contained single-file executable** which:
- Bundles .NET runtime (30+ MB)
- Uses compression and extraction
- Modifies itself at runtime (extract embedded DLLs)

These behaviors trigger heuristic-based detection in Windows Defender, even though they're standard .NET publishing features.

### To Eliminate Warnings (requires investment):
1. **Purchase a Code Signing Certificate** ($300-500/year from DigiCert, Sectigo, etc.)
2. **Sign the executable** with `signtool.exe`
3. **Build reputation** over time as users download and run it

---

## 📧 Still Concerned?

- **View the source code**: https://github.com/Yogi1972/Rpg_Dungeon
- **Report issues**: https://github.com/Yogi1972/Rpg_Dungeon/issues
- **Contact developer**: Create an issue on GitHub

---

## 🎮 After Bypassing the Warning

Once you run the game:
1. It will create saves in `%AppData%\RPG_Dungeon\`
2. Error logs go to `%AppData%\RPG_Dungeon\Logs\`
3. No system changes are made
4. Completely portable - can be moved anywhere

---

**Thank you for understanding! This is standard for indie games without code signing certificates.**

🎮 **Enjoy RPG Dungeon Crawler v2.0.0!**
