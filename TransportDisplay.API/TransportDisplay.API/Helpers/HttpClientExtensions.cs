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
    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> PostStreamAsync(
            this HttpClient httpClient, object content, string url, string contentType, CancellationToken cancellationToken)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, url))
            using (var httpContent = CreateHttpContent(content, contentType))
            {
                request.Content = await httpContent;

                var resp = await httpClient.SendAsync(
                    request,
                    HttpCompletionOption.ResponseContentRead,
                    cancellationToken
                );
                
                resp.EnsureSuccessStatusCode();
                return resp;
            }
        }

        private static async Task WriteStringToStreamAsync(string value, Stream stream)
        {
            using (var sw = new StreamWriter(stream, new UTF8Encoding(false), 1024, true))
            {
                await sw.WriteAsync(value);
                await sw.FlushAsync();
            }
        }

        private static async Task<HttpContent> CreateHttpContent(object content, string contentType)
        {
            HttpContent httpContent = null;

            if (content.GetType() == typeof(string))
            {
                var ms = new MemoryStream();
                await WriteStringToStreamAsync((string)content, ms);
                ms.Seek(0, SeekOrigin.Begin);
                httpContent = new StreamContent(ms);
                httpContent.Headers.ContentType =
                    new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
                return httpContent;
            }

            throw new NotImplementedException("Method only supports strings!");
        }
    }
}
