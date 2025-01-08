using System.Collections.ObjectModel;
using System.Windows;
using PlayScore.Services;

namespace PlayScore;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly DatabaseManager _databaseManager;
    private readonly DatabaseHelper _databaseHelper;

    private readonly MoonphaseService _moonphaseService;
    private readonly GameService _gameService;

    public ObservableCollection<GameModel> Games { get; } = [];

    public MainWindow(DatabaseManager databaseManager, DatabaseHelper databaseHelper)
    {
        InitializeComponent();

        _databaseManager = databaseManager;
        _databaseHelper = databaseHelper;

        _moonphaseService = new();
        _gameService = new();
    }

    private void ConnectToDatabase(object sender, RoutedEventArgs e)
    {
        _databaseHelper.ConnectToDatabase();
    }

    private void CreateDatabase(object sender, RoutedEventArgs e)
    {
        _databaseManager.CreateDatabase();
    }

    private void CreateTable(object sender, RoutedEventArgs e)
    {
        var tableName = TableName.Text;
        _databaseManager.CreateTable(tableName);
    }

    private async void GetMoonphase(object sender, RoutedEventArgs e)
    {
        string date = DateTextBox.Text;

        // Example: Rostock 
        var latitude = 54.0924;
        var longitude = 12.1407;

        var moonPhaseData = await _moonphaseService.GetMoonPhaseAsync(date, latitude, longitude);

        if (moonPhaseData == null)
        {
            MessageBox.Show("Failed to retrieve moon phase data. Please check your connection or try again.");
            return;
        }

        var translator = new MoonphaseTranslator();
        MoonPhaseTextBlock.Text = $"Mondphase: {translator.Translate(moonPhaseData.MoonPhase)}";
    }

    private async void GetGames(object sender, RoutedEventArgs e)
    {
        string date = DateTextBox.Text;
        GamesListBox.ItemsSource = Games;

        var gameData = await _gameService.GetGamesByReleaseDateAsync(date);

        if (gameData == null)
        {
            MessageBox.Show("Failed to retrieve game data. Please check your connection or try again.");
            return;
        }

        Games.Clear();
        gameData.ForEach(Games.Add);
    }
}