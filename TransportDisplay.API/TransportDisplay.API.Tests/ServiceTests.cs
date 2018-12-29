using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc.Core;
using TransportDisplay.API.Services;
using TransportDisplay.API.Clients;
using TransportDisplay.API.Models;

namespace TransportDisplayApiTests
{
    public class ServiceTests
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
            var timetableClient = new Mock<ITimetableClient>();
            var arrivalEstimateClient = new Mock<IArrivalEstimateClient>();
            timetableClient.Setup(s => s.GetTimetableAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_mockTimetable);

            var timetableService = new TimetableService(
                timetableClient.Object,
                arrivalEstimateClient.Object);

            var output = await timetableService.FetchTimetableAsync(mockStop, CancellationToken.None);

            Assert.IsType<TimetableModel.Timetable>(output);
        }
    }
}
