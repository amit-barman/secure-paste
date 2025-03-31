using SecurePaste.Services.Interfaces;

namespace SecurePaste.Services.Implementations;

public class Logger : ILogger
{
    private readonly string _logFilePath;

    public Logger(string logFilePath)
    {
        _logFilePath = logFilePath;
    }

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

    public void Info(string message) => Log(message, "INFO");
    public void Warn(string message) => Log(message, "WARN");
    public void Error(string message) => Log(message, "ERROR");
    public void Debug(string message) => Log(message, "DEBUG");
}