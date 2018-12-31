using System;
using System.Threading;
using System.Threading.Tasks;
using TransportDisplay.API.Models;

namespace TransportDisplay.API.Clients
{
    public interface ITimetableClient {
        Task<TimetableModel.Timetable> GetTimetableAsync(
            string stop, CancellationToken cancellationToken);
    }

    // Used to fetch arrival estimate information for a stop
    public interface IArrivalEstimateClient
    {
        Task<TimetableModel.ArrivalEstimate[]> GetArrivalEstimatesAsync(
            string stop, CancellationToken cancellationToken);
    }

    // TODO: Interface should describe methods to subscribe
    // to relevant information about bus/tram locations.
    // Probably requires line number and location filtering
    // with bounding box.
    public interface IPositionDataClient
    {
        Task Connect();
        Task Disconnect();
        Task Subscribe(string topic);
    }
}