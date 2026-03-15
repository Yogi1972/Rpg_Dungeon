# Troubleshooting Blank Screen Issue

## Possible Causes & Solutions

### Issue 1: Console.Clear() Problems
**Symptoms**: Blank screen after launch
**Cause**: Console.Clear() can fail in some environments (redirected output, some IDEs)
**Solution**: ✅ Added try-catch around all Console.Clear() calls

### Issue 2: UTF-8 Emoji Rendering
**Symptoms**: Garbled text or blank screen  
**Cause**: Console doesn't support UTF-8 or emoji characters
**Solution**: Check your console settings

### Issue 3: Running from Visual Studio
**Symptoms**: Console appears then disappears
**Cause**: VS might not be configured to keep console open
**Solution**: 
- Press Ctrl+F5 to run without debugging (keeps console open)
- OR set a breakpoint after ShowTitleScreen() call

### Issue 4: Infinite Loop Without Output
**Symptoms**: Program runs but shows nothing
**Cause**: Code might be stuck or output not flushing
**Solution**: Check if program is responsive (can you Ctrl+C?)

## 🔧 Quick Fixes to Try

### Fix 1: Run Without Debugging
```
In Visual Studio: Press Ctrl+F5 (instead of F5)
```
This keeps the console window open.

### Fix 2: Check Console Window
- Make sure you're looking at the correct console window
- Check if it's hidden behind Visual Studio
- Try Alt+Tab to find it

### Fix 3: Add Debug Output
If still having issues, temporarily add this at the start of Main():
```csharp
Console.WriteLine("Program starting...");
System.Diagnostics.Debug.WriteLine("Program starting...");
```

### Fix 4: Disable UTF-8 if Console Doesn't Support It
If emojis cause issues, comment out this line:
```csharp
// Console.OutputEncoding = System.Text.Encoding.UTF8;
```

### Fix 5: Test Minimal Version
Try this minimal test in Main():
```csharp
private static void Main()
{
    Console.WriteLine("TEST: Program is running!");
    Console.WriteLine("Press Enter...");
    Console.ReadLine();
    ShowTitleScreen();
}
```

## ✅ Current Code Status

All Console.Clear() calls are wrapped in try-catch blocks, so they won't crash if console doesn't support clearing.

## 🎯 Most Likely Solution

**Press Ctrl+F5** in Visual Studio to run without debugging. This will:
1. Launch the console window
2. Keep it open even after program ends
3. Allow you to see any output or errors

## Still Not Working?

Try these diagnostic steps:
1. Check Output window in Visual Studio (View → Output)
2. Check for any runtime exceptions
3. Verify the program is actually running (Task Manager)
4. Try running the .exe directly from bin folder
5. Check if antivirus is blocking execution

## Emergency Rollback

If you need to go back to the old simple version:
```csharp
private static void Main()
{
    Console.OutputEncoding = System.Text.Encoding.UTF8;
    Console.WriteLine("Welcome to the RPG Dungeon Crawler!");
    
    // ... rest of old code ...
}
```

Let me know which specific symptom you're seeing and I can help further!
