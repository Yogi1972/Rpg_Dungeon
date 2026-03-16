# 🚀 Creating a GitHub Release for v3.1.0-beta

**Purpose:** Make the in-game update checker work by creating an official GitHub release

---

## 📋 Quick Steps

### 1. Go to GitHub Repository
Navigate to: https://github.com/Yogi1972/Rpg_Dungeon/releases

### 2. Click "Create a new release"
Or go directly to: https://github.com/Yogi1972/Rpg_Dungeon/releases/new

### 3. Fill in Release Information

#### Tag Version
```
v3.1.0-beta
```
**Important:** Must start with "v" for the update checker to work!

#### Release Title
```
v3.1.0-beta - Enhanced Combat System (Beta Testing)
```

#### Target
```
main (leave as default)
```

#### Description
Copy and paste from `Docs/RELEASE_NOTES_v3.1.0-beta.md` or use this:

```markdown
## 🌟 What's New in v3.1.0-beta

### ⚔️ MAJOR FEATURE: Enhanced Combat System

This is the biggest combat overhaul in the game's history!

**16 Unique Class Abilities:**
- 🗡️ Warrior: Power Strike, Defensive Stance, Whirlwind Attack, Intimidating Shout
- 🔮 Mage: Fireball, Ice Bolt, Mana Shield, Lightning Storm
- 🗡️ Rogue: Backstab, Poison Blade, Shadow Step, Fan of Knives
- ✝️ Priest: Holy Smite, Divine Shield, Healing Prayer, Wrath

**8 Status Effect Types:**
🩸 Bleeding | 💫 Stunned | ☠️ Poisoned | 🔥 Burning | ❄️ Frozen | ⬇️ Weakened | 🎯 Vulnerable | 💚 Regenerating

**New Combat Features:**
- Ability Hotkeys (A1-A4) for quick access
- Enhanced combat UI with ability display
- Resource management (Mana/Stamina)
- Cooldown system for strategic gameplay
- Status effect visualization
- Smart target selection

**UI Improvements:**
- Beautiful bordered combat display
- Real-time status effect tracking
- Clear ability availability indicators
- Resource and cooldown displays

## ⚠️ Beta Testing Notice

This is a **BETA RELEASE** for testing. Please report:
- Bugs or unexpected behavior
- Balance issues
- UI/UX feedback
- Performance concerns

## 📖 Full Release Notes

See `Docs/RELEASE_NOTES_v3.1.0-beta.md` for complete details.

## 🔄 Save Compatibility

✅ Saves from v3.0.x are fully compatible
✅ Characters will auto-learn abilities
✅ No data loss

## 🎮 How to Use

1. Start or load a game
2. Enter combat
3. Look for "⚔️ ABILITIES:" section
4. Type A1, A2, A3, or A4 to use abilities
5. Manage resources and cooldowns!

## 📞 Feedback

Report issues at: https://github.com/Yogi1972/Rpg_Dungeon/issues

**Happy Testing!** ⚔️✨
```

