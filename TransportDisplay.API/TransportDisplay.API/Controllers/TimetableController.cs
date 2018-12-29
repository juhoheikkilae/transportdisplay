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
    [Route("api/[controller]")]
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

        // GET api/timetable?stop={stopId}
        [HttpGet]
        public async Task<ActionResult<TimetableModel.Timetable>> GetAsync(string id, CancellationToken cancellationToken)
        {
            await _logger.Log($"Fetcing timetable for stop {id}.");
            var response = await _timetableService.FetchTimetableAsync(id, cancellationToken);
            return response;
        }
    }
}
