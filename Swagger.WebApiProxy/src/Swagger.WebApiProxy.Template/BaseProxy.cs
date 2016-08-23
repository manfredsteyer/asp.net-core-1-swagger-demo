using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Swagger.WebApiProxy.Template
{
    public abstract class BaseProxy
    {
        private readonly Uri _baseUrl;

        protected BaseProxy(Uri baseUrl)
        {
            _baseUrl = baseUrl;
        }

        // Added:
        public string AuthToken { get; set; }

        protected HttpClient BuildHttpClient()
        {
            var httpClient = new HttpClient();

            // Added:
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.AuthToken);

            httpClient.BaseAddress = _baseUrl;
            return httpClient;
        }

        public async Task EnsureSuccessStatusCodeAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            try
            {
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false); ;
                throw new SimpleHttpResponseException(response.StatusCode, content);
            }
            finally
            {
                if (response.Content != null)
                    response.Content.Dispose();
            }
        }
    }

    public class SimpleHttpResponseException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }

        public SimpleHttpResponseException(HttpStatusCode statusCode, string content)
            : base(content)
        {
            StatusCode = statusCode;
        }
    }
}
