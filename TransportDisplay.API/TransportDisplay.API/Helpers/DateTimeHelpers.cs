using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using System.Text;

namespace TransportDisplay.API.Helpers
{
    public class DateTimeHelpers
    {
        private static DateTimeOffset epoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, new TimeSpan(0));

        public static DateTimeOffset FromUnixTime(long unixTime) {
            return epoch.AddSeconds(unixTime);
        }

        public static TimeSpan SecondsToTime(long seconds) {
            return new TimeSpan(seconds * 10000);
        }
    }
}