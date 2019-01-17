using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TransportDisplay.API.Models;
using TransportDisplay.API.Services;

namespace TransportDisplay.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WeatherController : ControllerBase, IWeatherController
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService) => _weatherService = weatherService;

        // GET api/weather/{locationId}
        [HttpGet]
        public async Task<ActionResult<WeatherModel.Conditions>> Conditions(string id, CancellationToken cancellationToken)
            => await _weatherService.FetchConditions(id, cancellationToken);
    }
}
