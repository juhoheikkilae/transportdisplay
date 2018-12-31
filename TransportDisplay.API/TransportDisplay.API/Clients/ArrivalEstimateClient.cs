using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TransportDisplay.API.Models;

namespace TransportDisplay.API.Clients
{
    public class ArrivalEstimateClient : IArrivalEstimateClient
    {
        private readonly HttpClient _httpClient;

        public ArrivalEstimateClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TimetableModel.ArrivalEstimate[]> GetArrivalEstimatesAsync(
            string stop, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
