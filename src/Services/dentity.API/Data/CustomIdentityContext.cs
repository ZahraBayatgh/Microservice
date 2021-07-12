using Authentication.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dentity.API.Data
{
    public class CustomIdentityContext : IdentityDbContext<CustomIdentityUser>
    {
        public CustomIdentityContext(DbContextOptions options) : base(options)
        {
        }

    }
}
