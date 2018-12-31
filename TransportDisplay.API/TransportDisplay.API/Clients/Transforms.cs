using System;
using System.Linq;
using TransportDisplay.API.Models;
using TransportDisplay.API.Helpers;
using static TransportDisplay.API.Clients.Responses;

namespace TransportDisplay.API.Clients
{
    public static class HslTimetableQueryTransformExtensions
    {
        internal static TimetableModel.Timetable ToTimetable(
            this HslApiResponse response) => new TimetableModel.Timetable
            {
                Stop = new TimetableModel.Stop
                {
                    Name = response.Data.Stop.Name
                },
                Departures = response.Data.Stop.StoptimesWithoutPatterns.Select(
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

        internal static TimetableModel.Stop[] ToStops(this HslApiResponse response) =>
            response.Data.Stops.Select(
                s => new TimetableModel.Stop
                {
                    Name = s.Name,
                    Id = s.GtfsId
                }
            ).ToArray();

        internal static TimetableModel.ArrivalEstimates ToArrivalEstimates(
            this HslApiResponse response) => new TimetableModel.ArrivalEstimates
            {
                Stop = new TimetableModel.Stop {
                    Name = response.Data.Stop.Name
                },
                Estimates = response.Data.Stop.StoptimesWithoutPatterns.Select(l => new TimetableModel.ArrivalEstimate {
                    Line = new TimetableModel.Line {
                        Id = l.Trip.RouteShortName
                    },
                    ArrivesIn = new TimeSpan(0, 0,
                        (int)(l.ServiceDay + l.RealtimeArrival - 
                        DateTimeHelpers.ToUnixTime(DateTime.UtcNow))),
                    IsRealtimeEstimate = l.Realtime
                }).ToArray()
            };
    }
}
