using Newtonsoft.Json;
using System.Configuration;
using System.Globalization;
using System.Net.Http;

namespace WpfTestApp
{
    public class MoonphaseService
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly string apiKey = ConfigurationManager.AppSettings["API_KEY_MOON"];
        private readonly string ApiUrl;

        public MoonphaseService()
        {
            ApiUrl = $"https://api.ipgeolocation.io/astronomy?apiKey={apiKey}&date=";
        }

        public async Task<MoonPhaseModel> GetMoonPhaseAsync(string date, double latitude, double longitude)
        {
            try
            {
                string latitudeStr = latitude.ToString(CultureInfo.InvariantCulture);  // Period as decimal separator
                string longitudeStr = longitude.ToString(CultureInfo.InvariantCulture);

                // Request URL bauen
                string requestUrl = $"{ApiUrl}{date}&lat={latitudeStr}&long={longitudeStr}";

                // Send GET request
                HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);
                response.EnsureSuccessStatusCode();

                // Response als string
                string content = await response.Content.ReadAsStringAsync();

                // Deserialize JSON to C# Object
                var moonphaseData = JsonConvert.DeserializeObject<MoonPhaseModel>(content);
                return moonphaseData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }

        }
    }
}
