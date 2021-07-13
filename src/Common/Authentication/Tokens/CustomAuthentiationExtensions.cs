using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Authentication.Tokens
{
    public static class CustomAuthentiationExtensions
    {
        public static AuthenticationBuilder AddCustomAuthentication(this IServiceCollection services)
        {

            services.AddAuthorization(options =>
            {
                var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
                defaultAuthorizationPolicyBuilder = defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            });

            return services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddCustomBearerToken();
        }


        public static AuthenticationBuilder AddCustomBearerToken(this AuthenticationBuilder builder, string key = "TokenOptions")
        {
            var config = builder.Services.BuildServiceProvider().GetService<Microsoft.Extensions.Configuration.IConfiguration>();


            var options = new CustomTokenOptions();
            config.Bind(key, options);

            return builder.AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = options.Issuer,
                    ValidAudience = options.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SigningKey)),
                };

            });
        }

        public static AuthenticationBuilder AddCustomToken(this AuthenticationBuilder builder, Action<JwtBearerOptions> options)
        {
            return builder.AddJwtBearer(options);
        }
    }
}
