using System;
using System.Threading.Tasks;

namespace TransportDisplay.API.Logger
{
    public interface ILogger
    {
        Task Log(string message);
    }
}
