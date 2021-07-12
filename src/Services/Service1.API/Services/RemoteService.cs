using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Service1.API.Services
{
    public class RemoteService : IRemoteService
    {
        private readonly HttpClient _httpClient;

        public RemoteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string[]> GetValuesAsync()
        {
            var responce = await _httpClient.GetAsync("api/Values");
            var content = await responce.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<string[]>(content);
        }
    }
}
