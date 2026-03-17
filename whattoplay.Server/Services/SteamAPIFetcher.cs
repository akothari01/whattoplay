using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace whattoplay.Server.Services
{
    public class SteamAPIFetcher
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public SteamAPIFetcher(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _apiKey = config["Steam:APIKey"] ?? throw new ArgumentNullException("Steam API key is missing in configuration.");  
        }

        public async Task<string> fetchPlayerOwnedGamesAsync(string steamId)
        {
            var query = new Dictionary<string, string?>() 
            {
                ["key"] = _apiKey,
                ["steamid"] = steamId,
                ["include_appinfo"] = "true",
                ["format"] = "json"
            };
            var baseURL = "http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/";
            var url = QueryHelpers.AddQueryString(baseURL, query);
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception($"Failed to fetch owned games for Steam ID {steamId}. Status code: {response.StatusCode}");
            }
        }

        public async Task<string> fetchSteamdId(string steamName)
        {
            var query = new Dictionary<string, string?>()
            {
                ["key"] = _apiKey,
                ["vanityurl"] = steamName,
                ["format"] = "json"
            };
            var baseURL = "http://api.steampowered.com/ISteamUser/ResolveVanityURL/v0001/";
            var url = QueryHelpers.AddQueryString(baseURL, query);
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception($"Failed to fetch Steam ID for vanity URL {steamName}. Status code: {response.StatusCode}");
            }
        }
    }
}
