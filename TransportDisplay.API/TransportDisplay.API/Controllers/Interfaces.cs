using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TransportDisplay.API.Models;

namespace TransportDisplay.API.Controllers
{
    public interface ITimetableController
    {
        Task<ActionResult<TimetableModel.Timetable>> ScheduledDepartures(string stop, CancellationToken cancellationToken);
        Task<ActionResult<TimetableModel.Timetable>> Arrivals(string stop, CancellationToken cancellationToken);
        Task<ActionResult<TimetableModel.Stop[]>> Stops(string search, CancellationToken cancellationToken);
    }
}