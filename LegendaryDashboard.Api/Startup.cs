using LegendaryDashboard.Application;
using LegendaryDashboard.Infrastructure.DbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

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
            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: "MyPolicy",
                    builder =>
                    {
                        builder.WithOrigins(Configuration["FrontAddress"]).AllowAnyHeader().AllowAnyMethod();
                    });
            });
            
            // добавляем контекст DashboardContext в качестве сервиса в приложение
            services.AddDbContext<DashboardContext>();
            
            
            /*
            // получаем строку подключения из файла конфигурации
            string connection = Configuration.GetConnectionString("DefaultConnection");
            // добавляем контекст DashboardContext в качестве сервиса в приложение
            services.AddDbContextFactory<DashboardContext>(options =>
                options.UseSqlServer(connection).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));*/

            services
                .AddControllers();

            services
                .AddApplicationModule()
                .AddAuthenticationModule(Configuration)
                .AddSwaggerModule();
            

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
            
            app.UseAuthentication();
            
            app.UseRouting();
            
            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}