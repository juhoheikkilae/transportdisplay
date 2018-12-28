using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Core;
using TransportDisplay.API.Controllers;
using TransportDisplay.API.Services;
using TransportDisplay.API.Models;

namespace TransportDisplay.API.Tests
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
            timetableService.Setup(s => s.FetchTimetableAsync(mockStop)).ReturnsAsync(_mockTimetable);

            var timetableController = new TimetableController(timetableService.Object);

            var output = await timetableController.GetAsync(mockStop);

            Assert.IsType<ActionResult<TimetableModel.Timetable>>(output);
        }
    }
}
