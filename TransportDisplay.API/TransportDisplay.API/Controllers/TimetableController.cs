using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TransportDisplay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimetableController : ControllerBase
    {
        private readonly Services.ITimetableService _timetableService;

        public TimetableController(Services.ITimetableService timetableService)
        {
            _timetableService = timetableService;
        }

        // GET api/timetable/{stopId}
        [HttpGet]
        public async Task<ActionResult<Models.TimetableModel.Timetable>> GetAsync(string stop)
            => await _timetableService.FetchTimetableAsync(stop);
    }
}
