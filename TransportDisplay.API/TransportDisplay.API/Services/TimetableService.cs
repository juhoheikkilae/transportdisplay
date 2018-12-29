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
        private readonly IArrivalEstimateClient _arrivalEstimateClient;

        public TimetableService(ITimetableClient timetableClient, IArrivalEstimateClient arrivalEstimateClient)
        {
            _timetableClient = timetableClient;
            _arrivalEstimateClient = arrivalEstimateClient;
        }

        public async Task<ArrivalEstimate> FetchArrivalEstimatesAsync(string stop, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Timetable> FetchTimetableAsync(string stop, CancellationToken cancellationToken)
        {
                return await _timetableClient.GetTimetableAsync(stop, cancellationToken);
        }
    }
}
