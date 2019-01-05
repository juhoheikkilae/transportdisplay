using System;
using System.Threading;
using System.Threading.Tasks;
using static TransportDisplay.API.Models.TimetableModel;

namespace TransportDisplay.API.Services
{
    public interface ITimetableService
    {
        Task<Timetable> FetchTimetableAsync(string stop, CancellationToken cancellationToken);
        Task<ArrivalEstimates> FetchArrivalEstimatesAsync(string stop, CancellationToken cancellationToken);
        Task<Stop[]> FetchStopsAsync(string search, CancellationToken cancellationToken);
    }
}
