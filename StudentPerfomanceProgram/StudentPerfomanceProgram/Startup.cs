using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentPerfomance.IdentityServer.Data;
using StudentPerfomance.IdentityServer.Data.Helpers;
using StudentPerfomance.IdentityServer.Models;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace StudentPerfomanceProgram
{
    public class Startup
    {
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _env;


        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _env = webHostEnvironment;
            _config = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _config.GetConnectionString("DefaultConnection");
            var migrationsAssembly = typeof(IdentityServerContext).Assembly.GetName().Name;
            var filePath = Path.Combine(_env.ContentRootPath, "StudentPerfomanceCert.pfx");
            var certificate = new X509Certificate2(filePath, "password1");

            services.AddDbContext<IdentityServerContext>(o => o.UseSqlServer(connectionString));

            services.AddIdentity<SPUser, SPRole>(config =>
            {
                config.Password.RequiredLength = 8;
                config.Password.RequireUppercase = false;
                config.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<IdentityServerContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "IdentityServer.Cookie";
                config.LoginPath = "/Auth/Login";
                config.LogoutPath = "/Auth/Logout";
            });

            services.AddIdentityServer()
                .AddAspNetIdentity<SPUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                })
            .AddSigningCredential(certificate);
            //.AddDeveloperSigningCredential();
            //.AddInMemoryIdentityResources(Config.GetIdentityResources())
            //.AddInMemoryApiResources(Config.GetApis())
            //.AddInMemoryClients(Config.GetClients());


            services.AddAuthentication().AddGoogle(config =>
            {
                config.ClientId = _config.GetConnectionString("GoogleId");
                config.ClientSecret = _config.GetConnectionString("GoogleSecret");
            });

            services.AddCors();

            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            //services.AddScoped<StudentManagementRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseIdentityServer();

            //app.UseAuthentication();

            DbInitializer.InitializeDatabase(app);

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
