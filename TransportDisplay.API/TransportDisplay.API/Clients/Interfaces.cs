using System;
using System.Threading;
using System.Threading.Tasks;
using TransportDisplay.API.Models;

namespace TransportDisplay.API.Clients
{
    public interface ITimetableClient
    {
        Task<TimetableModel.Timetable> GetTimetableAsync(
            string stop, CancellationToken cancellationToken);

        Task<TimetableModel.Timetable> GetArrivalEstimatesAsync(
            string stop, CancellationToken cancellationToken);

        Task<TimetableModel.Stop> GetStopByIdAsync(
            string id, CancellationToken cancellationToken);

        Task<TimetableModel.Stop[]> SearchStopsAsync(
            string search, CancellationToken cancellationToken);

        Task<TimetableModel.Alert[]> GetCurrentAlertsAsync(
            CancellationToken cancellationToken);
    }

    // TODO: Interface should describe methods to subscribe
    // to relevant information about bus/tram locations.
    // Probably requires line number and location filtering
    // with bounding box. Will be implemented later.
    public interface IPositionDataClient
    {
        Task Connect();
        Task Disconnect();
        Task Subscribe(string topic);
    }

    public interface IWeatherClient
    {
        Task<WeatherModel.Conditions> GetConditionsAsync(
            string id, CancellationToken cancellationToken);
    }
}