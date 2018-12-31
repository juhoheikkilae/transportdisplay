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

            return await QueryHslGraphApiAsync<TimetableModel.Timetable, HslTimetableQueryResponse>(
                query,
                x => TimetableQueryResponseToTimetable(x),
                cancellationToken);
        }

        public async Task<TimetableModel.ArrivalEstimates> GetArrivalEstimatesAsync(
            string stop, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<TimetableModel.Stop[]> GetStopsAsync(string search, CancellationToken cancellationToken)
        {
            var query = String.Join(
                Environment.NewLine,
                "{",
                "  stops(name: \"" + search + "\") {",
                "    gtfsId",
                "    name",
                "  }",
                "}");

            return await QueryHslGraphApiAsync<TimetableModel.Stop[], HslStopsQueryResponse>(
                query,
                s => StopsQueryToStops(s),
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

        private static TimetableModel.Timetable TimetableQueryResponseToTimetable(
            HslTimetableQueryResponse response)
        {
            var responseData = response.Data;

            return new TimetableModel.Timetable
            {
                Stop = new TimetableModel.Stop
                {
                    Name = responseData.Stop.Name
                },
                Departures = responseData.Stop.StoptimesWithoutPatterns.Select(
                    s => new TimetableModel.Departure
                    {
                        Line = new TimetableModel.Line
                        {
                            Id = s.Trip.RouteShortName
                        },
                        Time = Helpers.DateTimeHelpers.FromUnixTime(s.ServiceDay + s.ScheduledDeparture)
                    }
                ).ToArray()
            };
        }

        private static TimetableModel.Stop[] StopsQueryToStops(
            HslStopsQueryResponse response)
        {
            var responseData = response.Data;
            return responseData.Stops.Select(s =>
                new TimetableModel.Stop
                {
                    Name = s.Name,
                    Id = s.GtfsId
                }).ToArray();
        }
    }
}
