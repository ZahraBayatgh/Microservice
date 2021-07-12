using Authentication.Tokens;
using Identity.API.Models;
using Identity.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace dentity.API.Controllers
{
    [ApiController]
    [Route("auth/[controller]")]
    public class ConnectController : ControllerBase
    {
        public ConnectController(ITokenService tokenService)
        {
            TokenService = tokenService;
        }

        public ITokenService TokenService { get; }

        [HttpPost]
        public async Task<ActionResult<CustomToken>> Post([FromBody] TokenRequestModel model)
        {
            var token = await TokenService.GetToken(model);

            return Created("", token);
        }
    }
}
