using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TransportDisplay.API.Helpers
{
    public static class HttpResponseExtensions
    {
        public static Task<T> DeserializeResponseStream<T>(
            this Stream responseStream, CancellationToken cancellationToken)
        {
            if (!cancellationToken.IsCancellationRequested)
                return Task.Run(() => DeserializeJsonFromStream<T>(responseStream));

            throw new TaskCanceledException();
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
