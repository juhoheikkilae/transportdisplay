using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using System.Net.Http;
using TransportDisplay.API.Models;
using TransportDisplay.API.Clients;
using TransportDisplay.API.Settings;

namespace TransportDisplayApiTests
{
    public partial class IntegrationTests
    {
        public class ClientTests
        {
            private static HttpClient _httpClient = new HttpClient{
                BaseAddress = new Uri(Constants.TransportApiBaseUri)};

            // Should return timetable from service
            [Fact]
            public async Task ShouldFetchTimetableAsync()
            {
                // Example stop id
                string stopId = "HSL:2314601";

                var timeTableClient = new HslTimetableClient(_httpClient);

                var timetable = await timeTableClient.GetTimetableAsync(stopId, CancellationToken.None);

                Assert.IsType<TimetableModel.Timetable>(timetable);
            }

            [Fact]
            public async Task ShouldFetchArrivalEstimatesAsync()
            {
                // Piispansilta stop
                string stopId = "HSL:2311220";
                var arrivalEstimateClient = new HslTimetableClient(_httpClient);
                var estimates = await arrivalEstimateClient.GetArrivalEstimatesAsync(stopId, CancellationToken.None);
                Assert.IsType<TimetableModel.Timetable>(estimates);
            }

            [Fact]
            public async Task ShouldFetchStopsAsync()
            {
                string search = "Matinkyl";
                var client = new HslTimetableClient(_httpClient);
                var stops = await client.SearchStopsAsync(search, CancellationToken.None);
                Assert.IsType<TimetableModel.Stop[]>(stops);
            }

            [Fact]
            public async Task ShouldFetchStopAsync()
            {
                // Piispansilta stop
                string stopId = "HSL:2311220";
                ITimetableClient client = new HslTimetableClient(_httpClient);
                var stop = await client.GetStopByIdAsync(stopId, CancellationToken.None);
                Assert.IsType<TimetableModel.Stop>(stop);
            }
        }
    }
}
