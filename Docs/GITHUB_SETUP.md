# GitHub Setup Guide for RPG Dungeon Crawler

This guide will help you set up two GitHub repositories for your game: one for stable releases and one for development.

## Repository Structure

You should create **TWO** repositories:

### 1. Release Repository (Stable Builds)
- **Name**: `Rpg_Dungeon` (or `Rpg_Dungeon_Releases`)
- **Branch**: `main`
- **Purpose**: Contains only tested, stable release versions
- **Visibility**: Public (recommended) or Private

### 2. Development Repository (Work in Progress)
- **Name**: `Rpg_Dungeon-Dev` (or `Rpg_Dungeon`)
- **Branch**: `dev` or `development`
- **Purpose**: Contains work-in-progress code for testing and development
- **Visibility**: Public or Private (depending on whether you want others to see development)

---

## Step-by-Step Setup

### Option A: Two Separate Repositories

#### 1. Create Release Repository
```bash
# Navigate to your project folder
cd D:\VS_Projects\Rpg_Dungeon

# Initialize git (if not already done)
git init

# Create .gitignore file (see below)
# Add files
git add .
git commit -m "Initial release v1.0.0"

# Create repository on GitHub (via web interface)
# Then connect it:
git remote add origin https://github.com/YOUR_USERNAME/Rpg_Dungeon.git
git branch -M main
git push -u origin main
```

#### 2. Create Development Repository
```bash
# Create a separate dev repository on GitHub
# Clone it or push your working code to it:
git remote add dev https://github.com/YOUR_USERNAME/Rpg_Dungeon-Dev.git
git push dev main:dev
```

### Option B: Single Repository with Branches (Recommended)

#### 1. Create Single Repository with Two Branches
```bash
# Navigate to your project folder
cd D:\VS_Projects\Rpg_Dungeon

# Initialize git (if not already done)
git init

# Create .gitignore file (see below)

# Create and setup main branch (for releases)
git add .
git commit -m "Initial release v1.0.0"

# Create repository on GitHub, then:
git remote add origin https://github.com/YOUR_USERNAME/Rpg_Dungeon.git
git branch -M main
git push -u origin main

# Create development branch
git checkout -b dev
git push -u origin dev

# Now you have:
# - main branch: for stable releases
# - dev branch: for work in progress
```

---

## Updating VersionControl.cs

After creating your repository, update the URLs in `Systems/VersionControl.cs`:

Replace `YOUR_USERNAME` with your actual GitHub username:

```csharp
public const string GitHubReleaseUrl = "https://github.com/YOUR_USERNAME/Rpg_Dungeon/releases";
public const string GitHubDevUrl = "https://github.com/YOUR_USERNAME/Rpg_Dungeon";
public const string GitHubVersionCheckUrl = "https://raw.githubusercontent.com/YOUR_USERNAME/Rpg_Dungeon/main/version.json";
```

---

## .gitignore File

Create a `.gitignore` file in your project root with the following content:

```gitignore
# Build results
[Bb]in/
[Oo]bj/
[Dd]ebug/
[Rr]elease/
x64/
x86/
[Aa]rm/
[Aa]rm64/
bld/
[Ll]og/
[Ll]ogs/

# Visual Studio
.vs/
*.suo
*.user
*.userosscache
*.sln.docstates
*.userprefs

# User-specific files
*.rsuser

# Save files (optional - exclude if you don't want to share saves)
save_*.json
mpsave_*.json

# Error logs (optional - include if you want to track issues)
# ErrorLogs/

# Compiled outputs
*.exe
*.dll
*.pdb

# NuGet
*.nupkg
*.snupkg
packages/
.nuget/

# Local configuration
*.local.json
```

---

## Workflow for Development and Releases

### Development Workflow (dev branch)

1. **Make changes in dev branch:**
   ```bash
   git checkout dev
   # Make your changes
   git add .
   git commit -m "Description of changes"
   git push origin dev
   ```

2. **Test thoroughly** before promoting to release

### Release Workflow (main branch)

1. **When dev version is stable and tested:**
   ```bash
   # Switch to main branch
   git checkout main
   
   # Merge dev into main
   git merge dev
   
   # Update version in VersionControl.cs (increase version number)
   # Update version.json file with new version info
   
   git add .
   git commit -m "Release v1.1.0"
   git push origin main
   ```

2. **Create a GitHub Release:**
   - Go to your repository on GitHub
   - Click "Releases" → "Create a new release"
   - Tag version: `v1.1.0`
   - Release title: `Version 1.1.0 - Feature Name`
   - Description: Release notes
   - Attach compiled game executable (from bin/Release)
   - Publish release

3. **Update version.json** on main branch:
   ```json
   {
     "majorVersion": 1,
     "minorVersion": 1,
     "patchVersion": 0,
     "preReleaseTag": null,
     "releaseNotes": "Added new features, fixed bugs, improved performance",
     "releaseDate": "2025-01-15"
   }
   ```

---

## How Update Checking Works

1. When players click "Check for Updates" in the main menu
2. The game fetches `version.json` from your GitHub repository
3. Compares it with the current version in `VersionControl.cs`
4. If a newer version exists, shows update notification
5. Players can click to open your GitHub releases page to download

---

## Quick Commands Reference

### Initial Setup
```bash
cd D:\VS_Projects\Rpg_Dungeon
git init
git add .
git commit -m "Initial commit"
git remote add origin https://github.com/YOUR_USERNAME/Rpg_Dungeon.git
git push -u origin main
git checkout -b dev
git push -u origin dev
```

### Daily Development (dev branch)
```bash
git checkout dev
git add .
git commit -m "Your change description"
git push origin dev
```

### Creating a Release (main branch)
```bash
git checkout main
git merge dev
# Update VersionControl.cs and version.json
git add .
git commit -m "Release v1.x.x"
git tag v1.x.x
git push origin main --tags
# Then create release on GitHub with compiled .exe
```

### Switching Between Branches
```bash
git checkout main   # Switch to release branch
git checkout dev    # Switch to development branch
```

---

## File Structure for GitHub

```
Rpg_Dungeon/
├── .gitignore                  # Git ignore rules
├── version.json                # Version info for update checking
├── README.md                   # Project description
├── Night.csproj                # Project file
├── Program.cs                  # Entry point
├── Systems/
│   ├── VersionControl.cs       # Version management
│   ├── UpdateChecker.cs        # Update checking logic
│   └── ...
├── Characters/
├── Items/
├── Combat/
└── ...
```

---

## Important Notes

1. **Never commit sensitive data** (passwords, API keys, personal info)
2. **Test in dev branch first** before merging to main
3. **Update version.json** on GitHub after each release
4. **Create GitHub releases** with compiled executables for easy downloading
5. **Keep version.json on main branch** so update checker works properly

---

## Creating Your First Release

1. **Commit and push your code to main branch**
2. **Build your project in Release mode** (not Debug)
3. **Go to GitHub** → Your repository → "Releases"
4. **Click "Create a new release"**
5. **Fill in:**
   - Tag: `v1.0.0`
   - Title: `RPG Dungeon Crawler v1.0.0`
   - Description: List of features
   - Upload: `Night.exe` and any required DLLs from bin/Release/
6. **Publish release**
7. **Update version.json** in your repository

---

## Need Help?

If you encounter issues:
- Check that URLs in VersionControl.cs match your GitHub username
- Ensure version.json is in the root of your main branch
- Verify your repository is public (if checking updates fails)
- Check the Error Logs in the game for detailed error messages

---

Good luck with your game! 🎮
