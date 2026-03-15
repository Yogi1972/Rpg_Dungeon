# Quick Release Workflow

This guide provides a quick reference for creating new releases.

## Before Each Release

### 1. Update Version Numbers

Edit `Systems/VersionControl.cs`:
```csharp
public const int MajorVersion = 1;  // Breaking changes
public const int MinorVersion = 1;  // New features
public const int PatchVersion = 0;  // Bug fixes
public const string PreReleaseTag = null;  // null for stable, "alpha"/"beta" for pre-release
public static readonly DateTime BuildDate = new DateTime(2025, 1, 15);  // Update date
```

### 2. Update version.json

Edit `version.json` in your repository root:
```json
{
  "majorVersion": 1,
  "minorVersion": 1,
  "patchVersion": 0,
  "preReleaseTag": null,
  "releaseNotes": "List new features, bug fixes, and improvements here",
  "releaseDate": "2025-01-15"
}
```

### 3. Test Everything

- Run the game thoroughly
- Test all new features
- Check multiplayer if changes affect it
- Verify save/load functionality

---

## Creating a Release

### Development Phase (dev branch)

```bash
# Work in dev branch
git checkout dev

# Make changes and commit frequently
git add .
git commit -m "Add new feature X"
git push origin dev

# Continue until feature is complete and tested
```

### Promoting to Release (main branch)

```bash
# Switch to main branch
git checkout main

# Merge dev into main
git merge dev

# Update VersionControl.cs (increase version)
# Update version.json with new version info

# Commit the release
git add .
git commit -m "Release v1.1.0 - Feature description"

# Create version tag
git tag -a v1.1.0 -m "Version 1.1.0"

# Push to GitHub
git push origin main --tags
```

### Building Release Executable

In Visual Studio:
1. Right-click project → Properties
2. Change Configuration to **Release**
3. Build → Build Solution
4. Executable will be in `bin/Release/net10.0/`

### Creating GitHub Release

1. Go to: `https://github.com/YOUR_USERNAME/Rpg_Dungeon/releases`
2. Click **"Create a new release"**
3. Fill in:
   - **Tag**: `v1.1.0` (select from dropdown or create new)
   - **Title**: `RPG Dungeon Crawler v1.1.0 - Feature Name`
   - **Description**: 
     ```
     ## What's New
     - New feature 1
     - New feature 2
     - Bug fix 1
     
     ## Changes
     - Improvement 1
     - Improvement 2
     
     ## Download
     Download Night.exe and run it. Requires .NET 10 Runtime.
     ```
4. **Attach files**: Upload `Night.exe` from bin/Release/net10.0/
5. Click **"Publish release"**

### Update version.json on GitHub

```bash
# Make sure you're on main branch
git checkout main

# Edit version.json with new version info
# Commit and push
git add version.json
git commit -m "Update version info for v1.1.0"
git push origin main
```

---

## Version Numbering Guide

Use [Semantic Versioning](https://semver.org/):

- **MAJOR** (1.x.x): Breaking changes, major rewrites
  - Example: Changing save file format incompatibly
  
- **MINOR** (x.1.x): New features, backward compatible
  - Example: Adding new character class, new dungeon type
  
- **PATCH** (x.x.1): Bug fixes, minor improvements
  - Example: Fixing combat calculation bug, typos

### Pre-release Tags
- `alpha`: Early development, unstable
- `beta`: Feature complete, testing phase
- `rc1`, `rc2`: Release candidates
- `null`: Stable release

---

## Quick Commands for Common Tasks

### Start New Feature (in dev)
```bash
git checkout dev
git pull origin dev
# Make changes
git add .
git commit -m "Add [feature name]"
git push origin dev
```

### Fix Bug (in dev)
```bash
git checkout dev
# Fix bug
git add .
git commit -m "Fix [bug description]"
git push origin dev
```

### Hotfix for Release
```bash
# If critical bug in released version
git checkout main
# Fix the bug
git add .
git commit -m "Hotfix: [bug description]"
git tag -a v1.0.1 -m "Hotfix release"
git push origin main --tags
# Create GitHub release
# Then merge back to dev
git checkout dev
git merge main
git push origin dev
```

### Check Status
```bash
git status                    # See what's changed
git log --oneline -10        # See recent commits
git branch                   # See current branch
```

---

## Troubleshooting

### "Remote already exists" error
```bash
git remote remove origin
git remote add origin https://github.com/YOUR_USERNAME/Rpg_Dungeon.git
```

### "Updates were rejected" error
```bash
git pull origin main --rebase
git push origin main
```

### Need to undo last commit
```bash
git reset --soft HEAD~1      # Undo commit, keep changes
git reset --hard HEAD~1      # Undo commit, discard changes
```

---

## Checklist Before Each Release

- [ ] All tests pass
- [ ] Game runs without errors
- [ ] VersionControl.cs updated with new version
- [ ] version.json updated with release notes
- [ ] Changes committed to dev branch
- [ ] Dev branch merged to main
- [ ] Version tagged
- [ ] Pushed to GitHub with tags
- [ ] Built in Release mode
- [ ] GitHub release created with executable
- [ ] Release notes written
- [ ] Announcement made (if applicable)

---

## Need Help?

See **GITHUB_SETUP.md** for comprehensive setup instructions.
