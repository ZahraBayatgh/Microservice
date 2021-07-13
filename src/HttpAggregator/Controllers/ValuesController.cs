using HttpAggregator.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpAggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]

    public class ValuesController : ControllerBase
    {
        private readonly IService2 _service2;
        private readonly ILogger<ValuesController> _logger;

        public ValuesController(IService2 service2, ILogger<ValuesController> logger)
        {
            _service2 = service2;
            _logger = logger;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<string>> GetAsync()
        {
            _logger.LogInformation("GET values is invoking!");

            var result = await _service2.GetValuesAsync();

            return result;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        [ApiVersion("1.1")]
        public string Get1(int id)
        {
            return "value1";
        }

    }
}
