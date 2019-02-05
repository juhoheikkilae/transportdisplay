using System;

namespace TransportDisplay.API.Models
{
    public class TimetableModel
    {
        public class Timetable
        {
            public Stop Stop { get; set; }
            public Departure[] Departures { get; set; }
            public ArrivalEstimate[] ArrivalEstimates { get; set; }
        }

        public class ArrivalEstimate
        {
            public Line Line { get; set; }
            public double ArrivesIn { get; set; }
            public bool IsRealtimeEstimate { get; set; }
        }


        public class Departure
        {
            public Line Line { get; set; }
            public DateTimeOffset Time { get; set; }
        }

        public class Disturbance
        {
            public Line Line { get; set; }
            public DateTimeOffset From { get; set; }
            public DateTimeOffset To { get; set; }
            public string Message { get; set; }
        }

        public class Line
        {
            // Line id
            public string Id { get; set; }
            public string Name { get; set; }
            public string Origin { get; set; }
            public string Destination { get; set; }
            public Direction Direction { get; set; }
        }

        public class Stop
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public Line[] Lines { get; set; }
        }

        public class Alert
        {
            public string Id { get; set; }
            public Line Line { get; set; }
            public string AlertText { get; set; }
            public DateTimeOffset From { get; set; }
            public DateTimeOffset To { get; set; }
        }

        public enum Direction
        {
            NORMAL,
            REVERSED
        }
    }
}
