using Authentication.Identity;
using Authentication.Tokens;
using Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Identity.API.Services
{
    public class TokenService : ITokenService
    {
        private readonly ILogger<TokenService> _logger;
        private readonly CustomTokenFactory _tokenFactory;
        private readonly UserManager<CustomIdentityUser> _userManager;

        public TokenService(ILogger<TokenService> logger,
          CustomTokenFactory tokenFactory,
          UserManager<CustomIdentityUser> userManager)
        {
            _logger = logger;
            _tokenFactory = tokenFactory;
            _userManager = userManager;
        }
        public async Task<CustomToken> GetToken(TokenRequestModel model)
        {
            try
            {
                _logger.LogError("Calling Connect");

                // Allow by username or email
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user == null) user = await _userManager.FindByEmailAsync(model.Username);

                if (user == null) throw new NullReferenceException("Invalid User");

                var token = await _tokenFactory.GenerateForUser(user);
                return token;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
