using System;
using System.Threading;
using System.Threading.Tasks;
using TransportDisplay.API.Models;
using TransportDisplay.API.Clients;

namespace TransportDisplay.API.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherClient _client;

        public WeatherService(IWeatherClient client) => _client = client;
        public async Task<WeatherModel.Conditions> FetchConditions(string id, CancellationToken cancellationToken)
            => await _client.GetConditionsAsync(id, cancellationToken);
    }
}
