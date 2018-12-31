using System;
using System.Threading;
using System.Threading.Tasks;
using TransportDisplay.API.Clients;
using TransportDisplay.API.Models;
using static TransportDisplay.API.Models.TimetableModel;

namespace TransportDisplay.API.Services
{
    public class TimetableService : ITimetableService
    {
        private readonly ITimetableClient _timetableClient;

        public TimetableService(ITimetableClient timetableClient)
        {
            _timetableClient = timetableClient;
        }

        public async Task<ArrivalEstimate> FetchArrivalEstimatesAsync(string stop, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Timetable> FetchTimetableAsync(string stop, CancellationToken cancellationToken)
        {
            return await _timetableClient.GetTimetableAsync(stop, cancellationToken);
        }

        public async Task<Stop[]> FetchStopsAsync(string search, CancellationToken cancellationToken)
        {
            return await _timetableClient.GetStopsAsync(search, cancellationToken);
        }
    }
}
