using System;
using System.Text;
using LegendaryDashboard.Infrastructure.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace LegendaryDashboard.Application
{
    public static class AuthenticationModule
    {
        public static IServiceCollection AddAuthenticationModule(this IServiceCollection services, IConfiguration configuration)
        {
            //Аутентификация
            services.AddOptions<JwtOptions>().Configure<IConfiguration>((o, c) => {
                c.GetSection("Token").Bind(o);
            });
            services
                .AddAuthentication(cfg =>
                {
                    cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(jwtBearerOptions =>
                {
                    var c = new JwtOptions();
                    configuration.GetSection("Token").Bind(c);
                    jwtBearerOptions.RequireHttpsMetadata = false;
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.Zero,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(c.Key)),
                        ValidateAudience = false,
                        ValidateIssuer = false
                    };
                });
            services.AddHttpContextAccessor();

            return services;
        }
        
    }
}