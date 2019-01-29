using System;
using Microsoft.AspNetCore.SignalR;
using TransportDisplay.API.Services;

namespace TransportDisplay.API.SignalR
{
    public class AlertHub : Hub<IAlertService>
    {
    }
}
