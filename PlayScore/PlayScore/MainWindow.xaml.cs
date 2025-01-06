using System.Collections.ObjectModel;
using System.Windows;
using WpfTestApp.Services;

namespace WpfTestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DatabaseManager _databaseManager;
        private readonly DatabaseHelper _databaseHelper;

        private readonly MoonphaseService _moonphaseService;
        private readonly GameService _gameService;

        public ObservableCollection<GameModel> Games { get; set; } = new ObservableCollection<GameModel>();

        public MainWindow(DatabaseManager databaseManager, DatabaseHelper databaseHelper)
        {
            InitializeComponent();

            _databaseManager = databaseManager;
            _databaseHelper = databaseHelper;

            _moonphaseService = new MoonphaseService();
            _gameService = new GameService();
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
            string tableName = TableName.Text;

            _databaseManager.CreateTable(tableName);
        }

        private async void GetMoonphase(object sender, RoutedEventArgs e)
        {
            string date = DateTextBox.Text;

            // Example: Rostock 
            double latitude = 54.0924;
            double longitude = 12.1407;

            var moonPhaseData = await _moonphaseService.GetMoonPhaseAsync(date, latitude, longitude);

            if (moonPhaseData != null)
            {
                MoonphaseTranslator translator = new MoonphaseTranslator();

                MoonPhaseTextBlock.Text = $"Mondphase: {translator.Translate(moonPhaseData.MoonPhase)}";
            }
            else
            {
                MessageBox.Show("Failed to retrieve moon phase data. Please check your connection or try again.");
            }
        }

        private async void GetGames(object sender, RoutedEventArgs e)
        {
            string date = DateTextBox.Text;
            GamesListBox.ItemsSource = Games;

            var gameData = await _gameService.GetGamesByReleaseDateAsync(date);

            if (gameData != null)
            {
                Games.Clear();
                foreach (var game in gameData)
                {
                    Games.Add(game);
                }

            }
            else
            {
                MessageBox.Show("Failed to retrieve game data. Please check your connection or try again.");
            }

        }

    }
}