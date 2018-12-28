using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TransportDisplay.API.Helpers
{
    public static class HttpResponseExtensions
    {
        public static async Task<T> DeserializeResponseStream<T>(
            this Stream responseStream, CancellationToken cancellationToken)
        {
            return DeserializeJsonFromStream<T>(responseStream);
        }

        private static T DeserializeJsonFromStream<T>(Stream stream)
        {
            if (stream == null || !stream.CanRead)
                return default(T);

            using (var sr = new StreamReader(stream))
            using (var jtr = new JsonTextReader(sr))
            {
                var js = new JsonSerializer();
                return js.Deserialize<T>(jtr);
            }
        }
    }
}
