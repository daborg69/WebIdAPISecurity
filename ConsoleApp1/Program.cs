using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Azure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Abstractions;
using Microsoft.Identity.Web;
using RestSharp;
using WebApplication1.Models;

namespace ConsoleApp1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // .Net MSAL 2023

            // Get the Token acquirer factory instance. By default it reads an appsettings.json
            // file if it exists in the same folder as the app (make sure that the 
            // "Copy to Output Directory" property of the appsettings.json file is "Copy if newer").
            var tokenAcquirerFactory = TokenAcquirerFactory.GetDefaultInstance();

            // Create a downstream API service named 'MyApi' which comes loaded with several
            // utility methods to make HTTP calls to the DownstreamApi configurations found
            // in the "MyWebApi" section of your appsettings.json file.
            tokenAcquirerFactory.Services.AddDownstreamApi("MyApi",
                                                           tokenAcquirerFactory.Configuration.GetSection("MyWebApi"));
            var sp = tokenAcquirerFactory.Build();

            // Extract the downstream API service from the 'tokenAcquirerFactory' service provider.
            var api = sp.GetRequiredService<IDownstreamApi>();

            // You can use the API service to make direct HTTP calls to your API. Token
            // acquisition is handled automatically based on the configurations in your
            // appsettings.json file.
            //var result = api.GetForAppAsync<string>("MyApi", options => { options.RelativePath = "WeatherForecast"; });

            var result  = await api.GetForAppAsync<IEnumerable<TodoItem>>("MyApi", options => options.RelativePath = "api/TodoList");
            var result2 = await api.GetForAppAsync<String>("MyApi", options => options.RelativePath = "WeatherForecast");
            Console.WriteLine($"result = {result}");


            // .Net MSAL - Acquire Token
            /*
            string tenant   = "8574f991-f2d3-4413-90fa-1a24cba1e9f6";
            string clientId = "f89ddeb4-9513-4093-acff-5fd03601a7b3";
            string scope    = "api://847509a8-6023-46c6-afb7-cac6763a1750/.default";
            string secret   = "sLK8Q~YwrqBuX6g3sNe5PqZIsNgqOwQzOJv1Sc-7";
            string grant    = "client_credentials";


            HttpClient httpClient = new HttpClient();
            Uri        requestUri = new Uri($"https://login.microsoftonline.com/{tenant}/oauth2/v2.0/token");
            httpClient.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded")); //ACCEPT header

            // Parameter
            StringContent s = new StringContent($"grant_type={grant}&client_id={clientId}&scope={scope}&client_secret={secret}", Encoding.UTF8,
                                                "application/x-www-form-urlencoded");
            var response = httpClient.PostAsync(requestUri.ToString(), s).Result;
            var token    = response.Content.ReadAsStringAsync().Result;


            // Call the API.
            Uri apiUri = new Uri($"https://localhost:7147/WeatherForecast");
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Bearer", token);
            response = httpClient.GetAsync(apiUri.ToString()).Result;
            string text = response.Content.ReadAsStringAsync().Result;

            */
            // Auth0
            /*
            Console.WriteLine("Hello, World!");
            var client  = new RestClient("https://dev-kthygyk40xqm1u7o.us.auth0.com/oauth/token");
            var request = new RestRequest();
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json",
                                 "{\"client_id\":\"F9AGnQELiXaNu0252gKkG8it2MxSw9Fh\",\"client_secret\":\"sjIQ90ysvow5udD5sJlC--tXq9TMTwYt9tLL-EKwlXhKXFwJToJDw--R0b5_FXtM\",\"audience\":\"https://localhost:7147/\",\"grant_type\":\"client_credentials\"}",
                                 ParameterType.RequestBody);
            var response = client.Execute(request);
            */
        }
    }
}