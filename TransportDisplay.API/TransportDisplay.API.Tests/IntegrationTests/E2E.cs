using System;
using System.Threading;
using System.Threading.Tasks;
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
    public partial class IntegrationTests
    {
        public class E2E
        {
            private string exampleStop = "HSL:2314601";

            [Fact]
            public async Task ShouldReturnTimetable()
            {
                var logger = new DebugLogger();
                using (var httpClient = new HttpClient())
                {
                    var hslClient = new HslTimetableClient(httpClient);
                    var arrivalEstimateClient = new ArrivalEstimateClient(httpClient);
                    var timetableService = new TimetableService(hslClient, arrivalEstimateClient);
                    var timetableController = new TimetableController(timetableService, logger);

                    var result = await timetableController.GetAsync(exampleStop, CancellationToken.None);
                    Assert.IsType<ActionResult<TimetableModel.Timetable>>(result);
                }

            }
        }
    }
}
