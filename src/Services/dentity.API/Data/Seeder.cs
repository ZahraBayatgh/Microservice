using Authentication.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dentity.API.Data
{
    public static class Seeder
    {
        private static IServiceScope GenerateServiceScope()
        {
            var serviceCollection = new ServiceCollection();

            // ICollection
            var builder = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json")
              .AddJsonFile("appsettings.Development.json", true)
              .AddEnvironmentVariables();

            var config = builder.Build();
            serviceCollection.AddSingleton<IConfiguration>(config);

            // DbContext
            serviceCollection.AddDbContext<CustomIdentityContext>(option =>
            {
                var connString = config.GetConnectionString("AuthDb");
                if (string.IsNullOrEmpty(connString))
                {
                    Console.WriteLine("ConnectionString Missing!");
                    throw new InvalidProgramException("Missing Connection String");
                }
                option.UseSqlServer(connString);
            });

            // Identity
            serviceCollection.AddIdentityCore<CustomIdentityUser>()
                .AddEntityFrameworkStores<CustomIdentityContext>();

            return serviceCollection.BuildServiceProvider().CreateScope();
        }

        public static async Task Seed()
        {

            using (var scope = GenerateServiceScope())
            {
                var svcs = scope.ServiceProvider;
                var db = svcs.GetService<CustomIdentityContext>();
                db.Database.EnsureCreated();

                Console.WriteLine("Database Created");


                var userManager = svcs.GetService<UserManager<CustomIdentityUser>>();

                var first = await userManager.FindByEmailAsync("bytzahra@gmail.com");
                if (first == null)
                {
                    first = new CustomIdentityUser()
                    {
                        UserName = "bytzahra@gmail.com",
                        Email = "bytzahra@gmail.com",
                        EmailConfirmed = true
                    };

                    if (await userManager.CreateAsync(first, "P@ssw0rd!") == IdentityResult.Success)
                    {
                        // Add claim
                        await userManager.AddClaimsAsync(first, new Claim[]
                        {
                           new Claim(JwtRegisteredClaimNames.GivenName, "Zahra"),
                           new Claim(JwtRegisteredClaimNames.FamilyName, "Bayat"),
                           new Claim("role", "user"),
                           new Claim("role", "admin")
                        });
                        Console.WriteLine($"User ({first.UserName}) created.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to create user");
                    }
                }

            
            };
            Console.WriteLine("Database seeded...");
        }

        public static async Task Drop()
        {
            using (var scope = GenerateServiceScope())
            {
                var svcs = scope.ServiceProvider;
                var db = svcs.GetService<CustomIdentityContext>();
                await db.Database.EnsureDeletedAsync();
            }
            Console.WriteLine("Database dropped...");
        }
    }
}
