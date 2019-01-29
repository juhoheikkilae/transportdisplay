using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TransportDisplay.API.Models;
using TransportDisplay.API.SignalR;
using TransportDisplay.API.Clients;

namespace TransportDisplay.API.Services
{
    public class AlertService : IAlertService
    {
        private readonly IHubContext<AlertHub> _alertHub;

        private readonly ITimetableClient _timetableClient;

        public AlertService(ITimetableClient timetableClient, IHubContext<AlertHub> context)
        {
            _alertHub = context;
            _timetableClient = timetableClient;
        }

        public async Task<TimetableModel.Alert[]> FetchCurrentAlertsAsync(CancellationToken cancellationToken)
            => await _timetableClient.GetCurrentAlertsAsync(cancellationToken);

        public async Task BroadcastAlert(TimetableModel.Alert alert, CancellationToken cancellationToken)
        {
            await _alertHub.Clients.All.SendAsync("alert", alert, cancellationToken);
        }
    }
}
