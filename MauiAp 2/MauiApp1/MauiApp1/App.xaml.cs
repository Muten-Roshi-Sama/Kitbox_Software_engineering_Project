using Serilog;

namespace MauiApp1;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();
        
        // Global Exceptions
        AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
        {
            Log.Error(e.ExceptionObject as Exception, "Unhandled exception");
        };

        TaskScheduler.UnobservedTaskException += (sender, e) =>
        {
            Log.Error(e.Exception, "Unobserved task exception");
            e.SetObserved();
        };
    }
    // Cleaning up
    protected override void OnSleep()
    {
        Log.CloseAndFlush();
        base.OnSleep();
    }
}
