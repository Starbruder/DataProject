using Newtonsoft.Json;
using PlayScore.Models;
using System.Configuration;
using System.Net.Http;

namespace PlayScore.Services;

public sealed class GameService : IService
{
    private readonly HttpClient _httpClient;
    private readonly string apiKey = ConfigurationManager.AppSettings["API_KEY_GAMES"] ?? string.Empty;
    private readonly string ApiUrl;

    public GameService()
    {
        _httpClient = new HttpClient();
        ApiUrl = $"https://api.rawg.io/api/games?key={apiKey}&dates=";
    }

    public async Task<List<GameModel>> GetGamesByReleaseDateAsync(string releaseDate)
    {
        try
        {
            // Build the URL with the date filter, first date is start, second is end
            string url = $"{ApiUrl}{releaseDate},{releaseDate}";

            // Send a GET request
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            // Read the response content
            string content = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON into a list of games
            var jsonResponse = JsonConvert.DeserializeObject<dynamic>(content);
            var games = new List<GameModel>();

            foreach (var game in jsonResponse.results)
            {
                games.Add(new GameModel
                {
                    Id = game.id,
                    Name = game.name,
                    Released = game.released,
                    Rating = game.rating
                });
            }

            return games;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching games: {ex.Message}");
            return [];
        }
    }
}
