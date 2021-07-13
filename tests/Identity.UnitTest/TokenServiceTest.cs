using Authentication.Identity;
using Authentication.Tokens;
using Identity.API.Models;
using Identity.API.Services;
using Identity.UnitTest.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Identity.UnitTest
{
    public class TokenServiceTest
    {
        private readonly Mock<ILogger<TokenService>> _logger;
        private readonly CustomTokenFactory _tokenFactory;
        private readonly Mock<UserManager<CustomIdentityUser>> _userManager;
        private readonly TokenService _tokenService;

        public TokenServiceTest()
        {
            _logger = new Mock<ILogger<TokenService>>();
            _tokenFactory = new TokenFactoryConfig().MockTokenFactory();
            _userManager = new UserManagerTestConfig().MockUserManager();
            _tokenService = new TokenService(_logger.Object, _tokenFactory,_userManager.Object );
        }

        [Fact]
        public async Task CreateTokenAsync_When_Model_Is_Null_Throw_NullReferenceException()
        {
            // Act-Assert
            var result = await Assert.ThrowsAsync<NullReferenceException>((() =>
                        _tokenService.GetToken(null)));
        }
        [Fact]
        public async Task CreateTokenAsync_When_Model_Is_Empty_Throw_NullReferenceException()
        {
            // Arrange
            TokenRequestModel tokenRequestViewModel = new TokenRequestModel();

            // Act-Assert
            var result = await Assert.ThrowsAsync<NullReferenceException>((() =>
                        _tokenService.GetToken(tokenRequestViewModel)));
        }
      
    }

}
