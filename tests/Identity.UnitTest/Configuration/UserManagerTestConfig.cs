using Authentication.Identity;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.UnitTest.Configuration
{
    public class UserManagerTestConfig
    {
        public Mock<UserManager<CustomIdentityUser>> MockUserManager()
        {
            Mock<IUserStore<CustomIdentityUser>> store = new Mock<IUserStore<CustomIdentityUser>>();
            CustomIdentityUser user = new CustomIdentityUser { UserName = "JonSmith@Sample.com", Email = "JonSmith@Sample.com", PasswordHash = "0e44ce7308af2b3de5232e4616403ce7d49ba2aec83f79c196409556422a4927" };

            var userManager = new Mock<UserManager<CustomIdentityUser>>(store.Object, null, null, null, null, null, null, null, null);

            userManager.Setup(x => x.FindByEmailAsync("JonSmith@Sample.com"))
             .Returns(Task.FromResult(user));

            IList<Claim> claims = new[]
           {
                new Claim("c1", "v1"),
                new Claim("c2", "v2"),
                new Claim("c3", "v3")
            };

            userManager.Setup(x => x.GetClaimsAsync(It.IsAny<CustomIdentityUser>()))
             .Returns(Task.FromResult(claims));


            userManager.Setup(x => x.CreateAsync(It.IsAny<CustomIdentityUser>()))
                .Returns(Task.FromResult(IdentityResult.Success));


            return userManager;
        }

    }
}
