using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace TestDB;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        Console.WriteLine("beginning");
        using var connection = new MySqlConnection("Server=localhost;User ID=UserDB;Password=1234;Database=ITAcademyDB");
        connection.Open();
        Console.WriteLine("connected");
        using var command = new MySqlCommand("SELECT * FROM UserTab;", connection);
        using var reader = command.ExecuteReader();
        while (reader.Read()){
            Console.WriteLine("here");
            Console.WriteLine(reader.GetString("Pseudo"));
        }

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}