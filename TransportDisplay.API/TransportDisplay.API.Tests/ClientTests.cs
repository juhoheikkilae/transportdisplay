using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using System.Net.Http;
using TransportDisplay.API.Models;
using TransportDisplay.API.Clients;
using TransportDisplay.API.Settings;
using Moq;
using Moq.Protected;
using System.Net;

namespace TransportDisplayApiTests
{
    public class ClientTests
    {
        private static string _weatherConditionsJson = "{\"coord\":{\"lon\":24.65,\"lat\":60.21},\"weather\":[{\"id\":803,\"main\":\"Clouds\",\"description\":\"broken clouds\",\"icon\":\"04n\"}],\"base\":\"stations\",\"main\":{\"temp\":266.15,\"pressure\":992,\"humidity\":92,\"temp_min\":266.15,\"temp_max\":266.15},\"visibility\":10000,\"wind\":{\"speed\":2.1,\"deg\":100},\"clouds\":{\"all\":75},\"dt\":1547670000,\"sys\":{\"type\":1,\"id\":1332,\"message\":0.0024,\"country\":\"FI\",\"sunrise\":1547622494,\"sunset\":1547646899},\"id\":660158,\"name\":\"Espoo\",\"cod\":200}";
        private static string _mockTimetableResponseJson = "{\"data\":{\"stop\":{\"name\":\"Matinkylä\",\"stoptimesWithoutPatterns\":[{\"scheduledArrival\":58020,\"realtimeArrival\":58020,\"arrivalDelay\":0,\"scheduledDeparture\":58020,\"realtimeDeparture\":58020,\"departureDelay\":0,\"realtime\":false,\"realtimeState\":\"SCHEDULED\",\"serviceDay\":1545948000,\"headsign\":\"Vuosaari\",\"trip\":{\"routeShortName\":\"M1\"}},{\"scheduledArrival\":58320,\"realtimeArrival\":58320,\"arrivalDelay\":0,\"scheduledDeparture\":58320,\"realtimeDeparture\":58320,\"departureDelay\":0,\"realtime\":false,\"realtimeState\":\"SCHEDULED\",\"serviceDay\":1545948000,\"headsign\":\"Vuosaari\",\"trip\":{\"routeShortName\":\"M1\"}},{\"scheduledArrival\":58620,\"realtimeArrival\":58620,\"arrivalDelay\":0,\"scheduledDeparture\":58620,\"realtimeDeparture\":58620,\"departureDelay\":0,\"realtime\":false,\"realtimeState\":\"SCHEDULED\",\"serviceDay\":1545948000,\"headsign\":\"Vuosaari\",\"trip\":{\"routeShortName\":\"M1\"}},{\"scheduledArrival\":58920,\"realtimeArrival\":58920,\"arrivalDelay\":0,\"scheduledDeparture\":58920,\"realtimeDeparture\":58920,\"departureDelay\":0,\"realtime\":false,\"realtimeState\":\"SCHEDULED\",\"serviceDay\":1545948000,\"headsign\":\"Vuosaari\",\"trip\":{\"routeShortName\":\"M1\"}},{\"scheduledArrival\":59220,\"realtimeArrival\":59220,\"arrivalDelay\":0,\"scheduledDeparture\":59220,\"realtimeDeparture\":59220,\"departureDelay\":0,\"realtime\":false,\"realtimeState\":\"SCHEDULED\",\"serviceDay\":1545948000,\"headsign\":\"Vuosaari\",\"trip\":{\"routeShortName\":\"M1\"}}]}}}";
        private static string _mockStopByIdResponse = "{\"data\":{\"stop\":{\"name\":\"Piispansilta\",\"stoptimesWithoutPatterns\":[{\"scheduledArrival\":83940,\"realtimeArrival\":83940,\"arrivalDelay\":0,\"scheduledDeparture\":83940,\"realtimeDeparture\":83940,\"departureDelay\":0,\"realtime\":false,\"realtimeState\":\"SCHEDULED\",\"serviceDay\":1547071200,\"headsign\":\"Kamppi\",\"trip\":{\"routeShortName\":\"147N\",\"id\":\"VHJpcDpIU0w6MjE0N05fMjAxOTAxMDlfVG9fMl8yMzAz\",\"directionId\":\"1\",\"route\":{\"longName\":\"Kamppi-Kivenlahti\"}}},{\"scheduledArrival\":84900,\"realtimeArrival\":84900,\"arrivalDelay\":0,\"scheduledDeparture\":84900,\"realtimeDeparture\":84900,\"departureDelay\":0,\"realtime\":false,\"realtimeState\":\"SCHEDULED\",\"serviceDay\":1547071200,\"headsign\":\"Kamppi\",\"trip\":{\"routeShortName\":\"165N\",\"id\":\"VHJpcDpIU0w6MjE2NU5fMjAxOTAxMDlfVG9fMl8yMzA2\",\"directionId\":\"1\",\"route\":{\"longName\":\"Kamppi-Saunalahti-Kauklahti\"}}},{\"scheduledArrival\":85140,\"realtimeArrival\":85140,\"arrivalDelay\":0,\"scheduledDeparture\":85140,\"realtimeDeparture\":85140,\"departureDelay\":0,\"realtime\":false,\"realtimeState\":\"SCHEDULED\",\"serviceDay\":1547071200,\"headsign\":\"Kamppi\",\"trip\":{\"routeShortName\":\"147N\",\"id\":\"VHJpcDpIU0w6MjE0N05fMjAxOTAxMDlfVG9fMl8yMzIz\",\"directionId\":\"1\",\"route\":{\"longName\":\"Kamppi-Kivenlahti\"}}},{\"scheduledArrival\":85740,\"realtimeArrival\":85740,\"arrivalDelay\":0,\"scheduledDeparture\":85740,\"realtimeDeparture\":85740,\"departureDelay\":0,\"realtime\":false,\"realtimeState\":\"SCHEDULED\",\"serviceDay\":1547071200,\"headsign\":\"Kamppi\",\"trip\":{\"routeShortName\":\"146N\",\"id\":\"VHJpcDpIU0w6MjE0Nk5fMjAxOTAxMDlfVG9fMl8yMzMz\",\"directionId\":\"1\",\"route\":{\"longName\":\"Kamppi-Kivenlahti-Saunalahti\"}}},{\"scheduledArrival\":86580,\"realtimeArrival\":86580,\"arrivalDelay\":0,\"scheduledDeparture\":86580,\"realtimeDeparture\":86580,\"departureDelay\":0,\"realtime\":false,\"realtimeState\":\"SCHEDULED\",\"serviceDay\":1547071200,\"headsign\":\"Kamppi\",\"trip\":{\"routeShortName\":\"173N\",\"id\":\"VHJpcDpIU0w6NjE3M05fMjAxOTAxMDlfVG9fMl8yMjU1\",\"directionId\":\"1\",\"route\":{\"longName\":\"Kamppi-Masala-Kirkkonummi-Upinniemi\"}}}]}}  }";
        private static string _mockStopsByNameResponse = "{\"data\":{\"stops\":[{\"gtfsId\":\"HSL:2314601\",\"name\":\"Matinkylä\",\"routes\":[{\"shortName\":\"M1\",\"longName\":\"Matinkylä - Vuosaari\"}]},{\"gtfsId\":\"HSL:2314602\",\"name\":\"Matinkylä\",\"routes\":[{\"shortName\":\"M1\",\"longName\":\"Matinkylä - Vuosaari\"}]},{\"gtfsId\":\"HSL:2313215\",\"name\":\"Matinkylän terv.as.\",\"routes\":[{\"shortName\":\"137\",\"longName\":\"Tynnyripuisto-Matinkallio-Piispankylä-Kuitinmäki\"}]},{\"gtfsId\":\"HSL:2314210\",\"name\":\"Matinkylä (M)\",\"routes\":[{\"shortName\":\"112\",\"longName\":\"Tapiola (M)-Niittykumpu-Haukilahti-Matinkylä (M)\"},{\"shortName\":\"138\",\"longName\":\"Tynnyripuisto-Nuottaniemi-Matinkallio\"},{\"shortName\":\"137\",\"longName\":\"Tynnyripuisto-Matinkallio-Piispankylä-Kuitinmäki\"}]},{\"gtfsId\":\"HSL:2314212\",\"name\":\"Matinkylä (M)\",\"routes\":[{\"shortName\":\"136\",\"longName\":\"Matinkylä(M)-Espoon keskus-Suna-Tuomarila\"}]},{\"gtfsId\":\"HSL:2314211\",\"name\":\"Matinkylä (M)\",\"routes\":[{\"shortName\":\"134\",\"longName\":\"Matinkylä(M)-Espoon keskus-Suvela-Tuomarila\"}]},{\"gtfsId\":\"HSL:2314214\",\"name\":\"Matinkylä (M)\",\"routes\":[{\"shortName\":\"147\",\"longName\":\"Matinkylä (M)-Iivisniemi-Soukka-Laurinlahti-Kivenlahti\"}]},{\"gtfsId\":\"HSL:2314213\",\"name\":\"Matinkylä (M)\",\"routes\":[{\"shortName\":\"145\",\"longName\":\"Matinkylä (M)-Soukka-Suvisaaristo\"},{\"shortName\":\"143\",\"longName\":\"Matinkylä (M)-Soukanniemi\"}]},{\"gtfsId\":\"HSL:2314216\",\"name\":\"Matinkylä (M)\",\"routes\":[{\"shortName\":\"159\",\"longName\":\"Matinkylä (M)-Latokaski\"}]},{\"gtfsId\":\"HSL:2314215\",\"name\":\"Matinkylä (M)\",\"routes\":[{\"shortName\":\"146\",\"longName\":\"Matinkylä (M)-Espoonlahti-Kivenlahti\"}]}]}  }";

