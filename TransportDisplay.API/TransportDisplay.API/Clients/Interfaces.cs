using System;
using System.Threading;
using System.Threading.Tasks;
using static TransportDisplay.API.Models.TimetableModel;

namespace TransportDisplay.API.Clients
{
    public interface ITimetableClient {
        Task<Timetable> GetTimetableAsync(string stop, CancellationToken cancellationToken);
    }

    public interface IArrivalEstimateClient
    {
        
    }
}