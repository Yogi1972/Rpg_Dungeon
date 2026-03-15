# GitHub Setup Script for RPG Dungeon Crawler
# This script helps automate the GitHub repository setup

Write-Host "╔══════════════════════════════════════════════════════════════════╗" -ForegroundColor Cyan
Write-Host "║           RPG Dungeon Crawler - GitHub Setup Script             ║" -ForegroundColor Cyan
Write-Host "╚══════════════════════════════════════════════════════════════════╝" -ForegroundColor Cyan
Write-Host ""

# Check if git is installed
try {
    $gitVersion = git --version
    Write-Host "✅ Git is installed: $gitVersion" -ForegroundColor Green
} catch {
    Write-Host "❌ Git is not installed!" -ForegroundColor Red
    Write-Host "   Please download and install Git from: https://git-scm.com/download/win" -ForegroundColor Yellow
    Write-Host ""
    Read-Host "Press Enter to exit"
    exit
}

Write-Host ""
Write-Host "This script will help you set up GitHub repositories for your game." -ForegroundColor Cyan
Write-Host ""

# Get GitHub username
Write-Host "Step 1: GitHub Username" -ForegroundColor Yellow
Write-Host "════════════════════════════════════════════════════════════════════"
$username = Read-Host "Enter your GitHub username"

if ([string]::IsNullOrWhiteSpace($username)) {
    Write-Host "❌ Username cannot be empty!" -ForegroundColor Red
    Read-Host "Press Enter to exit"
    exit
}

Write-Host ""
Write-Host "Step 2: Repository Setup Option" -ForegroundColor Yellow
Write-Host "════════════════════════════════════════════════════════════════════"
Write-Host "Choose your setup option:"
Write-Host "  1) Single repository with 'main' and 'dev' branches (Recommended)"
Write-Host "  2) Two separate repositories"
Write-Host ""
$setupOption = Read-Host "Choose option (1 or 2)"

Write-Host ""
Write-Host "Step 3: Repository Name" -ForegroundColor Yellow
Write-Host "════════════════════════════════════════════════════════════════════"
$repoName = Read-Host "Enter repository name (default: Rpg_Dungeon)"
if ([string]::IsNullOrWhiteSpace($repoName)) {
    $repoName = "Rpg_Dungeon"
}

Write-Host ""
Write-Host "═══════════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host "                        SETUP SUMMARY" -ForegroundColor Cyan
Write-Host "═══════════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host "  GitHub Username: $username" -ForegroundColor White
Write-Host "  Repository Name: $repoName" -ForegroundColor White
if ($setupOption -eq "1") {
    Write-Host "  Setup Type: Single repository (main + dev branches)" -ForegroundColor White
} else {
    Write-Host "  Setup Type: Two separate repositories" -ForegroundColor White
}
Write-Host ""

# Update VersionControl.cs
Write-Host "📝 Updating VersionControl.cs with your GitHub URLs..." -ForegroundColor Cyan

$versionControlPath = "Systems\VersionControl.cs"
if (Test-Path $versionControlPath) {
    $content = Get-Content $versionControlPath -Raw
    $content = $content -replace 'YOUR_USERNAME', $username
    Set-Content $versionControlPath $content
    Write-Host "✅ VersionControl.cs updated successfully!" -ForegroundColor Green
} else {
    Write-Host "⚠️  Warning: Could not find $versionControlPath" -ForegroundColor Yellow
}

Write-Host ""
Write-Host "Step 4: Initialize Git Repository" -ForegroundColor Yellow
Write-Host "════════════════════════════════════════════════════════════════════"
Write-Host ""

# Check if git is already initialized
if (Test-Path ".git") {
    Write-Host "⚠️  Git repository already exists!" -ForegroundColor Yellow
    $reinit = Read-Host "Do you want to reconfigure it? (Y/N)"
    if ($reinit -ne "Y" -and $reinit -ne "y") {
        Write-Host "Skipping git initialization..." -ForegroundColor Yellow
    } else {
        Remove-Item ".git" -Recurse -Force
        git init
        Write-Host "✅ Git repository reinitialized!" -ForegroundColor Green
    }
} else {
    git init
    Write-Host "✅ Git repository initialized!" -ForegroundColor Green
}

Write-Host ""
Write-Host "Step 5: Create Initial Commit" -ForegroundColor Yellow
Write-Host "════════════════════════════════════════════════════════════════════"
Write-Host ""

git add .
git commit -m "Initial commit - RPG Dungeon Crawler v1.0.0"
Write-Host "✅ Initial commit created!" -ForegroundColor Green

Write-Host ""
Write-Host "═══════════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host "                    NEXT STEPS (MANUAL)" -ForegroundColor Cyan
Write-Host "═══════════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host ""

