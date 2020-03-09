using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentPerfomance.IdentityServer.Models;

namespace StudentPerfomance.IdentityServer.Data
{
    public class IdentityServerContext : IdentityDbContext<SPUser, SPRole, int>
    {
        public IdentityServerContext(DbContextOptions<IdentityServerContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
