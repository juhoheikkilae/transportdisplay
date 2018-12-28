using System;
using System.Threading.Tasks;
using Xunit;
using System.Net.Http;
using TransportDisplay.API.Models;
using TransportDisplay.API.Clients;
using TransportDisplay.API.Settings;

namespace TransportDisplay.API.Tests
{
    public class ClientIntegrationTests
    {
        // Should return timetable from service
        [Fact]
        public async Task ShouldFetchTimetableAsync()
        {
            // Example stop id
            string stopId = "HSL:2314601";

            var timeTableClient = new HslTimetableClient(
                new HttpClient { BaseAddress = new Uri(Constants.TransportApiBaseUri) });

            var timetable = await timeTableClient.GetTimetableAsync(stopId);

            Assert.IsType<TimetableModel.Timetable>(timetable);
        }
    }
}
