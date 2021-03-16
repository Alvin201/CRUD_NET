using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Image_System
{
    public class GlobalVariables
    {
        public static HttpClientHandler handler = new HttpClientHandler();
        public static HttpClient WebApiClient = new HttpClient(handler);
        static GlobalVariables()
        {
            WebApiClient.BaseAddress = new Uri("http://localhost:1041/api/");
            WebApiClient.DefaultRequestHeaders.Clear();

            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}