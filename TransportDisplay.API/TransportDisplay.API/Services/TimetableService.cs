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

        public TimetableService(ITimetableClient timetableClient) => _timetableClient = timetableClient;

        public async Task<Timetable> FetchArrivalEstimatesAsync(string stop, CancellationToken cancellationToken)
            => await _timetableClient.GetArrivalEstimatesAsync(stop, cancellationToken);

        public async Task<Timetable> FetchTimetableAsync(string stop, CancellationToken cancellationToken)
            => await _timetableClient.GetTimetableAsync(stop, cancellationToken);

        public async Task<Stop[]> SearchStopsAsync(string search, CancellationToken cancellationToken)
            => await _timetableClient.SearchStopsAsync(search, cancellationToken);

        public async Task<Alert[]> FetchCurrentAlertsAsync(CancellationToken cancellationToken)
            => await _timetableClient.GetCurrentAlertsAsync(cancellationToken);
    }
}
