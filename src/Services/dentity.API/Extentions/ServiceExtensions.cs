using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;

namespace Identity.API.Extentions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCustomCorsPolicy(this IServiceCollection coll)
        {
            return coll.AddCors(opt =>
            {
                opt.AddDefaultPolicy(cfg =>
                {
                    // TODO configure better policy and move it to separate project
                    cfg.AllowAnyHeader();
                    cfg.AllowAnyMethod();
                    cfg.AllowAnyOrigin();
                });
            });
        }

        public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            var hcBuilder = services.AddHealthChecks();

            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());

            hcBuilder
                .AddSqlServer(
                    configuration.GetConnectionString("AuthDb"),
                    name: "IdentityDb-check",
                    tags: new string[] { "identitydb" });

            return services;
        }
    }
}
