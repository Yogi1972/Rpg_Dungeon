# Build script to create release binaries for v3.1.0-beta

Write-Host "================================" -ForegroundColor Cyan
Write-Host "Building RPG Dungeon v3.1.0-beta" -ForegroundColor Cyan
Write-Host "================================" -ForegroundColor Cyan
Write-Host ""

# Clean previous builds
Write-Host "Cleaning previous builds..." -ForegroundColor Yellow
if (Test-Path ".\publish") {
    Remove-Item ".\publish" -Recurse -Force
}
if (Test-Path ".\RPG_Dungeon_v3.1.0-beta.zip") {
    Remove-Item ".\RPG_Dungeon_v3.1.0-beta.zip" -Force
}

Write-Host "Building release version..." -ForegroundColor Yellow
Write-Host ""

# Publish the application (self-contained, ready to run)
dotnet publish Night.csproj -c Release -o ./publish --self-contained false -p:PublishSingleFile=false

if ($LASTEXITCODE -eq 0) {
    Write-Host ""
    Write-Host "Build successful!" -ForegroundColor Green
    Write-Host ""
    
    # Create README for the release
    $readmeContent = @"
# RPG Dungeon Crawler v3.1.0-beta

## Installation

1. Extract this ZIP to any folder
2. Make sure you have .NET 10 Runtime installed
3. Run Night.exe

## What's New

This version includes the Enhanced Combat System with 16 unique abilities!

- Use A1, A2, A3, A4 in combat to use abilities
- Manage Mana/Stamina resources
- Experience strategic combat with cooldowns

## Requirements

- Windows 10/11
- .NET 10 Runtime: https://dotnet.microsoft.com/download/dotnet/10.0

## Feedback

Report issues at: https://github.com/Yogi1972/Rpg_Dungeon/issues

Enjoy!
"@
    
    $readmeContent | Out-File -FilePath ".\publish\README.txt" -Encoding UTF8
    
    Write-Host "Creating ZIP file..." -ForegroundColor Yellow
    Compress-Archive -Path ".\publish\*" -DestinationPath ".\RPG_Dungeon_v3.1.0-beta.zip" -Force
    
    Write-Host ""
    Write-Host "================================" -ForegroundColor Green
    Write-Host "BUILD COMPLETE!" -ForegroundColor Green
    Write-Host "================================" -ForegroundColor Green
    Write-Host ""
    Write-Host "Release file created:" -ForegroundColor Cyan
    Write-Host "   RPG_Dungeon_v3.1.0-beta.zip" -ForegroundColor White
    Write-Host ""
    Write-Host "File size:" -ForegroundColor Cyan
    $zipFile = Get-Item ".\RPG_Dungeon_v3.1.0-beta.zip"
    $sizeMB = [math]::Round($zipFile.Length / 1MB, 2)
    Write-Host "   $sizeMB MB" -ForegroundColor White
    Write-Host ""
    Write-Host "Location:" -ForegroundColor Cyan
    Write-Host "   $((Get-Location).Path)\RPG_Dungeon_v3.1.0-beta.zip" -ForegroundColor White
    Write-Host ""
    Write-Host "Next Steps:" -ForegroundColor Yellow
    Write-Host "   1. Go to your GitHub release page" -ForegroundColor White
    Write-Host "   2. Drag and drop RPG_Dungeon_v3.1.0-beta.zip onto the 'Attach binaries' section" -ForegroundColor White
    Write-Host "   3. Publish the release!" -ForegroundColor White
    Write-Host ""
    
    # Open the folder
    Write-Host "Opening folder..." -ForegroundColor Yellow
    Start-Process explorer.exe -ArgumentList (Get-Location).Path
    
} else {
    Write-Host ""
    Write-Host "Build failed!" -ForegroundColor Red
    Write-Host "Check the errors above." -ForegroundColor Red
}

Write-Host ""
