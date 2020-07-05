using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Dynamics365_Benchmark.Model;
using Dynamics365_Benchmark.Rest;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics365_Benchmark
{

    public class Program
    {
        static void Main(string[] args) => BenchmarkRunner.Run<RestVsSoap>();
    }

    [RPlotExporter]
    [HtmlExporter]
    [CsvExporter]
    public class RestVsSoap
    {
        private CrmServiceClient _crm;
        private HttpClient _http;

        [GlobalSetup]
        public void Setup()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            _crm = new CrmServiceClient(ConfigurationManager.ConnectionStrings["CrmConnectionString"].ConnectionString);

            var token = TokenHelper.GetAccessToken();
            _http = new HttpClient()
            {
                BaseAddress = new Uri(ConfigurationManager.AppSettings["organizationUrl"]),
            };
            _http.DefaultRequestHeaders.Add("OData-MaxVersion", "4.0");
            _http.DefaultRequestHeaders.Add("OData-Version", "4.0");
            _http.DefaultRequestHeaders.Add("Prefer", "odata.include-annotations=\"*\"");
            _http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _http.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }

        [Params(1, 10, 100, 1000)]
        public int N;

        [Benchmark]
        public void CreateOrganizationService()
        {
            for (int i = 0; i < N; i++)
            {
                var entity = new Entity("account");
                entity["name"] = $"Test account {i}";
                _crm.Create(entity);
            }
        }

        [Benchmark]
        public async Task CreateRest()
        {
            for (int i = 0; i < N; i++)
            {
                var entity = new Account() { Name = $"test account {i}" };

                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "api/data/v9.1/accounts")
                {
                    Content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json")
                };
                message.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                await _http.SendAsync(message);
            }
        }
    }
}

