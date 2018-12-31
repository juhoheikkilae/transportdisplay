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
                // TODO
                string stopId = "validstopidneededhere";

                var arrivalEstimateClient = new ArrivalEstimateClient(_httpClient);

                var estimates = arrivalEstimateClient.GetArrivalEstimatesAsync(stopId, CancellationToken.None);

                Assert.IsType<TimetableModel.ArrivalEstimate[]>(estimates);
            }
        }
    }
}
