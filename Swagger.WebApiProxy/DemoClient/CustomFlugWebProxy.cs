
using SwaggerServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DemoClient
{
    public class CustomFlugWebProxy : FlugWebProxy
    {
        public CustomFlugWebProxy(Uri baseUrl) : base(baseUrl)
        {
        }

        public string AuthToken { get; set; }

        protected override HttpClient BuildHttpClient()
        {
            var client = base.BuildHttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.AuthToken);
            return client;
        }



    }
}
