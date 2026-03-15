# Error Logging System - RPG Dungeon Crawler

## 📋 Overview
The game now includes a comprehensive error logging system that automatically captures and records any errors that occur during gameplay.

## 📂 Error Log Location
All error logs are saved to:
```
D:\VS_Projects\Rpg_Dungeon\ErrorLogs\
```

## 📝 Log Files

### Automatic Error Logs
- **Format:** `error_log_YYYY-MM-DD.txt`
- **Example:** `error_log_2024-01-15.txt`
- **Content:** Daily error logs with timestamps, stack traces, and system info
- **Created:** Automatically when an error occurs

### Error Reports (To Send to Developer)
- **Format:** `ERROR_REPORT_YYYYMMDD_HHMMSS.txt`
- **Example:** `ERROR_REPORT_20240115_143022.txt`
- **Content:** Comprehensive report including:
  - All recent error logs
  - System information (OS, .NET version, hardware)
  - Application version and location
  - Save file information
  - Performance metrics
- **Created:** Manually via in-game menu (Option 7 → Option 3)

## 🎮 How to Use

### Accessing Error Logs from Main Menu
1. Launch the game
2. Select **7) 📋 Error Logs & Diagnostics**
3. Choose from the following options:
   - **1) View Statistics** - See how many errors have been logged
   - **2) Open Logs Folder** - Opens the ErrorLogs folder in Windows Explorer
   - **3) Create Error Report** - Creates a comprehensive report to send to developer
   - **4) Clean Old Logs** - Removes logs older than 30 days
   - **5) Run Diagnostics** - Tests system features
   - **6) Send Report via Email** ⭐ NEW - Automatically opens email client
   - **7) Send Test Report** ⭐ NEW - Send test report to verify system works
   - **8) Email Instructions** ⭐ NEW - View email sending guide

### Sending an Error Report to Developer

**🌟 Option 1: Automatic Email (Easiest & Recommended)**
1. In-game: Main Menu → 7 → **6) Send Error Report via Email**
2. Report is created automatically
3. Your default email client opens with pre-filled message
4. Manually attach the error report file (path shown on screen)
5. Add description of what happened
6. Click Send in your email client
7. **Developer email: v\*\*\*\*\*7@g\*\*\*l.com** (masked for security)

**Option 2: Send Test Report (For Testing)**
1. Main Menu → 7 → **7) Create & Send Test Report to Developer**
2. Creates a test error with full system diagnostics
3. Confirms before sending
4. Opens email client automatically
5. Perfect for verifying the error system works!

**Option 3: Create Then Email**
1. In-game: Main Menu → 7 → 3 (Create Error Report)
2. Press **'E'** when prompted to send via email
3. Follow the prompts

**Option 4: Manual Method**
1. Navigate to: `D:\VS_Projects\Rpg_Dungeon\ErrorLogs\`
2. Find the `ERROR_REPORT_*.txt` file
3. Compose email manually to developer
4. Attach the report file
5. Include a description of what you were doing when the error occurred

## 📊 What Gets Logged

### For Each Error:
- **Timestamp** - Exact date and time
- **Error Type** - Full .NET exception type
- **Error Message** - Human-readable description
- **Stack Trace** - Detailed code execution path
- **Inner Exceptions** - Any nested errors
- **Context** - What was happening when error occurred

### System Information:
- Operating System version
- .NET Runtime version
- CPU architecture (32/64-bit)
- Machine name and user
- Working directory

### Application Information:
- Game version
- Assembly location
- Memory usage
- Thread count
- Application uptime

### Save File Information:
- List of all save files
- File sizes and modification dates
- Helps debug save/load issues

## 🔄 Automatic Features

### Error Logging is Automatic
Errors are automatically logged when:
- The game crashes
- An exception occurs during gameplay
- UTF-8 encoding fails (logged as warning)
- Any unhandled exception occurs

### Log File Management
- One log file per day
- Old logs (30+ days) can be cleaned automatically
- Thread-safe logging (multiple errors won't corrupt the log)

## 💻 For Developers

### Code Integration
Error logging is integrated at:
- **Program.cs** - Global exception handler
- **Any catch blocks** - Can call `ErrorLogger.LogError(ex, "context")`

### Available Methods:
```csharp
// Log a critical error (returns path to log file)
string path = ErrorLogger.LogCriticalError(exception, "optional context");

// Log a general error
string path = ErrorLogger.LogError(exception, "optional context");

// Log a warning
ErrorLogger.LogWarning("warning message", "optional context");

// Create error report
string reportPath = ErrorLogger.CreateErrorReport();

// Open logs folder
ErrorLogger.OpenLogDirectory();

// Clean old logs
ErrorLogger.CleanOldLogs(30); // days to keep
```

## 🛡️ Privacy & Security
- **No personal data** collected beyond Windows username
- **No network activity** - all logs stored locally
- **Email address masked** in UI for security (v\*\*\*\*\*7@g\*\*\*l.com)
- **Email obfuscated** in code (Base64 encoded)
- **User controlled** - You decide when to send reports
- **Uses your email client** - No SMTP credentials stored
- **Optional** - Logs can be deleted at any time

## 📧 Email Features

### How Email Sending Works
1. **No Direct Sending** - Uses your default email client (Outlook, Gmail, etc.)
2. **Your Email Account** - Sends from YOUR email, not the game
3. **Pre-filled Message** - Subject and body are automatically filled
4. **Manual Attachment** - You attach the report file manually
5. **Full Control** - You see everything before sending

### Email Address Security
- Developer email: **v\*\*\*\*\*7@g\*\*\*l.com**
- Stored as Base64 in code: `dmVudHJ1ZTA3QGdtYWlsLmNvbQ==`
- Decoded only when needed
- Not visible to casual users

## 🗑️ Cleaning Up Logs
If error logs take up too much space:
1. Main Menu → 7 → 4 (Clean Old Logs)
2. Or manually delete files from the ErrorLogs folder

## ❓ FAQ

**Q: Will this slow down my game?**  
A: No. Logging only occurs when errors happen, and it's very fast.

**Q: Can I disable error logging?**  
A: Error logging is essential for debugging. Files are small and only created when errors occur.

**Q: What if the game crashes before showing the error?**  
A: The error is logged to file immediately. Check the ErrorLogs folder even if the game closes.

**Q: Should I send every error report?**  
A: Send reports for:
- Game crashes
- Lost progress/saves
- Repeated errors
- Unexpected behavior

**Q: Can I view logs outside the game?**  
A: Yes! Navigate to the ErrorLogs folder and open any .txt file with Notepad.

**Q: Is the email feature safe?**  
A: Yes! It only opens your email client - you control what gets sent.

**Q: Will users see my email address?**  
A: No. The email is masked in the UI (v\*\*\*\*\*7@g\*\*\*l.com) and obfuscated in the code.

**Q: Do I need SMTP credentials?**  
A: No! It uses the default email client on your system (Outlook, Thunderbird, etc.).

**Q: How do I test if it works?**  
A: Use Menu → 7 → 7 to send a test report. It's marked as TEST so you know it's not a real error.

## 📧 Reporting Issues
When reporting an issue via email, please include:
1. The ERROR_REPORT_*.txt file (attached)
2. Description of what you were doing
3. Steps to reproduce the error
4. Your save file (if related to save/load)

**Email to: v\*\*\*\*\*7@g\*\*\*l.com** (opens automatically via menu option 6 or 7)

---

**Thank you for helping improve RPG Dungeon Crawler!** 🎮⚔️
