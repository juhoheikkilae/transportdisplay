using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TransportDisplay.API.Controllers;
using TransportDisplay.API.Services;
using TransportDisplay.API.Logger;
using TransportDisplay.API.Clients;
using TransportDisplay.API.Models;
using System.Net.Http;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace TransportDisplayApiTests
{
    public class E2E
    {
        private readonly HttpClient _httpClient;

        private readonly IAlertController _alertController;
        private readonly IAlertService _alertService;

        private readonly ITimetableClient _timetableClient;
        private readonly ITimetableService _timetableService;
        private readonly ITimetableController _timetableController;
        
        private readonly IWeatherClient _weatherClient;
        private readonly IWeatherService _weatherService;
        private readonly IWeatherController _weatherController;

        private string exampleStop = "HSL:2314601";
        private string exampleSearch = "Mati";
        private string openWeatherMapLocationId = "660158";

        IConfiguration Configuration { get; set; }

        public E2E()
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<E2E>();

            Configuration = builder.Build();

            _httpClient = new HttpClient();
            
            _timetableClient = new HslClient(_httpClient);
            _timetableService = new TimetableService(_timetableClient);
            _timetableController = new TimetableController(_timetableService);

            _alertService = new AlertService(_timetableClient, null);
            _alertController = new AlertController(_alertService);

            _weatherClient = new WeatherClient(_httpClient, Configuration["Weather:ApiKey"]);
            _weatherService = new WeatherService(_weatherClient);
            _weatherController = new WeatherController(_weatherService);
        }

        [Fact]
        public async Task ShouldReturnTimetable()
        {
            var result = await _timetableController.ScheduledDepartures(exampleStop, CancellationToken.None);
            Assert.IsType<ActionResult<TimetableModel.Timetable>>(result);
        }

        [Fact]
        public async Task ShouldReturnStops()
        {
            var result = await _timetableController.Stops(exampleSearch, CancellationToken.None);
            Assert.IsType<ActionResult<TimetableModel.Stop[]>>(result);
        }

        [Fact]
        public async Task ShouldReturnArrivals()
        {
            var result = await _timetableController.Arrivals(exampleStop, CancellationToken.None);
            Assert.IsType<ActionResult<TimetableModel.Timetable>>(result);
        }

        [Fact]
        public async Task ShouldFetchWeatherConditions()
        {            
            var result = await _weatherController.Conditions(openWeatherMapLocationId, CancellationToken.None);
            Assert.IsType<ActionResult<WeatherModel.Conditions>>(result);
        }

        [Fact]
        public async Task ShouldFetchCurrentAlerts()
        {
            var result = await _alertController.Get(CancellationToken.None);
            Assert.IsType<ActionResult<TimetableModel.Alert[]>>(result);
        }
    }
}
