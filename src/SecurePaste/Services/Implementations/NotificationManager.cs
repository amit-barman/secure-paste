using System.Reflection;

using SecurePaste.Constants;
using SecurePaste.Services.Interfaces;

namespace SecurePaste.Services.Implementations;

public class NotificationManager : INotificationManager
{
    private readonly NotifyIcon _notifyIcon;

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

    public void CleanUp()
    {
        _notifyIcon.Dispose();
    }

    private static Icon NotificationIcon(Stream? stream) =>
        stream is not null ? new Icon(stream) : SystemIcons.Information;
}