using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data.SQLite;
using System.Windows;

namespace PlayScore;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public IServiceProvider ServiceProvider { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Set up the DI container
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        // Build the service provider
        ServiceProvider = serviceCollection.BuildServiceProvider();

        // Resolve and show MainWindow
        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        // Register dependencies
        string connectionString = ConfigurationManager.ConnectionStrings["SQLiteConnection"].ConnectionString;

        services.AddSingleton(new SQLiteConnection(connectionString));

        services.AddSingleton<DatabaseHelper>();
        services.AddSingleton<DatabaseManager>();

        // Register MainWindow
        services.AddSingleton<MainWindow>();
    }
}
