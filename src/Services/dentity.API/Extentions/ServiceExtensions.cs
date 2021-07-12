using Microsoft.Extensions.DependencyInjection;

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

    }
}
