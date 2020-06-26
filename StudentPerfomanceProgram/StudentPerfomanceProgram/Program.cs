using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StudentPerfomance.IdentityServer.Data.Helpers;
using StudentPerfomance.IdentityServer.Models;
using System;
using System.Threading.Tasks;

namespace StudentPerfomanceProgram
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                services.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
                try
                {
                    await DbInitializer.InitializeIdentityServer(services.GetRequiredService<ConfigurationDbContext>());
                    await DbInitializer.InitializeRolesAsync(services.GetRequiredService<UserManager<SPUser>>(), services.GetRequiredService<RoleManager<SPRole>>());
                }
                catch (Exception ex)
                {
                    services.GetRequiredService<ILogger<Program>>().LogError(ex, "An error occurred while seeding the database.");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