if ($setupOption -eq "1") {
    Write-Host "1️⃣  Create repository on GitHub:" -ForegroundColor Yellow
    Write-Host "   • Go to: https://github.com/new" -ForegroundColor White
    Write-Host "   • Repository name: $repoName" -ForegroundColor White
    Write-Host "   • Make it Public (recommended for update checking)" -ForegroundColor White
    Write-Host "   • Do NOT initialize with README, .gitignore, or license" -ForegroundColor White
    Write-Host "   • Click 'Create repository'" -ForegroundColor White
    Write-Host ""
    
    Write-Host "2️⃣  Push your code to GitHub:" -ForegroundColor Yellow
    Write-Host "   Run these commands:" -ForegroundColor White
    Write-Host "   ────────────────────────────────────────────────────────────────" -ForegroundColor DarkGray
    Write-Host "   git remote add origin https://github.com/$username/$repoName.git" -ForegroundColor Cyan
    Write-Host "   git branch -M main" -ForegroundColor Cyan
    Write-Host "   git push -u origin main" -ForegroundColor Cyan
    Write-Host "   ────────────────────────────────────────────────────────────────" -ForegroundColor DarkGray
    Write-Host ""
    
    Write-Host "3️⃣  Create development branch:" -ForegroundColor Yellow
    Write-Host "   ────────────────────────────────────────────────────────────────" -ForegroundColor DarkGray
    Write-Host "   git checkout -b dev" -ForegroundColor Cyan
    Write-Host "   git push -u origin dev" -ForegroundColor Cyan
    Write-Host "   ────────────────────────────────────────────────────────────────" -ForegroundColor DarkGray
    Write-Host ""
    
} else {
    Write-Host "1️⃣  Create RELEASE repository on GitHub:" -ForegroundColor Yellow
    Write-Host "   • Go to: https://github.com/new" -ForegroundColor White
    Write-Host "   • Repository name: $repoName" -ForegroundColor White
    Write-Host "   • Make it Public" -ForegroundColor White
    Write-Host "   • Create repository" -ForegroundColor White
    Write-Host ""
    
    Write-Host "2️⃣  Create DEV repository on GitHub:" -ForegroundColor Yellow
    Write-Host "   • Go to: https://github.com/new" -ForegroundColor White
    Write-Host "   • Repository name: $repoName-Dev" -ForegroundColor White
    Write-Host "   • Make it Public or Private" -ForegroundColor White
    Write-Host "   • Create repository" -ForegroundColor White
    Write-Host ""
    
    Write-Host "3️⃣  Push to release repository:" -ForegroundColor Yellow
    Write-Host "   ────────────────────────────────────────────────────────────────" -ForegroundColor DarkGray
    Write-Host "   git remote add origin https://github.com/$username/$repoName.git" -ForegroundColor Cyan
    Write-Host "   git branch -M main" -ForegroundColor Cyan
    Write-Host "   git push -u origin main" -ForegroundColor Cyan
    Write-Host "   ────────────────────────────────────────────────────────────────" -ForegroundColor DarkGray
    Write-Host ""
}

Write-Host "4️⃣  Create your first release:" -ForegroundColor Yellow
Write-Host "   • Build project in Release mode in Visual Studio" -ForegroundColor White
Write-Host "   • Go to: https://github.com/$username/$repoName/releases/new" -ForegroundColor White
Write-Host "   • Tag version: v1.0.0" -ForegroundColor White
Write-Host "   • Release title: RPG Dungeon Crawler v1.0.0" -ForegroundColor White
Write-Host "   • Upload Night.exe and required DLLs" -ForegroundColor White
Write-Host "   • Publish release" -ForegroundColor White
Write-Host ""

Write-Host "📖 For more details, see GITHUB_SETUP.md" -ForegroundColor Cyan
Write-Host ""

# Ask if user wants to copy commands to clipboard
Write-Host "💡 Would you like to run the git commands now? (Y/N)" -ForegroundColor Yellow
$runCommands = Read-Host

if ($runCommands -eq "Y" -or $runCommands -eq "y") {
    Write-Host ""
    Write-Host "⚠️  Make sure you've created the repository on GitHub first!" -ForegroundColor Yellow
    Write-Host "   Visit: https://github.com/new" -ForegroundColor White
    Write-Host ""
    $confirm = Read-Host "Have you created the repository on GitHub? (Y/N)"
    
    if ($confirm -eq "Y" -or $confirm -eq "y") {
        Write-Host ""
        Write-Host "Running git commands..." -ForegroundColor Cyan
        
        try {
            git remote add origin "https://github.com/$username/$repoName.git"
            git branch -M main
            Write-Host "✅ Remote added and branch renamed to main" -ForegroundColor Green
            
            Write-Host ""
            Write-Host "Pushing to GitHub..." -ForegroundColor Cyan
            git push -u origin main
            Write-Host "✅ Code pushed to GitHub!" -ForegroundColor Green
            
            if ($setupOption -eq "1") {
                Write-Host ""
                Write-Host "Creating development branch..." -ForegroundColor Cyan
                git checkout -b dev
                git push -u origin dev
                Write-Host "✅ Development branch created and pushed!" -ForegroundColor Green
                git checkout main
            }
            
            Write-Host ""
            Write-Host "🎉 Setup complete!" -ForegroundColor Green
            Write-Host "   Your repository is at: https://github.com/$username/$repoName" -ForegroundColor White
            
        } catch {
            Write-Host ""
            Write-Host "⚠️  Error during git operations:" -ForegroundColor Yellow
            Write-Host "   $($_.Exception.Message)" -ForegroundColor Red
            Write-Host ""
            Write-Host "   You may need to run the commands manually." -ForegroundColor Yellow
        }
    }
}

Write-Host ""
Write-Host "══════════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host "Setup script complete!" -ForegroundColor Green
Write-Host "See GITHUB_SETUP.md for detailed workflow instructions." -ForegroundColor White
Write-Host "══════════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host ""
Read-Host "Press Enter to exit"
