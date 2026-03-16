# Quick script to create v3.1.0-beta release on GitHub
# This will open your browser to the release creation page

$releaseUrl = "https://github.com/Yogi1972/Rpg_Dungeon/releases/new?tag=v3.1.0-beta&title=v3.1.0-beta%20-%20Enhanced%20Combat%20System%20(Beta%20Testing)&prerelease=1"

Write-Host "================================" -ForegroundColor Cyan
Write-Host "Creating GitHub Release" -ForegroundColor Cyan
Write-Host "================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "Opening GitHub release page..." -ForegroundColor Green
Write-Host ""
Write-Host "Tag: v3.1.0-beta" -ForegroundColor Yellow
Write-Host "Title: v3.1.0-beta - Enhanced Combat System (Beta Testing)" -ForegroundColor Yellow
Write-Host ""
Write-Host "You'll need to:" -ForegroundColor White
Write-Host "  1. Check 'This is a pre-release'" -ForegroundColor White
Write-Host "  2. Add the description (see below)" -ForegroundColor White
Write-Host "  3. Click 'Publish release'" -ForegroundColor White
Write-Host ""
Write-Host "Press any key to open GitHub..." -ForegroundColor Cyan
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")

# Open browser
Start-Process $releaseUrl

Write-Host ""
Write-Host "================================" -ForegroundColor Cyan
Write-Host "Release Description (Copy This)" -ForegroundColor Cyan
Write-Host "================================" -ForegroundColor Cyan
Write-Host ""

$description = @"
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

See ``Docs/RELEASE_NOTES_v3.1.0-beta.md`` for complete details.

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

## 📊 Statistics

- **Files Changed:** 13
- **Lines Added:** 3,408+
- **New Abilities:** 16 (4 per class)
- **Status Effects:** 8 types
- **Progress:** Step 1 - 60% complete

## 📞 Feedback

Report issues at: https://github.com/Yogi1972/Rpg_Dungeon/issues

**Happy Testing!** ⚔️✨
"@

Write-Host $description -ForegroundColor White
Write-Host ""
Write-Host "================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "The description has been copied to your clipboard!" -ForegroundColor Green
Set-Clipboard -Value $description

Write-Host "Just paste it into the description field on GitHub!" -ForegroundColor Yellow
Write-Host ""
Write-Host "Release URL: $releaseUrl" -ForegroundColor Gray
