using dentity.API;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Service2.IntegrationTest.Config
{
    public abstract class BaseIntegerationTest : IDisposable,
       IClassFixture<IdentityFixture<Startup>>
    {
        protected readonly HttpClient HttpClient;

        protected BaseIntegerationTest(IdentityFixture<Startup> testFixture
            )
        {
            HttpClient = testFixture.Client;
        }

        protected async Task<HttpResponseMessage> PostRequest(string url, object data)
        {
            var dataDicionary = Newtonsoft.Json.Linq.JObject.FromObject(data).ToObject<Dictionary<string, string>>();
            dataDicionary.Add("Content-Type", "application/x-www-form-urlencoded");
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            using HttpResponseMessage response = await HttpClient.PostAsync($"{AppDomainVariable.BaseAddress}/{url}", content);

            /// _client
            return response;
        }
        public void Dispose()
        {
        }
    }
}
