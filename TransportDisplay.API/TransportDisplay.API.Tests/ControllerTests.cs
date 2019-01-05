using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Core;
using TransportDisplay.API.Controllers;
using TransportDisplay.API.Services;
using TransportDisplay.API.Models;
using TransportDisplay.API.Logger;
using static TransportDisplay.API.Tests.Mock.Mocks;

namespace TransportDisplayApiTests
{
    public class ControllerTests
    {
        // Should retrieve timetable from service
        [Fact]
        public async Task ShouldReturnTimetable()
        {
            var timetableService = new Mock<ITimetableService>();
            timetableService.Setup(_ => _.FetchTimetableAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockTimetable);

            var timetableController = new TimetableController(timetableService.Object, new DebugLogger());

            var output = await timetableController.ScheduledDepartures(mockStopId, CancellationToken.None);

            Assert.IsType<ActionResult<TimetableModel.Timetable>>(output);
        }

        // Should retrieve arrivals from service
        [Fact]
        public async Task ShouldReturnArrivals()
        {
            var timetableService = new Mock<ITimetableService>();
            timetableService.Setup(_ => _.FetchArrivalEstimatesAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockArrivals);

            var timetableController = new TimetableController(timetableService.Object, new DebugLogger());

            var output = await timetableController.Arrivals(mockStopId, CancellationToken.None);

            Assert.IsType<ActionResult<TimetableModel.ArrivalEstimates>>(output);
        }

        // Should retrieve stops from service
        [Fact]
        public async Task ShouldReturnStops()
        {
            var timetableService = new Mock<ITimetableService>();
            timetableService.Setup(_ => _.FetchStopsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockStopArray);

            var timetableController = new TimetableController(timetableService.Object, new DebugLogger());

            var output = await timetableController.Stops(mockStopId, CancellationToken.None);

            Assert.IsType<ActionResult<TimetableModel.Stop[]>>(output);
        }
    }
}
