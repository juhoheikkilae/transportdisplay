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

namespace TransportDisplayApiTests
{
    public class ControllerTests
    {
        private static string mockStop = "mockStopId";
        internal TimetableModel.Timetable _mockTimetable = new TimetableModel.Timetable
        {
            Stop = new TimetableModel.Stop
            {
                Name = "MockStop"
            }
        };

        // Should retrieve timetable from service
        [Fact]
        public async Task ShouldReturnTimetable()
        {
            var timetableService = new Mock<ITimetableService>();
            timetableService.Setup(_ => _.FetchTimetableAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(_mockTimetable);

            var timetableController = new TimetableController(timetableService.Object, new DebugLogger());

            var output = await timetableController.ScheduledDepartures(mockStop, CancellationToken.None);

            Assert.IsType<ActionResult<TimetableModel.Timetable>>(output);
        }
    }
}
