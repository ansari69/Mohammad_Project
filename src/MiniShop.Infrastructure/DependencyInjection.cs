using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MiniShop.Application.Common.Interfaces;
using MiniShop.Infrastructure.Persistence;
using MiniShop.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using MiniShop.Infrastructure.Security;

namespace MiniShop.Infrastructure
{
   public static class DependencyInjection
    {

        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            //DbContext
            services.AddDbContext<AppDbContext>(options =>
            {
                //options.UseInMemoryDatabase("KoshtargahDb");
                options.UseSqlServer(
                   configuration.GetConnectionString("AppConnectionString"),
                   m => m.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
            });
            services.AddTransient<IAppDbContext>(provider => provider.GetService<AppDbContext>());



            //Identity
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 0;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();



            //Add Authorization
           services.AddCustomAuthorization();




        }

    }
}
