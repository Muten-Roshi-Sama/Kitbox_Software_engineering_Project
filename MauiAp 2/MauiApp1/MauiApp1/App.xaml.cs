namespace MauiApp1;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        
        MainPage = new AppShell();
        
        
        Application.Current.UserAppTheme = AppTheme.Light;

        // gestionnaire d'exceptions global , quand une exception pas captée par un bloc try-catch
        AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
        {
            Console.WriteLine("Une exception non gérée a été détectée.");
            // se contente d'ajouter un msg d'erreur mais modifier pour avoir des logs + informer
        };

        // Pour tâches asynchrones qui n'ont pas été "observées" ou traitées
        TaskScheduler.UnobservedTaskException += (sender, e) =>
        {
            Console.WriteLine("Exception dans une tâche non observée.");
            e.SetObserved(); // Evite l'app de crash , il faut aussi faire des logs pour le suivi des errors 
        };
    }
}