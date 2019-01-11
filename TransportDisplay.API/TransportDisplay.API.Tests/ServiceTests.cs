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
        // Ones commented out need mock configuration

        // Should retrieve timetable from service
        // [Fact]
        // public async Task ShouldReturnTimetable()
        // {
        //     var timetableClient = new Mock<ITimetableClient>();

        //     var timetableService = new TimetableService(timetableClient.Object);

        //     var output = await timetableService.FetchTimetableAsync(mockStopId, CancellationToken.None);

        //     Assert.IsType<TimetableModel.Timetable>(output);
        // }

        // [Fact]
        // public async Task ShouldReturnArrivals()
        // {
        //     var timetableClient = new Mock<ITimetableClient>();

        //     var timetableService = new TimetableService(timetableClient.Object);

        //     var output = await timetableService.FetchArrivalEstimatesAsync(mockStopId, CancellationToken.None);

        //     Assert.IsType<TimetableModel.Timetable>(output);
        // }

        [Fact]
        public async Task ShouldReturnStops()
        {
            var timetableClient = new Mock<ITimetableClient>();

            var timetableService = new TimetableService(timetableClient.Object);

            var output = await timetableService.SearchStopsAsync(mockStopId, CancellationToken.None);

            Assert.IsType<TimetableModel.Stop[]>(output);
        }
    }
}
