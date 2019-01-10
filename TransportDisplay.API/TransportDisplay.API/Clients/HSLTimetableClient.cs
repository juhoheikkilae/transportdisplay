using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TransportDisplay.API.Models;
using TransportDisplay.API.Settings;
using TransportDisplay.API.Helpers;
using static TransportDisplay.API.Clients.Responses;

namespace TransportDisplay.API.Clients
{
    public class HslTimetableClient : ITimetableClient
    {
        private readonly HttpClient _httpClient;
        public HslTimetableClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TimetableModel.Timetable> GetTimetableAsync(
            string stop, CancellationToken cancellationToken)
            => await QueryHslGraphApiAsync(
                TimetableQuery(stop),
                response => response.Data.Stop.ToDepartureTimetable(),
                cancellationToken);

        public async Task<TimetableModel.Timetable> GetArrivalEstimatesAsync(
            string stop, CancellationToken cancellationToken)
            => await QueryHslGraphApiAsync(
                TimetableQuery(stop),
                response => response.Data.Stop.ToArrivalTimetable(),
                cancellationToken);

        public async Task<TimetableModel.Stop> GetStopByIdAsync(
            string id, CancellationToken cancellationToken)
            => await QueryHslGraphApiAsync(
                string.Join(Environment.NewLine,
                    "{",
                    "  stop(id: \"" + id + "\") {",
                    "    gtfsId",
                    "    name",
                    "    routes {",
                    "      shortName",
                    "      longName",
                    "    }",
                    "  }",
                    "}"),
                response => response.Data.Stop.ToStop(),
                cancellationToken
            );

        public async Task<TimetableModel.Stop[]> SearchStopsAsync(
            string search, CancellationToken cancellationToken)
            => await QueryHslGraphApiAsync(
                string.Join(Environment.NewLine,
                    "{",
                    "  stops(name: \"" + search + "\") {",
                    "    gtfsId",
                    "    name",
                    "    routes {",
                    "      shortName",
                    "      longName",
                    "    }",
                    "  }",
                    "}"),
                response => response.Data.Stops.ToStops(),
                cancellationToken);

        /// <summary>
        /// Query HSL graph API and map response to internal model
        /// </summary>
        /// <typeparam name="T">Response type</typeparam>
        /// <param name="query">Graph API query</param>
        /// <param name="transform">Transformation function from graph API response to desired type</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Transformed response</returns>
        private async Task<T> QueryHslGraphApiAsync<T>(
            string query, Func<HslApiResponse, T> transform, CancellationToken cancellationToken)
        {
            var responseMessage = await _httpClient.PostStreamAsync(
                query,
                Constants.TransportApiBaseUri,
                "application/graphql",
                cancellationToken
            );

            using (var contentStream = await responseMessage.Content.ReadAsStreamAsync())
            {
                var response = await contentStream.DeserializeResponseStream<HslApiResponse>(cancellationToken);
                return transform(response);
            }
        }

        private static string TimetableQuery(string stop)
            => string.Join(Environment.NewLine,
                "{",
                "  stop(id: \"" + stop + "\") {",
                "    name",
                "    gtfsId",
                "    stoptimesWithoutPatterns {",
                "      scheduledArrival",
                "      realtimeArrival",
                "      arrivalDelay",
                "      scheduledDeparture",
                "      realtimeDeparture",
                "      departureDelay",
                "      realtime",
                "      realtimeState",
                "      serviceDay",
                "      headsign",
                "      trip {",
                "        routeShortName",
                "        directionId",
                "        route {",
                "          longName",
                "        }",
                "      }",
                "    }",
                "  }  ",
                "}");
    }
}
