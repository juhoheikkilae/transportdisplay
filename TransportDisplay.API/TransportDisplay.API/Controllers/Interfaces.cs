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

    public interface IWeatherController
    {
        Task<ActionResult<WeatherModel.Conditions>> Conditions(string id, CancellationToken cancellationToken);
    }

    public interface IAlertController {
        Task<ActionResult<TimetableModel.Alert[]>> Get(CancellationToken cancellationToken);
        Task<ActionResult> Post(TimetableModel.Alert alert, CancellationToken cancellationToken);
    }
}