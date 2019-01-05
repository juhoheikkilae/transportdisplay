using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TransportDisplay.API.Models;
using TransportDisplay.API.Logger;
using TransportDisplay.API.Services;

namespace TransportDisplay.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TimetableController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ITimetableService _timetableService;

        public TimetableController(ITimetableService timetableService, ILogger logger)
        {
            _timetableService = timetableService;
            _logger = logger;
        }

        // GET api/timetable/{stopId}
        [HttpGet]
        public async Task<ActionResult<TimetableModel.Timetable>> ScheduledDepartures(string stop, CancellationToken cancellationToken)
        {
            await _logger.Log($"Fetcing timetable for stop {stop}.");
            var response = await _timetableService.FetchTimetableAsync(stop, cancellationToken);
            return response;
        }

        [HttpGet]
        public async Task<ActionResult<TimetableModel.ArrivalEstimates>> Arrivals(string stop, CancellationToken cancellationToken)
        {
            return await _timetableService.FetchArrivalEstimatesAsync(stop, cancellationToken);
        }

        [HttpGet]
        public async Task<ActionResult<TimetableModel.Stop[]>> Stops(string search, CancellationToken cancellationToken)
        {
            return await _timetableService.FetchStopsAsync(search, cancellationToken);
        }
    }
}
