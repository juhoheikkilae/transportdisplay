using System;
using TransportDisplay.API.Clients;

namespace TransportDisplay.API.Services
{
    public class ArrivalEstimateService : IArrivalEstimateService
    {
        private readonly IArrivalEstimateClient _arrivalEstimateClient;

        public ArrivalEstimateService(IArrivalEstimateClient arrivalEstimateClient)
        {
            _arrivalEstimateClient = arrivalEstimateClient;
        }
    }
}
