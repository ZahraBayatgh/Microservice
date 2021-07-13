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
        public string Get(int id)
        {
            return "value";
        }

    }
}
