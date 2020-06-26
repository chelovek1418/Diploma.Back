using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentPerfomance.Api.Helpers.Filters;
using StudentPerfomance.Bll.Interfaces;
using StudentPerfomance.Bll.Services;
using StudentPerfomance.Dal;
using StudentPerfomance.Dal.Interfaces;
using StudentPerfomance.Dal.Repository;

namespace StudentPerfomance.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<StudentPerfomanceDbContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IGroupService, GroupService>();

            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<ILessonService, LessonService>();

            services.AddScoped<IMarkRepository, MarkRepository>();
            services.AddScoped<IMarkService, MarkService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<ITeacherService, TeacherService>();

            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStudentService, StudentService>();

            services.AddScoped<IDetailRepository, DetailRepository>();
            services.AddScoped<IDetailService, DetailService>();

            services.AddScoped<IGroupSubjectRepository, GroupSubjectRepository>();

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", config =>
                {
                    config.Authority = "https://localhost:44361/";
                    
                    config.Audience = "StudentPerfomanceApi";
                });

            services.AddCors(opt =>
                opt.AddPolicy("AllowAll", p => p
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()));

            services.AddControllers(options => options.Filters.Add(new ExceptionFilterAttribute()))
                    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
