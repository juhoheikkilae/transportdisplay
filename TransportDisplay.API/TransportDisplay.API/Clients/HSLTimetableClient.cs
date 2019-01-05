using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using TransportDisplay.API.Models;
using TransportDisplay.API.Settings;
using TransportDisplay.API.Helpers;
using System.Linq;
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
        {
            System.Diagnostics.Debug.WriteLine("Fetching timetables!");

            string query = String.Join(
                Environment.NewLine,
                "{",
                "  stop(id: \"" + stop + "\") {",
                "    name",
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
                "      }",
                "    }",
                "  }  ",
                "}");

            return await QueryHslGraphApiAsync<TimetableModel.Timetable, HslApiResponse>(
                query,
                response => response.ToTimetable(),
                cancellationToken);
        }

        public async Task<TimetableModel.ArrivalEstimate[]> GetArrivalEstimatesAsync(
            string stop, CancellationToken cancellationToken)
        {
            string query = String.Join(
                Environment.NewLine,
                "{",
                "  stop(id: \"" + stop + "\") {",
                "    name",
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
                "      }",
                "    }",
                "  }  ",
                "}");

            return await QueryHslGraphApiAsync<TimetableModel.ArrivalEstimate[], HslApiResponse>(
                query,
                response => response.ToArrivalEstimates(),
                cancellationToken);
        }

        public async Task<TimetableModel.Stop> GetStopByIdAsync(string id, CancellationToken cancellationToken)
        {
            var query = String.Join(
                Environment.NewLine,
                "{",
                "  stop(id: \"" + id + "\") {",
                "    gtfsId",
                "    name",
                "  }",
                "}");
            
            return await QueryHslGraphApiAsync<TimetableModel.Stop, HslApiResponse>(
                query,
                response => response.ToStop(),
                cancellationToken
            );
        }

        public async Task<TimetableModel.Stop[]> SearchStopsAsync(string search, CancellationToken cancellationToken)
        {
            var query = String.Join(
                Environment.NewLine,
                "{",
                "  stops(name: \"" + search + "\") {",
                "    gtfsId",
                "    name",
                "  }",
                "}");

            return await QueryHslGraphApiAsync<TimetableModel.Stop[], HslApiResponse>(
                query,
                response => response.ToStops(),
                cancellationToken
            );
        }

        // T is return type.
        // R is type the graph api response is deserialized to before transformation.
        // T transform(R) is function to transform API response to return type.
        private async Task<T> QueryHslGraphApiAsync<T, R>(string query, Func<R, T> transform, CancellationToken cancellationToken)
        {
            var responseMessage = await _httpClient.PostStreamAsync(
                query,
                Constants.TransportApiBaseUri,
                "application/graphql",
                cancellationToken
            );

            using (var contentStream = await responseMessage.Content.ReadAsStreamAsync())
            {
                var response = await contentStream.DeserializeResponseStream<R>(cancellationToken);
                return transform(response);
            }
        }
    }
}
