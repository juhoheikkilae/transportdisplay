using System;
using System.Threading.Tasks;
using static TransportDisplay.API.Models.TransportTimetableModel;

namespace TransportDisplay.API.Services
{
    public interface IArrivalEstimateService
    {
    }

    public interface ITimetableService
    {
        Task<Timetable> FetchTimetable(string stop);
    }
}
