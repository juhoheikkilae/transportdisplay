using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TransportDisplay.API.Models;
using System.Threading.Tasks;

namespace TransportDisplay.API.Clients
{
    public class HSLTimetableClient : ITimetableClient
    {
        private readonly HttpClient _httpClient;

        public HSLTimetableClient(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public Task<TransportTimetableModel.Timetable> GetTimetable(string stop)
        {
            string query = "";
            throw new NotImplementedException();
        }
    }
}
