using Authentication.Identity;
using Authentication.Tokens;
using dentity.API.Data;
using Identity.API.Extentions;
using Identity.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Reflection;

namespace dentity.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<CustomIdentityContext>(cfg =>
            {
                cfg.UseSqlServer(Configuration["ConnectionStrings:AuthDb"]);
            });

            services.AddIdentityCore<CustomIdentityUser>()
                .AddEntityFrameworkStores<CustomIdentityContext>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddCustomCorsPolicy();

            services.AddScoped<CustomTokenFactory>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddControllers()
              .AddNewtonsoftJson(cfg =>
              {
                  cfg.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
              });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Service1.API", Version = "v1" });
            });

            services.AddApiVersioning(cfg =>
            {
                cfg.ReportApiVersions = true;
                cfg.DefaultApiVersion = new ApiVersion(1, 0);
                cfg.AssumeDefaultVersionWhenUnspecified = true;
                cfg.ApiVersionReader = new HeaderApiVersionReader("X-Version");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
