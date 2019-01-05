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
            private static HttpClient _httpClient = new HttpClient();
            private string exampleStop = "HSL:2314601";
            private string exampleSearch = "Mati";

            [Fact]
            public async Task ShouldReturnTimetable()
            {
                var hslClient = new HslTimetableClient(_httpClient);
                var timetableService = new TimetableService(hslClient);
                var timetableController = new TimetableController(timetableService);

                var result = await timetableController.ScheduledDepartures(exampleStop, CancellationToken.None);
                Assert.IsType<ActionResult<TimetableModel.Timetable>>(result);
            }

            [Fact]
            public async Task ShouldReturnStops()
            {
                var hslClient = new HslTimetableClient(_httpClient);
                var timetableService = new TimetableService(hslClient);
                var timetableController = new TimetableController(timetableService);

                var result = await timetableController.Stops(exampleSearch, CancellationToken.None);
                Assert.IsType<ActionResult<TimetableModel.Stop[]>>(result);
            }

            [Fact]
            public async Task ShouldReturnArrivals()
            {
                var hslClient = new HslTimetableClient(_httpClient);
                var timetableService = new TimetableService(hslClient);
                var timetableController = new TimetableController(timetableService);

                var result = await timetableController.Arrivals(exampleStop, CancellationToken.None);
                Assert.IsType<ActionResult<TimetableModel.Timetable>>(result);
            }
        }
    }
}
