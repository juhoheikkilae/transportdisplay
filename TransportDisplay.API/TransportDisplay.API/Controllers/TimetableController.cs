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
    public class TimetableController : ControllerBase, ITimetableController
    {
        private readonly ITimetableService _timetableService;

        public TimetableController(ITimetableService timetableService)
        {
            _timetableService = timetableService;
        }

        // GET api/timetable/{stopId}
        [HttpGet]
        public async Task<ActionResult<TimetableModel.Timetable>> ScheduledDepartures(string stop, CancellationToken cancellationToken)
        {
            return await _timetableService.FetchTimetableAsync(stop, cancellationToken);
        }

        [HttpGet]
        public async Task<ActionResult<TimetableModel.Timetable>> Arrivals(string stop, CancellationToken cancellationToken)
        {
            return await _timetableService.FetchArrivalEstimatesAsync(stop, cancellationToken);
        }

        [HttpGet]
        public async Task<ActionResult<TimetableModel.Stop[]>> Stops(string search, CancellationToken cancellationToken)
        {
            return await _timetableService.SearchStopsAsync(search, cancellationToken);
        }
    }
}
