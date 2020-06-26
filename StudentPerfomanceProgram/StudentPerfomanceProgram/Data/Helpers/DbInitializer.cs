using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudentPerfomance.Api;
using StudentPerfomance.IdentityServer.Data.Constants;
using StudentPerfomance.IdentityServer.Models;
using System.Threading.Tasks;

namespace StudentPerfomance.IdentityServer.Data.Helpers
{
    internal static class DbInitializer
    {
        private const string AdminEmail = "daniilchelyshev@gmail.com";
        private const string Password = "password123";

        internal static async Task InitializeIdentityServer(ConfigurationDbContext context)
        {
            await context.Database.MigrateAsync();

            if (!await context.Clients.AnyAsync())
            {
                foreach (var client in Config.GetClients())
                {
                    await context.Clients.AddAsync(client.ToEntity());
                }
                await context.SaveChangesAsync();
            }

            if (!await context.IdentityResources.AnyAsync())
            {
                foreach (var resource in Config.GetIdentityResources())
                {
                    await context.IdentityResources.AddAsync(resource.ToEntity());
                }
                await context.SaveChangesAsync();
            }

            if (!await context.ApiResources.AnyAsync())
            {
                foreach (var resource in Config.GetApis())
                {
                    await context.ApiResources.AddAsync(resource.ToEntity());
                }
                await context.SaveChangesAsync();
            }
        }

        internal static async Task InitializeRolesAsync(UserManager<SPUser> userManager, RoleManager<SPRole> roleManager)
        {
            if (await roleManager.FindByNameAsync(IdentityData.Admin) == null)
            {
                await roleManager.CreateAsync(new SPRole { Name = IdentityData.Admin });
            }
            if (await roleManager.FindByNameAsync(IdentityData.Teacher) == null)
            {
                await roleManager.CreateAsync(new SPRole { Name = IdentityData.Teacher });
            }
            if (await roleManager.FindByNameAsync(IdentityData.Student) == null)
            {
                await roleManager.CreateAsync(new SPRole { Name = IdentityData.Student });
            }
            if (await userManager.FindByNameAsync(AdminEmail) == null)
            {
                var admin = new SPUser { Email = AdminEmail, UserName = AdminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, IdentityData.Admin);
                }
            }
        }
    }
}
