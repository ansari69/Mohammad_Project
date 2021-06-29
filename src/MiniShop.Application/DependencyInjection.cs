using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MiniShop.Application.Common.Behaviours;
using MiniShop.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MiniShop.Application
{
    public static class DependencyInjection
    {

        public static void AddApplication(this IServiceCollection services,
                    IConfiguration configuration)
        {


            //Fluent Validation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            //Add AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //Behaviours
           // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            //MediatR
            services.AddMediatR(Assembly.GetExecutingAssembly());

            // configure strongly typed settings objects
            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.JwtSecret);

            //Adding JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        }

    }
}
