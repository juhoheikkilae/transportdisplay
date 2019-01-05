using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc.Core;
using TransportDisplay.API.Services;
using TransportDisplay.API.Clients;
using TransportDisplay.API.Models;
using static TransportDisplay.API.Tests.Mock.Mocks;

namespace TransportDisplayApiTests
{
    public class ServiceTests
    {
        // Should retrieve timetable from service
        [Fact]
        public async Task ShouldReturnTimetable()
        {
            var timetableClient = new Mock<ITimetableClient>();
            timetableClient.Setup(s => s.GetTimetableAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockTimetable);

            var timetableService = new TimetableService(timetableClient.Object);

            var output = await timetableService.FetchTimetableAsync(mockStopId, CancellationToken.None);

            Assert.IsType<TimetableModel.Timetable>(output);
        }

        [Fact]
        public async Task ShouldReturnArrivals()
        {
            var timetableClient = new Mock<ITimetableClient>();
            timetableClient.Setup(s => s.GetArrivalEstimatesAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockArrivals);

            var timetableService = new TimetableService(timetableClient.Object);

            var output = await timetableService.FetchArrivalEstimatesAsync(mockStopId, CancellationToken.None);

            Assert.IsType<TimetableModel.ArrivalEstimates>(output);
        }

        [Fact]
        public async Task ShouldReturnStops()
        {
            var timetableClient = new Mock<ITimetableClient>();
            timetableClient.Setup(s => s.GetStopsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockStopArray);

            var timetableService = new TimetableService(timetableClient.Object);

            var output = await timetableService.FetchStopsAsync(mockStopId, CancellationToken.None);

            Assert.IsType<TimetableModel.Stop[]>(output);
        }
    }
}
