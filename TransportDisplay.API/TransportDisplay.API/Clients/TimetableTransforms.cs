using System;
using System.Linq;
using TransportDisplay.API.Models;
using TransportDisplay.API.Helpers;
using static TransportDisplay.API.Clients.Responses;

namespace TransportDisplay.API.Clients
{
    public static class HslTimetableQueryTransformExtensions
    {
        internal static TimetableModel.Timetable ToDepartureTimetable(
            this Stop stop) => new TimetableModel.Timetable
            {
                Stop = stop.ToStop(),
                Departures = stop.StoptimesWithoutPatterns.Select(
                    x => new TimetableModel.Departure
                    {
                        Line = x.Trip.ToLine(),
                        Time = Helpers.DateTimeHelpers.FromUnixTime(x.ServiceDay + x.ScheduledDeparture)
                    }
                ).ToArray()
            };

        internal static TimetableModel.Timetable ToArrivalTimetable(
            this Stop stop) => new TimetableModel.Timetable
            {
                Stop = stop.ToStop(),
                ArrivalEstimates = stop.StoptimesWithoutPatterns != null ?
                    stop.StoptimesWithoutPatterns.Select(x => new TimetableModel.ArrivalEstimate
                    {
                        Line = x.Trip.ToLine(),
                        ArrivesIn = new TimeSpan(0, 0,
                            (int)(x.ServiceDay + x.RealtimeArrival -
                            DateTimeHelpers.ToUnixTime(DateTime.UtcNow))).TotalMinutes,
                        IsRealtimeEstimate = x.Realtime
                    }).ToArray() : null
            };

        internal static TimetableModel.Stop[] ToStops(this Stop[] stops) =>
            stops.Select(
                s => s.ToStop()
            ).ToArray();

        internal static TimetableModel.Line ToLine(this Trip trip)
            => new TimetableModel.Line
            {
                Id = trip.RouteShortName,
                Origin = trip.Route != null
                    ? trip.Route.LongName.Split('-')[0].Trim()
                    : null,
                Destination = trip.Route != null
                    ? trip.Route.LongName.Split('-')[trip.Route.LongName.Split('-').Length - 1].Trim()
                    : null,
                Direction = trip.DirectionId == 0 ? TimetableModel.Direction.NORMAL : TimetableModel.Direction.REVERSED
            };

        internal static TimetableModel.Stop ToStop(this Stop stop) =>
            new TimetableModel.Stop
            {
                Name = stop.Name,
                Id = stop.GtfsId,
                Lines = stop.Routes != null ? stop.Routes.Select(r => new TimetableModel.Line
                {
                    Id = r.ShortName,
                    Name = r.LongName,
                    Origin = r.LongName.Split('-')[0]
                }).ToArray() : null
            };

        internal static TimetableModel.Alert[] ToAlerts(this Alert[] alerts)
            => alerts.Select(
                a => new TimetableModel.Alert {
                    Line = new TimetableModel.Line {
                        Id = a.Route.ShortName,
                        Name = a.Route.LongName
                    },
                    AlertText = a.AlertDescriptionText,
                    From = DateTimeHelpers.FromUnixTime(a.EffectiveStartDate),
                    To = DateTimeHelpers.FromUnixTime(a.EffectiveEndDate)
                }
            ).ToArray();
    }
}
