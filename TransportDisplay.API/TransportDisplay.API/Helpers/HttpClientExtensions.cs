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
        /// <summary>
        /// Query API and map response to internal model
        /// </summary>
        /// <typeparam name="T">Response type</typeparam>
        /// <typeparam name="R">API response type</typeparam>
        /// <param name="uri">API Uri</param>
        /// <param name="transform">Transformation function from graph API response to desired type</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Transformed response</returns>
        public static async Task<T> ApiGetAsync<T, R>(
            this HttpClient client, string uri, Func<R, T> transform, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var request = new HttpRequestMessage(HttpMethod.Get, uri))
            using (var response = await client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
            {
                response.EnsureSuccessStatusCode();

                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    return transform(responseStream.DeserializeResponseStream<R>(cancellationToken));
                }
            }
        }

        /// <summary>
        /// Query API and map response to internal model
        /// </summary>
        /// <typeparam name="T">Response type</typeparam>
        /// <typeparam name="R">API response type</typeparam>
        /// <param name="query">Graph API query</param>
        /// <param name="uri">API Uri</param>
        /// <param name="transform">Transformation function from graph API response to desired type</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Transformed response</returns>
        public static async Task<T> ApiPostAsync<T, R>(
            this HttpClient client, string uri, string query, string contentType, Func<R, T> transform, CancellationToken cancellationToken)
        {
            var responseMessage = await client.PostStreamAsync(
                query,
                uri,
                contentType,
                cancellationToken
            );

            using (var contentStream = await responseMessage.Content.ReadAsStreamAsync())
            {
                return transform(contentStream.DeserializeResponseStream<R>(cancellationToken));
            }
        }

        public static async Task<HttpResponseMessage> PostStreamAsync(
            this HttpClient httpClient, object content, string url, string contentType, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var request = new HttpRequestMessage(HttpMethod.Post, url))
            using (var httpContent = CreateHttpContent(content, contentType))
            {
                request.Content = await httpContent;

                var resp = await httpClient.SendAsync(
                    request,
                    HttpCompletionOption.ResponseHeadersRead,
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
