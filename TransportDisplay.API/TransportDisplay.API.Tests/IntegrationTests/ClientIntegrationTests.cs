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
            // Should return timetable from service
            [Fact]
            public async Task ShouldFetchTimetableAsync()
            {
                using (var cts = new CancellationTokenSource())
                {
                    // Example stop id
                    string stopId = "HSL:2314601";

                    var timeTableClient = new HslTimetableClient(
                        new HttpClient { BaseAddress = new Uri(Constants.TransportApiBaseUri) });

                    var timetable = await timeTableClient.GetTimetableAsync(stopId, cts.Token);

                    Assert.IsType<TimetableModel.Timetable>(timetable);
                }
            }
        }
    }
}
