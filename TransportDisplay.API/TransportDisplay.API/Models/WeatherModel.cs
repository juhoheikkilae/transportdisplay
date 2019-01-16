using System;

namespace TransportDisplay.API.Models
{
    public class WeatherModel
    {
        public class Conditions
        {
            public string Place { get; set; }
            public double Temperature { get; set; }
            public string Description { get; set; }
        }
    }
}
