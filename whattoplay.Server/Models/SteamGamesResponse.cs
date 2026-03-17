using System.Text.Json.Serialization;

namespace whattoplay.Server.Models
{
    public class SteamGamesRoot
    {
        [JsonPropertyName("response")]
        public SteamGamesResponse Response { get; set; } = new();
    }
    public class SteamGamesResponse
    { [JsonPropertyName("game_count")]
        public int GameCount { get; set; }
        [JsonPropertyName("games")]
        public List<SteamGame> Games { get; set; } = new();
    }
    public class SteamGame
    {
        [JsonPropertyName("appid")]
        public int AppId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("playtime_forever")]
        public int PlaytimeForever { get; set; }

        [JsonPropertyName("img_icon")]
        public string ImgIcon { get; set; }
    }

    public class SteamIdRoot
    {
        [JsonPropertyName("response")]
        public SteamIdResponse Response { get; set; } = new();
    }
    public class SteamIdResponse
    {
        [JsonPropertyName("success")]
        public int Success { get; set; }
        [JsonPropertyName("steamid")]
        public string SteamId { get; set; }
    }

}



