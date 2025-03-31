## 1. **App Overview**  
"Secure Paste" is a security-focused application designed to protect users' privacy by monitoring their clipboard and replacing any sensitive data with fake, random information to prevent accidental sharing. It ensures that sensitive data, such as phone numbers, credit card details, and other private information, is kept safe by immediately replacing it when copied to the clipboard.

### **Key Objectives:**
- Prevent accidental sharing of sensitive data.
- Provide users with control over the data monitoring process.
- Maintain user awareness of any replacement events through logs or notifications.

## 2. **Key Features**

### **Core Features**

#### **Clipboard Monitoring**  
- **Functionality**: The app continuously monitors the clipboard for any changes, specifically for the presence of sensitive information.  
- **Goal**: Detect sensitive data as soon as it is copied to the clipboard, without affecting the system's normal clipboard functionality.

#### **Sensitive Data Detection and Random Replacement**  
- **Functionality**: Automatically detects a variety of sensitive data types, including phone numbers, credit card numbers, social security numbers, email addresses, access tokens, bank account details, etc. Once sensitive data is detected, the app replaces it with fake, randomly generated data while maintaining the format and structure of the original data.  
- **Goal**: Ensure the protection of sensitive information by masking it while maintaining data integrity and format, allowing users to copy and paste content without exposing sensitive data.

#### **User Control**  
- **Functionality**: The app provides an interface where users can enable or disable the clipboard monitoring feature.  
- **Goal**: Give users control over the application.

#### **Logging & Alerts**  
- **Functionality**: The app records every instance of sensitive data replacement and notifies users upon application startup and shutdown.  
- **Goal**: Allow users to track replacement activities.

---

## 3. **Technology Stack and Implementation Details**

### **Platform**  
- **Framework**: .NET Windows Forms Application  
  - The app is built using the .NET Framework, leveraging Windows Forms to create a simple, user-friendly graphical interface.

### **Programming Language**  
- **C#**: The application is written in C#, utilizing object-oriented principles to ensure maintainable and extensible code. C# provides the flexibility needed for system-level integration with the clipboard, user interface, and background monitoring processes.

### **Clipboard Monitoring**  
- **Clipboard API**: The application uses the .NET `Clipboard.GetText()` method to access and monitor clipboard content, detecting changes as they occur.  
```cs
string clipboardText = Clipboard.GetText();
```

### **Sensitive Data Detection and Replacement**  
- **Detection**: The app uses a dictionary of `Regular Expressions (Regex)` to identify patterns related to sensitive data types (e.g., phone numbers, credit card numbers, email addresses, etc.).  
```cs
_patterns = new Dictionary<string, string>
{
    { "Email", @"(?i)[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}" },
    { "PhoneNumber", @"(?:\(\d{3}\)\s?|\d{3}[-.\s]?)\d{3}[-.\s]?\d{4}(?=\s|$)(?!\d)" },
    { "SSN", @"\b\d{3}-\d{2}-\d{4}\b" },
    { "CreditCard", @"(?:\d{4}[-\s]?){3}\d{4}" },
    { "Date", @"\b\d{2}/\d{2}/\d{4}\b" },
    { "IPAddress", @"\b(?:\d{1,3}\.){3}\d{1,3}\b" },
    { "URL", @"\bhttps?:\/\/[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}(/\S*)?\b" },
    { "BankAccount", @"\b\d{8,12}\b" },
    { "APIKey", @"\b[A-Za-z0-9]{32,64}\b" },
    { "PAN", @"[A-Z]{5}\d{4}[A-Z]{1}" },
    { "Aadhaar", @"\d{4}[- ]\d{4}[- ]\d{4}" }
};
```
- **Replacement**: When it detects potentially sensitive data, it uses the `ReplaceSensitiveData(string inputText)` method to replace the real data with randomly generated dummy values.  
```cs
public string ReplaceSensitiveData(string inputText)
{
    var outputText = new StringBuilder(inputText);

    foreach (var pattern in _patterns)
    {
        var dummyData = RandomDataProvider.GenerateDummyData(pattern.Key);
        var modifiedText = Regex.Replace(outputText.ToString(), pattern.Value, dummyData);
        outputText.Clear();
        outputText.Append(modifiedText);
    }
    return outputText.ToString();
}
```

