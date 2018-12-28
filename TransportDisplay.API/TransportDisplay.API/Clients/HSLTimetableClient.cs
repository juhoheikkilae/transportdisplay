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
            
            var responseMessage = await _httpClient.PostStreamAsync(
                query,
                Constants.TransportApiBaseUri,
                "application/graphql",
                cancellationToken
            );

            var response = await (await responseMessage.Content.ReadAsStreamAsync())
                .DeserializeResponseStream<Responses.HslTimetableQueryResponse>(cancellationToken);
            return TimetableQueryResponseToTimetable(response);
        }

        private TimetableModel.Timetable TimetableQueryResponseToTimetable(
            Responses.HslTimetableQueryResponse response)
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
                        }
                    }
                ).ToArray()
            };
        }
    }
}
