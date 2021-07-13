using Authentication.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Tokens
{
    public class CustomTokenFactory
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly CustomTokenOptions _tokenOptions = new CustomTokenOptions();

        public CustomTokenFactory(IConfiguration configuration, UserManager<CustomIdentityUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<CustomToken> GenerateForUser(CustomIdentityUser user, string optionsKey = "TokenOptions")
        {
            _configuration.Bind(optionsKey, _tokenOptions);

            // Create the token
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, user.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            var userClaims = await _userManager.GetClaimsAsync(user);

            claims.AddRange(userClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SigningKey));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
              _tokenOptions.Issuer,
              _tokenOptions.Audience,
              claims,
              expires: DateTime.Now.AddMinutes(_tokenOptions.ExpirationLength),
              signingCredentials: signingCredentials);

            return new CustomToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };
        }
    }
}