### **Random Data Generation**  
- Random data is generated using the helper method `RandomDataProvider.GenerateDummyData(string patternKey)`, which utilizes the `Random()` class along with the random string generator method `RandomDataProvider.GenerateRandomString(int length)`.
```cs
public static string GenerateDummyData(string patternKey)
{
    var random = new Random();

    return patternKey switch
    {
        "Email" => $"{GenerateRandomString(5)}@{GenerateRandomString(5)}.com",
        "PhoneNumber" => $"({random.Next(100, 999)}) {random.Next(100, 999)}-{random.Next(1000, 9999)}",
        "SSN" => $"{random.Next(100, 999)}-{random.Next(10, 99)}-{random.Next(1000, 9999)}",
        "CreditCard" => $"{random.Next(1000, 9999)}-{random.Next(1000, 9999)}-{random.Next(1000, 9999)}-{random.Next(1000, 9999)}",
        "Date" => $"{random.Next(1, 13):D2}/{random.Next(1, 32):D2}/{random.Next(1900, 2023)}",
        "IPAddress" => $"{random.Next(1, 255)}.{random.Next(1, 255)}.{random.Next(1, 255)}.{random.Next(1, 255)}",
        "URL" => $"https://www.{GenerateRandomString(8)}.com",
        "BankAccount" => $"{random.Next(10000000, 999999999)}",
        "APIKey" => GenerateRandomString(32),
        "PAN" => $"{new string(Enumerable.Range(0, 5).Select(_ => (char)('A' + random.Next(0, 26))).ToArray())}{random.Next(1000, 9999)}{(char)('A' + random.Next(0, 26))}",
        "Aadhaar" => $"{random.Next(1000, 9999)}-{random.Next(1000, 9999)}-{random.Next(1000, 9999)}",
        _ => "REPLACEMENT"
    };
}
```

### **User Interface**  
- **Windows Forms UI**: The app uses `Windows Forms` to provide an interactive user interface for configuring and managing the monitoring features.  
  - Options to start/stop monitoring.

### **Logging & Notifications**  
- **File-Based Logging**: Logs are stored in a local file, with a timestamp for each event using a custom class `Logger` for file-based logging. This file can be reviewed by the user to monitor clipboard security.  
```cs
// Logic for logging
private void Log(string message, string level)
{
    string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}";
    try
    {
        File.AppendAllText(_logFilePath, $"{logMessage}{Environment.NewLine}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Logging failed: {ex.Message}");
    }
}
```

```log
2025-03-29 14:26:27 [INFO] Data Replaced Successfully.
```

- **System Notifications**: The app integrates with the Windows notification system to alert users when the app starts and stops. To achieve this, it uses the `NotifyIcon` class.  
```cs
public NotificationManager()
{
    using var stream = Assembly.GetExecutingAssembly()
        .GetManifestResourceStream(AppConstants.AppIcon);
    _notifyIcon = new NotifyIcon
    {
        Icon = NotificationIcon(stream),
        Text = AppConstants.AppName
    };
}

public void ShowNotification(string title, string message, ToolTipIcon icon, int duration = 3000)
{
    _notifyIcon.BalloonTipTitle = title;
    _notifyIcon.BalloonTipText = message;
    _notifyIcon.BalloonTipIcon = icon;

    _notifyIcon.Visible = true;
    _notifyIcon.ShowBalloonTip(duration);
}
```

---

## 5. **Installation & Setup**

### **System Requirements**  
- **Operating System**: Windows 7 or later.  
- **.NET Framework**: Version 4.8 or higher.  
- **Disk Space**: Approximately 5 MB for the installation package.

### **Installation Steps**
1. Download the latest version of the Secure Paste installer from the releases section on GitHub.
2. Run the installer and follow the on-screen instructions to complete the setup.
3. Once installed, launch the application, and start clipboard monitoring.

### **Uninstallation**
1. Go to Control Panel > Programs and Features.
2. Locate "Secure Paste" in the list of installed applications.
3. Click "Uninstall" and follow the prompts to remove the application.

---

## 6. **Usage Guide**

### **Starting the App**  
Upon launching the app, you will see the main interface with the following options:  
- **Start/Stop Clipboard Monitoring**: Toggle to start or stop monitoring clipboard content.
- **Logs**: View detailed logs about past replacements in the app’s root directory.

---
