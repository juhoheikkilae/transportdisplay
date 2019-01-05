using System;
using TransportDisplay.API.Models;

namespace TransportDisplay.API.Tests.Mock
{
    public class Mocks
    {
        internal static string mockStopId = "mockStopId";
        internal static string mockStopName = "mockStopName";
        internal static string mockSearch = "search";

        internal static TimetableModel.Stop mockStop = new TimetableModel.Stop
        {
            Name = mockStopName,
            Id = mockStopId
        };

        internal static TimetableModel.Stop[] mockStopArray = { mockStop };

        internal static TimetableModel.Timetable mockTimetable = new TimetableModel.Timetable
        {
            Stop = mockStop
        };

        internal static TimetableModel.ArrivalEstimates mockArrivals = new TimetableModel.ArrivalEstimates
        {
            Stop = mockStop
        };
    }
}
