using System;
using System.Threading.Tasks;
using TransportDisplay.API.Clients;
using static TransportDisplay.API.Models.TransportTimetableModel;

namespace TransportDisplay.API.Services
{
    public class TimetableService
    {
        private readonly ITimetableClient _timetableClient;
        async Task<Timetable> FetchTimetable(string stop) {
            throw new NotImplementedException();
        }
    }
}
