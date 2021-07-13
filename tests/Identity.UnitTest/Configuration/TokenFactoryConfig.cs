using Authentication.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using System;

namespace Identity.UnitTest.Configuration
{
    public class TokenFactoryConfig
    {
        public CustomTokenFactory MockTokenFactory()
        {
            string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"TestCommons\bin\" }, StringSplitOptions.None)[0];
            IConfiguration config = new ConfigurationBuilder()
               .SetBasePath(projectPath)
               .AddJsonFile("appsettings.json")
               .Build();
            TokenOptions tokenOptions = new TokenOptions();

            var option = new Mock<IOptionsSnapshot<TokenOptions>>();
            option.Setup(m => m.Value).Returns(tokenOptions);
            var userManager = new UserManagerTestConfig().MockUserManager();

            CustomTokenFactory result = new CustomTokenFactory(config, userManager.Object);

            return result;
        }
    }
}
