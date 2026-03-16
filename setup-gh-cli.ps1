# GitHub CLI Helper Script
# This script adds the GitHub CLI to your PowerShell session
# 
# USAGE:
#   1. Run this once: .\setup-gh-cli.ps1
#   2. Or add to your PowerShell profile for permanent access

# Add GitHub CLI to PATH for this session
$ghPath = "C:\Program Files\GitHub CLI"
if (Test-Path $ghPath) {
    $env:Path = "$ghPath;$env:Path"
    Write-Host "✅ GitHub CLI added to PATH" -ForegroundColor Green
    Write-Host "   You can now use 'gh' commands directly" -ForegroundColor Cyan
    
    # Test it
    gh --version
    Write-Host ""
    Write-Host "🎉 GitHub CLI is ready to use!" -ForegroundColor Green
    Write-Host "   Try: gh release list" -ForegroundColor Yellow
} else {
    Write-Host "❌ GitHub CLI not found at: $ghPath" -ForegroundColor Red
    Write-Host "   Please install from: https://cli.github.com/" -ForegroundColor Yellow
}

# Optional: Add to PowerShell profile for permanent access
Write-Host ""
Write-Host "💡 TIP: To make this permanent, add this line to your PowerShell profile:" -ForegroundColor Cyan
Write-Host '   $env:Path = "C:\Program Files\GitHub CLI;$env:Path"' -ForegroundColor White
Write-Host ""
Write-Host "   Your profile location: $PROFILE" -ForegroundColor Gray
