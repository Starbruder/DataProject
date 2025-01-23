using Newtonsoft.Json;

namespace PlayScore;

public sealed class GameModel
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("released")]
    public string Released { get; set; }

    [JsonProperty("rating")]
    public double Rating { get; set; }
}
