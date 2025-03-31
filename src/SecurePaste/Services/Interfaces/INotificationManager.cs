namespace SecurePaste.Services.Interfaces;

public interface INotificationManager
{
    void CleanUp();
    void ShowNotification(string title, string message, ToolTipIcon icon, int duration = 3000);
}