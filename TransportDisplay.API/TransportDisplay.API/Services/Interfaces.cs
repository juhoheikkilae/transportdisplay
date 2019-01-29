using System;
using System.Threading;
using System.Threading.Tasks;
using TransportDisplay.API.Models;

namespace TransportDisplay.API.Services
{
    public interface ITimetableService
    {
        Task<TimetableModel.Timetable> FetchTimetableAsync(string stop, CancellationToken cancellationToken);
        Task<TimetableModel.Timetable> FetchArrivalEstimatesAsync(string stop, CancellationToken cancellationToken);
        Task<TimetableModel.Stop[]> SearchStopsAsync(string search, CancellationToken cancellationToken);
    }

    public interface IWeatherService
    {
        Task<WeatherModel.Conditions> FetchConditions(string id, CancellationToken cancellationToken);
    }

    public interface IAlertService
    {
        Task<TimetableModel.Alert[]> FetchCurrentAlertsAsync(CancellationToken cancellationToken);
        Task BroadcastAlert(TimetableModel.Alert alert, CancellationToken cancellationToken);
    }
}
