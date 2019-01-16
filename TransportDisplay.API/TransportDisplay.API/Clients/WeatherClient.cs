using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TransportDisplay.API.Helpers;
using TransportDisplay.API.Models;

namespace TransportDisplay.API.Clients
{
    public class WeatherClient : IWeatherClient
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        public WeatherClient(HttpClient httpClient, string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = httpClient;
        }

        public async Task<WeatherModel.Conditions> GetConditionsAsync(string id, CancellationToken cancellationToken)
            => await _httpClient.ApiGetAsync<WeatherModel.Conditions, Responses.OpenWeatherMapResponse>(
                $"https://api.openweathermap.org/data/2.5/weather?id={id}&appid={_apiKey}",
                response => response.ToConditions(),
                cancellationToken
            );
    }
}
