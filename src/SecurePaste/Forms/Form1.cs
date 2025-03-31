using SecurePaste.Constants;
using SecurePaste.Services.Interfaces;

namespace SecurePaste.Forms;

public partial class Form1 : Form
{
    private readonly System.Windows.Forms.Timer _clipboardTimer;
    private const int MonitoringInterval = 1000;
    private readonly INotificationManager _notificationManager;
    private readonly ISensitiveDataReplacer _sensitiveDataReplacer;
    private readonly ILogger _logger;
    private string _currentReplaced = string.Empty;

    public Form1(INotificationManager notificationManager,
        ISensitiveDataReplacer sensitiveDataReplacer,
        ILogger logger)
    {
        InitializeComponent();
        _clipboardTimer = new System.Windows.Forms.Timer
        {
            Interval = MonitoringInterval // 1 second interval
        };
        _clipboardTimer.Tick += ClipboardTimer_Tick;
        _notificationManager = notificationManager;
        _sensitiveDataReplacer = sensitiveDataReplacer;
        _logger = logger;
    }

    private void StartButton_Click(object sender, EventArgs e)
    {
        _clipboardTimer.Start();  // Start monitoring clipboard
        statusLabel.Text = "Monitoring clipboard for sensitive data...";
        startButton.Enabled = false;  // Disable Start button
        stopButton.Enabled = true;   // Enable Stop button
        _notificationManager.ShowNotification(AppConstants.AppName, "Clipboard Monitoring Started.", ToolTipIcon.Info);
    }

    private void StopButton_Click(object sender, EventArgs e)
    {
        _clipboardTimer.Stop();  // Stop monitoring clipboard
        statusLabel.Text = "Clipboard monitoring stopped.";
        startButton.Enabled = true;  // Enable Start button
        stopButton.Enabled = false; // Disable Stop button
        _notificationManager.ShowNotification(AppConstants.AppName, "Clipboard monitoring stopped.", ToolTipIcon.Warning);
    }

    private void ClipboardTimer_Tick(object sender, EventArgs e)
    {
        try
        {
            // Get the current clipboard content
            string clipboardText = Clipboard.GetText();

            if (!string.IsNullOrEmpty(clipboardText) && _currentReplaced != clipboardText)
            {
                string replacedText = _sensitiveDataReplacer.ReplaceSensitiveData(clipboardText);
                _currentReplaced = replacedText;

                if (replacedText != clipboardText)
                {
                    Clipboard.SetText(replacedText);  // Update clipboard with fake data
                    _logger.Info("Data Replaced Successfully.");
                }
            }
        }
        catch (Exception ex)
        {
            statusLabel.Text = "Error monitoring clipboard: " + ex.Message;
        }
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        // Clean up the NotificationManager when the form is closed
        _notificationManager.CleanUp();
        base.OnFormClosed(e);
    }
}
