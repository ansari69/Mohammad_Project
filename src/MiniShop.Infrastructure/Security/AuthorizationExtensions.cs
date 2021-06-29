using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using MiniShop.Infrastructure.Security.General;
using MiniShop.Infrastructure.Security.Stores;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Infrastructure.Security
{
   public static class AuthorizationExtensions
    {
        public static void AddCustomAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                    JwtBearerDefaults.AuthenticationScheme, "Bearer");
                defaultAuthorizationPolicyBuilder =
                    defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();


                // Base 
                options.AddPolicy(ApplicationPolicyStore.Base.AdminPanel, policy =>
                    policy.AddRequirements(new AdminRequirement())
                );


                // Shared
                options.AddPolicy(ApplicationPolicyStore.Shared.Operator, policy =>
                    policy.AddRequirements(new OperatorRequirement())
                );

                // Simple
                options.AddPolicy(ApplicationPolicyStore.SimpleUser.Simple, policy =>
                    policy.AddRequirements(new SimpleRequirement())
                );

            }
            );

            services.InjectHandlers();
        }


        public static void InjectHandlers(this IServiceCollection services)
        {
            // Base
            services.AddTransient<IAuthorizationHandler, AdminRequirementHandler>();

            // Shared
            services.AddTransient<IAuthorizationHandler, OperatorRequirementHandler>();

            // Simple
            services.AddTransient<IAuthorizationHandler, SimpleRequirementHandler>();


        }
    }
}
