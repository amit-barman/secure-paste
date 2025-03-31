using System.ComponentModel;
using System.Reflection;

using SecurePaste.Constants;

namespace SecurePaste.Forms;

partial class Form1
{
    private IContainer components = null;
    private Button startButton;
    private Button stopButton;
    private Label statusLabel;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.startButton = new System.Windows.Forms.Button();
        this.stopButton = new System.Windows.Forms.Button();
        this.statusLabel = new System.Windows.Forms.Label();
        this.SuspendLayout();

        // startButton
        this.startButton.Location = new System.Drawing.Point(50, 50);
        this.startButton.Name = "startButton";
        this.startButton.Size = new System.Drawing.Size(120, 40);
        this.startButton.TabIndex = 0;
        this.startButton.Text = "Start Monitoring";
        this.startButton.UseVisualStyleBackColor = true;
        this.startButton.Click += new System.EventHandler(this.StartButton_Click);

        // stopButton
        this.stopButton.Location = new System.Drawing.Point(200, 50);
        this.stopButton.Name = "stopButton";
        this.stopButton.Size = new System.Drawing.Size(120, 40);
        this.stopButton.TabIndex = 1;
        this.stopButton.Text = "Stop Monitoring";
        this.stopButton.UseVisualStyleBackColor = true;
        this.stopButton.Click += new System.EventHandler(this.StopButton_Click);
        this.stopButton.Enabled = false;  // Initially disabled

        // statusLabel
        this.statusLabel.AutoSize = true;
        this.statusLabel.Location = new System.Drawing.Point(50, 120);
        this.statusLabel.Name = "statusLabel";
        this.statusLabel.Size = new System.Drawing.Size(200, 20);
        this.statusLabel.TabIndex = 2;
        this.statusLabel.Text = "Clipboard is not being monitored.";

        // Form1
        this.ClientSize = new System.Drawing.Size(400, 200);
        this.Controls.Add(this.statusLabel);
        this.Controls.Add(this.stopButton);
        this.Controls.Add(this.startButton);
        this.Name = "Form1";
        this.Text = AppConstants.AppName;
        using var stream = Assembly.GetExecutingAssembly()
            .GetManifestResourceStream(AppConstants.AppIcon);
        this.Icon = new Icon(stream);
        this.ResumeLayout(false);
        this.PerformLayout();
    }
}
