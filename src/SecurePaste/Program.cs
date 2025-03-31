using System.Configuration;

using Microsoft.Extensions.DependencyInjection;

using SecurePaste.Constants;
using SecurePaste.Forms;
using SecurePaste.Services.Implementations;
using SecurePaste.Services.Interfaces;

namespace SecurePaste;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        var services = new ServiceCollection();
        // Register Services
        services.AddTransient<Form1>();
        services.AddTransient<INotificationManager, NotificationManager>();
        services.AddTransient<ISensitiveDataReplacer, SensitiveDataReplacer>();
        services.AddSingleton<ILogger>(provider => new Logger(ConfigurationManager.AppSettings["logfile"] ?? AppConstants.LogFilePath));

        using var serviceProvider = services.BuildServiceProvider();
        var form1 = serviceProvider.GetRequiredService<Form1>();

        Application.Run(form1);
    }
}