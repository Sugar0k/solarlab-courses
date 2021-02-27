using System.Text;
using LegendaryDashboard.Application.Services.FeedbackService.Implementations;
using LegendaryDashboard.Application.Services.FeedbackService.Interfaces;
using LegendaryDashboard.Application.Services.Repositories;
using LegendaryDashboard.Application.Services.UserService.Implementations;
using LegendaryDashboard.Application.Services.UserService.Interfaces;
using LegendaryDashboard.Infrastructure.DbContext;
using LegendaryDashboard.Infrastructure.IRepositories;
using LegendaryDashboard.Infrastructure.MapperProfiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LegendaryDashboard.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // получаем строку подключения из файла конфигурации
            string connection = Configuration.GetConnectionString("DefaultConnection");
            // добавляем контекст DashboardContext в качестве сервиса в приложение
            services.AddDbContext<DashboardContext>(options =>
                options.UseSqlServer(connection));
            services
                .AddControllers();
            services
                .AddScoped<IUserService, UserService>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddAutoMapper(typeof(UserMapperProfile).Assembly);

            services
                .AddScoped<IFeedbackService, FeedbackService>()
                .AddScoped<IFeedbackRepository, FeedbackRepository>();

            services
                .AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "LegendaryDashboard", Version = "v1.1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LegendaryDashboard v1.1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}