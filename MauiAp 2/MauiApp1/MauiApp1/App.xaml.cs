using Serilog;

//dotnet add package Serilog
//dotnet add package Serilog.Sinks.Console
//dotnet add package Serilog.Sinks.File

namespace MauiApp1;

public partial class App : Application
{
    public App()
    {
        // initializes a new configuration for Serilog
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug() 
            .WriteTo.Console()
            .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        Log.Information("Starting application");

        InitializeComponent();
        MainPage = new AppShell();
        
        // Global exception
        AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
        {
            Log.Error(e.ExceptionObject as Exception, "Unhandled exception");
        };

        TaskScheduler.UnobservedTaskException += (sender, e) =>
        {
            Log.Error(e.Exception, "Unobserved task exception");
            e.SetObserved(); // Prevents application crash
        };
    }

    protected override void OnSleep()
    {
        // close and empty the log before the application is put to sleep
        Log.CloseAndFlush();
        base.OnSleep();
    }

    
}
