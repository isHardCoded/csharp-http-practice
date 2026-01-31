using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace http_practice.Infrastructure
{
    public static class ApiClient
    {
        public static HttpClient Instance { get; }

        static ApiClient()
        {
            Instance = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:8080")
            };
        }
    }
}
