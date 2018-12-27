using System;
using System.Threading.Tasks;
using static TransportDisplay.API.Models.TransportTimetableModel;

namespace TransportDisplay.API.Clients
{
    public interface ITimetableClient {
        Task<Timetable> GetTimetable(string stop);
    }

    public interface IArrivalEstimateClient
    {
        
    }
}