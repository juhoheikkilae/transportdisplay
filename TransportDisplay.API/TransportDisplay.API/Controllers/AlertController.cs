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
    public class AlertController : ControllerBase, IAlertController
    {
        private readonly IAlertService _alertService;

        public AlertController(IAlertService alertService)
        {
            _alertService = alertService;
        }

        [HttpGet]
        public async Task<ActionResult<TimetableModel.Alert[]>> Get(CancellationToken cancellationToken)
        {
            return Ok(
                await _alertService.FetchCurrentAlertsAsync(cancellationToken)
                );
        }

        [HttpPost]
        public async Task<ActionResult> Post(TimetableModel.Alert alert, CancellationToken cancellationToken)
        {
            try
            {
                await _alertService.BroadcastAlert(alert, cancellationToken);
                return Ok(new { Message = "Alert sent!" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Message = e.Message });
            }
        }
    }
}