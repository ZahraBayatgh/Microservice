using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service2.Services;
using System;
using System.Collections.Generic;

namespace Service2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        public IValueService ValueService { get; }

        public ValuesController(IValueService valueService)
        {
            ValueService = valueService;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        [Authorize]
        public IEnumerable<string> Get()
        {
            DateTime cacheMessageTime=DateTime.Now;
            return ValueService.GetValues(cacheMessageTime.ToString());
        }

    }
}
