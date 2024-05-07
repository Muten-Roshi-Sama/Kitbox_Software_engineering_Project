using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Serilog; 
using Serilog.Extensions.Logging;

namespace MauiApp1;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        // Serilog configuration
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("logs/myapp.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        // Integrate Serilog into MAUI's logging system
        builder.Logging.AddSerilog(); // All logs generated will go through Serilog.
#endif

        return builder.Build();
    }
}