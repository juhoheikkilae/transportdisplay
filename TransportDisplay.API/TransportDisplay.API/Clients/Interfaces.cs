using System;
using System.Threading;
using System.Threading.Tasks;
using TransportDisplay.API.Models;

namespace TransportDisplay.API.Clients
{
    public interface ITimetableClient {
        Task<TimetableModel.Timetable> GetTimetableAsync(string stop, CancellationToken cancellationToken);
    }

    public interface IArrivalEstimateClient
    {
        
    }
}