        // Should return weather conditions from service
        [Fact]
        public async Task ShouldReturnWeatherConditions()
        {
            var handler = CreateMockHandler(_weatherConditionsJson);

            using (var httpClient = new HttpClient(handler.Object))
            {
                var weatherClient = new WeatherClient(httpClient, "");
                Assert.IsType<WeatherModel.Conditions>(
                    await weatherClient.GetConditionsAsync("_", CancellationToken.None)
                );
            }
        }

        // Should return timetable from service
        [Fact]
        public async Task ShouldFetchTimetableAsync()
        {
            var handler = CreateMockHandler(_mockTimetableResponseJson);

            // Example stop id
            string stopId = "HSL:2314601";

            using (var httpClient = new HttpClient(handler.Object))
            {
                var timeTableClient = new HslTimetableClient(httpClient);
                var timetable = await timeTableClient.GetTimetableAsync(stopId, CancellationToken.None);
                Assert.IsType<TimetableModel.Timetable>(timetable);
            }
        }

        [Fact]
        public async Task ShouldFetchArrivalEstimatesAsync()
        {
            var handler = CreateMockHandler(_mockTimetableResponseJson);

            // Piispansilta stop
            string stopId = "HSL:2311220";

            using (var httpClient = new HttpClient(handler.Object))
            {
                var arrivalEstimateClient = new HslTimetableClient(httpClient);
                var estimates = await arrivalEstimateClient.GetArrivalEstimatesAsync(stopId, CancellationToken.None);
                Assert.IsType<TimetableModel.Timetable>(estimates);
            }
        }

        [Fact]
        public async Task ShouldFetchStopsAsync()
        {
            var handler = CreateMockHandler(_mockStopsByNameResponse);

            string search = "Matinkyl";

            using (var httpClient = new HttpClient(handler.Object))
            {
                var client = new HslTimetableClient(httpClient);
                var stops = await client.SearchStopsAsync(search, CancellationToken.None);
                Assert.IsType<TimetableModel.Stop[]>(stops);
            }
        }

        [Fact]
        public async Task ShouldFetchStopAsync()
        {
            var handler = CreateMockHandler(_mockStopByIdResponse);

            // Piispansilta stop
            string stopId = "HSL:2311220";

            using (var httpClient = new HttpClient(handler.Object))
            {
                ITimetableClient client = new HslTimetableClient(httpClient);
                var stop = await client.GetStopByIdAsync(stopId, CancellationToken.None);
                Assert.IsType<TimetableModel.Stop>(stop);
            }
        }

        private static Mock<HttpMessageHandler> CreateMockHandler(string mockResp)
        {
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected().Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
                ).ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(mockResp)
                });
            return handler;
        }
    }
}