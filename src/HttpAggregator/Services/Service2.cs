using HttpAggregator.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpAggregator.Services
{
    public class Service2 : IService2
    {
        private readonly HttpClient _httpClient;
        private readonly WebApplicationOptions _webApplicationOptions;

        public Service2(HttpClient httpClient, IOptions<WebApplicationOptions> webApplicationOptions)
        {
            _httpClient = httpClient;
            _webApplicationOptions = webApplicationOptions.Value;
        }
        public async Task<string[]> GetValuesAsync()
        {
            var responce = await _httpClient.GetAsync(_webApplicationOptions.GetValues());

            var content = await responce.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<string[]>(content);
        }
    }
}
