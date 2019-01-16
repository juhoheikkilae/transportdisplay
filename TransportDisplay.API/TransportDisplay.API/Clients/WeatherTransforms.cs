using System;
using TransportDisplay.API.Models;

namespace TransportDisplay.API.Clients
{
    public static class WeatherTransformExtensions
    {
        internal static WeatherModel.Conditions ToConditions(this Responses.OpenWeatherMapResponse response) {
            return new WeatherModel.Conditions {
                Description = response.Weather[0].Description,
                Place = response.Name,
                Temperature = response.Main.Temp - 273.15
            };
        }
    }
}
