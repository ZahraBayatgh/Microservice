using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service1.API.Services;
using System.Threading.Tasks;

namespace Service1.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IRemoteService remoteService;

        public WeatherForecastController(IRemoteService remoteService)
        {
            this.remoteService = remoteService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var values = await remoteService.GetValuesAsync();

            return Ok(values);
        }
    }
}
