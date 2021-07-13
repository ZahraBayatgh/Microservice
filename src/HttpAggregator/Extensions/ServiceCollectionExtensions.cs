using Authentication;
using HttpAggregator.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service1.API.Extentions;
using System;

namespace HttpAggregator.Extensions
{
    static class ServiceCollectionExtensions
    {
        // Adds all Http client services
        public static IServiceCollection AddHttpClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //register delegating handlers
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            ////set 5 min as the lifetime for each HttpMessageHandler int the pool
            //services.AddHttpClient("extendedhandlerlifetime").SetHandlerLifetime(TimeSpan.FromMinutes(5)).AddDevspacesSupport();

            //add http client services
            services.AddHttpClient<IService2, Service2>(client =>
            {
                client.BaseAddress = new Uri(configuration["Service2Api:BaseAddress"]);
            })
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(ServiceExtentions.GetRetryPolicy())
                .AddPolicyHandler(ServiceExtentions.GetCircuitBreakerPolicy());

            return services;
        }



    }
}
