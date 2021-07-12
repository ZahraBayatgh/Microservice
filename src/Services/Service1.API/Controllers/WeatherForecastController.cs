using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Service1.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public WeatherForecastController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("service2"); 
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var responce = await _httpClient.GetAsync("api/Values");
            var content = await responce.Content.ReadAsStringAsync();

           var values = JsonConvert.DeserializeObject<string[]>(content);

            return Ok(values);
        }
    }
}
