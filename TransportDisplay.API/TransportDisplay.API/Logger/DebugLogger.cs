using System;
using System.Threading.Tasks;

namespace TransportDisplay.API.Logger
{
    public class DebugLogger : ILogger
    {
        public async Task Log(string message) {
            System.Diagnostics.Debug.WriteLine($"{DateTime.Now}: {message}");
        }
    }
}
