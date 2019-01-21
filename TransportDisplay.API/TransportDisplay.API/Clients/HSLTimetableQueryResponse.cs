using Newtonsoft.Json;

namespace TransportDisplay.API.Clients
{
    public partial class Responses
    {
        public partial class HslApiResponse
        {
            [JsonProperty("data")]
            public Data Data { get; set; }
        }

        public partial class Data
        {
            [JsonProperty("stop")]
            public Stop Stop { get; set; }

            [JsonProperty("stops")]
            public Stop[] Stops { get; set; }

            [JsonProperty("routes")]
            public Route[] Routes { get; set; }

            [JsonProperty("alerts")]
            public Alert[] Alerts { get; set; }
        }

        public partial class Stop
        {
            [JsonProperty("gtfsId")]
            public string GtfsId { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("routes")]
            public Route[] Routes { get; set; }

            [JsonProperty("stoptimesWithoutPatterns")]
            public StoptimesWithoutPattern[] StoptimesWithoutPatterns { get; set; }
        }

        public partial class Route
        {
            [JsonProperty("shortName")]
            public string ShortName { get; set; }

            [JsonProperty("longName")]
            public string LongName { get; set; }

            [JsonProperty("trips")]
            public Trip[] Trips { get; set; }

            [JsonProperty("alerts")]
            public Alert[] Alerts { get; set; }
        }

        public partial class StoptimesWithoutPattern
        {
            [JsonProperty("scheduledArrival")]
            public long ScheduledArrival { get; set; }

            [JsonProperty("realtimeArrival")]
            public long RealtimeArrival { get; set; }

            [JsonProperty("arrivalDelay")]
            public long ArrivalDelay { get; set; }

            [JsonProperty("scheduledDeparture")]
            public long ScheduledDeparture { get; set; }

            [JsonProperty("realtimeDeparture")]
            public long RealtimeDeparture { get; set; }

            [JsonProperty("departureDelay")]
            public long DepartureDelay { get; set; }

            [JsonProperty("realtime")]
            public bool Realtime { get; set; }

            [JsonProperty("realtimeState")]
            public string RealtimeState { get; set; }

            [JsonProperty("serviceDay")]
            public long ServiceDay { get; set; }

            [JsonProperty("headsign")]
            public string Headsign { get; set; }

            [JsonProperty("trip")]
            public Trip Trip { get; set; }
        }

        public partial class Trip
        {
            [JsonProperty("routeShortName")]
            public string RouteShortName { get; set; }

            [JsonProperty("directionId")]
            public int DirectionId { get; set; }

            [JsonProperty("route")]
            public Route Route { get; set; }
        }

        public partial class Alert
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("alertDescriptionText")]
            public string AlertDescriptionText { get; set; }

            [JsonProperty("effectiveStartDate")]
            public long EffectiveStartDate { get; set; }

            [JsonProperty("effectiveEndDate")]
            public long EffectiveEndDate { get; set; }

            [JsonProperty("route")]
            public Route Route { get; set; }
        }
    }
}