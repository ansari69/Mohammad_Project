using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MiniShop.Application.Common.Helpers;
using MiniShop.Domain.Identity;
using MiniShop.Infrastructure.Persistence;
using MiniShop.Infrastructure.Security.Stores;
using Serilog;

namespace MiniShop.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            var config = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json").Build();

            //   public async static void Main(string[] args)

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();

            try
            {
                var host = CreateHostBuilder(args).Build();

                using (var serviceScope = host.Services.CreateScope())
                {
                    //Get Context
                    var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

                    //Get user manager and role manager
                    var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

                    await DataSeeding.SeedBaseDataAsync(context, userManager, roleManager);

                    //Base Seeding
                    if (!context.ProductTypes.Any())
                    {
                      //  await DataSeeding.SeedBaseDataAsync(context, userManager, roleManager);
                    }


                    //Developer Seeding
                    if (await userManager.FindByNameAsync("admin") == null)
                    {

                        var developerUser = new ApplicationUser
                        {
                            UserName = "admin",
                            Email = "test@gmail.com",
                            PhoneNumber = "+989304695182",
                            FirstName = "محمد",
                            LastName = "انصاریان",
                            Rank = "برنامه نویس",
                            CreateDate = DateTime.Now,
                            IsActive = true,
                            IsManagmentConfirm = true
                        };



                        //claims
                        var devClaims = new List<Claim>
                        {
                            new Claim(ApplicationClaimsStore.FullAccess.Name, "true"),
                            new Claim(ApplicationClaimsStore.Operator.Name, "true")
                        };

                        //creation
                        await userManager.CreateAsync(developerUser, "Admin5151");
                        await userManager.AddClaimsAsync(developerUser, devClaims);

                        // Roles
                        #region Roles

                        var managerRole = new ApplicationRole { Name = "مدیر سایت", IsActive = true };
                        await roleManager.CreateAsync(managerRole);
                        await roleManager.AddClaimAsync(managerRole,
                            new Claim(ApplicationClaimsStore.FullAccess.Name, "true"));

                        //await roleManager.AddClaimAsync(,
                        //    new Claim(ApplicationClaimsStore.Operator.Name, "true"));

                        var operatorRole = new ApplicationRole { Name = "اپراتور", IsActive = true };
                        await roleManager.CreateAsync(operatorRole);
                        await roleManager.AddClaimAsync(operatorRole,
                            new Claim(ApplicationClaimsStore.Operator.Name, "true"));


                        //simple
                        var simpleRole = new ApplicationRole { Name = "عادی", IsActive = true };
                        await roleManager.CreateAsync(simpleRole);
                        await roleManager.AddClaimAsync(simpleRole,
                            new Claim(ApplicationClaimsStore.Simple.Name, "true"));

                        #endregion

                    }


                }

                await host.RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "The application faild to satart correctly");

            }
            finally
            {
                Log.CloseAndFlush();
            }




        }







        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .UseSerilog()
               .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });

    }
}