#### Release Options
- ✅ **Check:** "This is a pre-release" (since it's beta)
- ✅ **Check:** "Set as the latest release"
- ❌ **Uncheck:** "Create a discussion for this release" (optional)

### 4. Attach Build (Optional but Recommended)

If you want to provide a compiled binary:

**Option A: Quick Build**
```powershell
# Build release version
dotnet publish -c Release -o ./publish

# Create a zip
Compress-Archive -Path ./publish/* -DestinationPath RPG_Dungeon_v3.1.0-beta.zip
```

Then drag and drop `RPG_Dungeon_v3.1.0-beta.zip` onto the release assets section.

**Option B: Let Users Build**
Just provide the source code (GitHub does this automatically)

### 5. Click "Publish release"

---

## ✅ Verification

After publishing, verify the update checker works:

### 1. Check the API
Visit: https://api.github.com/repos/Yogi1972/Rpg_Dungeon/releases/latest

You should see:
```json
{
  "tag_name": "v3.1.0-beta",
  "name": "v3.1.0-beta - Enhanced Combat System (Beta Testing)",
  ...
}
```

### 2. Test In-Game
1. Run the game
2. Game should show at startup: "🔍 Checking for updates..."
3. Should detect your current version (v3.1.0-beta)
4. If on older version, shows: "🎉 NEW UPDATE AVAILABLE!"

---

## 🎯 How the Update Checker Works

### Your Code (UpdateChecker.cs)
```csharp
public const string GitHubVersionCheckUrl = 
    "https://api.github.com/repos/Yogi1972/Rpg_Dungeon/releases/latest";
```

### What It Does
1. Queries GitHub API for latest release
2. Reads the `tag_name` field (e.g., "v3.1.0-beta")
3. Parses version numbers (Major: 3, Minor: 1, Patch: 0)
4. Compares with current version in VersionControl.cs
5. Shows update notification if newer version exists

### Version Format
The tag MUST follow this format:
- `vX.Y.Z` (e.g., v3.1.0)
- `vX.Y.Z-tag` (e.g., v3.1.0-beta)

Where:
- X = Major version
- Y = Minor version
- Z = Patch version
- tag = Optional pre-release tag (alpha, beta, rc1, etc.)

---

## 🔧 Troubleshooting

### Update Checker Not Working?

**Problem:** "Unable to check for updates"
**Solutions:**
1. Ensure release is published (not draft)
2. Check tag format (must start with "v")
3. Verify API URL is correct
4. Check internet connection
5. Wait a few minutes (GitHub API cache)

**Problem:** Shows old version
**Solutions:**
1. Uncheck "Set as the latest release" on old releases
2. Only the latest release is checked
3. Ensure new release tag is higher version number

**Problem:** Shows "No releases published yet"
**Solutions:**
1. Create a release (not just a tag)
2. Releases are different from tags
3. Must be a full release, not draft

---

## 📊 Release Checklist

Use this checklist when creating releases:

- [ ] Version number updated in VersionControl.cs
- [ ] Code committed and pushed to GitHub
- [ ] Release notes written
- [ ] Tag version follows vX.Y.Z format
- [ ] Release title is descriptive
- [ ] Description includes key features
- [ ] "This is a pre-release" checked (if beta/alpha)
- [ ] "Set as the latest release" checked
- [ ] Release published (not draft)
- [ ] API endpoint returns correct version
- [ ] In-game update checker tested

---

## 🎉 You're Done!

Once the release is published:

✅ Your code is on GitHub  
✅ Update checker will work  
✅ Users can download the release  
✅ Automatic update notifications  

### Next Steps

1. **Test the game** - Make sure everything works
2. **Gather feedback** - From beta testers
3. **Fix bugs** - Based on reports
4. **Iterate** - Improve based on feedback
5. **Plan v3.2** - Turn order & combos!

---

## 💡 Tips for Future Releases

### Version Numbering
- **Major (X):** Breaking changes (v3 → v4)
- **Minor (Y):** New features (v3.0 → v3.1)
- **Patch (Z):** Bug fixes (v3.1.0 → v3.1.1)

### Pre-release Tags
- **alpha:** Very early, unstable
- **beta:** Feature complete, needs testing
- **rc1, rc2:** Release candidates, almost ready
- **(none):** Stable release

### Best Practices
1. Always update VersionControl.cs first
2. Write release notes before publishing
3. Test update checker after release
4. Announce on Discord/community
5. Monitor for issues

---

## 🔗 Quick Links

- **Repository:** https://github.com/Yogi1972/Rpg_Dungeon
- **Releases:** https://github.com/Yogi1972/Rpg_Dungeon/releases
- **New Release:** https://github.com/Yogi1972/Rpg_Dungeon/releases/new
- **Issues:** https://github.com/Yogi1972/Rpg_Dungeon/issues
- **API Check:** https://api.github.com/repos/Yogi1972/Rpg_Dungeon/releases/latest

---

**Ready to create your release?**

**Go to:** https://github.com/Yogi1972/Rpg_Dungeon/releases/new

**And follow the steps above!** 🚀

