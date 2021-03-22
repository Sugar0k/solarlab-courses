using System;
using System.Collections.Generic;
using System.Text;
using LegendaryDashboard.Application.Services.CategoryService.Implementations;
using LegendaryDashboard.Application.Services.CategoryService.Interfaces;
using LegendaryDashboard.Application.Services.FeedbackService.Implementations;
using LegendaryDashboard.Application.Services.FeedbackService.Interfaces;
using LegendaryDashboard.Application.Services.Repositories;
using LegendaryDashboard.Application.Services.UserService.Implementations;
using LegendaryDashboard.Application.Services.UserService.Interfaces;
using LegendaryDashboard.Infrastructure.DbContext;
using LegendaryDashboard.Infrastructure.IRepositories;
using LegendaryDashboard.Infrastructure.MapperProfiles;
using LegendaryDashboard.Infrastructure.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using IdentityUser = Microsoft.AspNetCore.Identity.IdentityUser;

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
                options.UseSqlServer(connection).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services
                .AddControllers();
            //добавление сервисов и репозиториев Категорий
            services
                .AddScoped<ICategoryService, CategoryService>()
                .AddScoped<ICategoryRepository, CategoryRepository>()
                
                //подключение автомаппера
                .AddAutoMapper(typeof(CategoryMapperProfile).Assembly);
            
            //добавление сервисов и репозиториев Пользователя
            services
                .AddScoped<IUserService, UserService>()
                .AddScoped<IUserRepository, UserRepository>()
                
                //подключение автомаппера
                .AddAutoMapper(typeof(UserMapperProfile).Assembly);

            //добавление репозитория Объявления
            services
                .AddScoped<IAdvertRepository, AdvertRepository>();
            
            //добавление репозитория связи Пользователя и Объявления
            services
                .AddScoped<IUserAdvertRepository, UserAdvertRepository>();
            
            //добавление сервисов и репозиториев Отзыва
            services
                .AddScoped<IFeedbackService, FeedbackService>()
                .AddScoped<IFeedbackRepository, FeedbackRepository>();
            
            //Аутентификация
            services.AddOptions<JwtOptions>().Configure<IConfiguration>((o, c) => {
                c.GetSection("Token").Bind(o);
            });
            services
                .AddAuthentication(cfg =>
                {
                    cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(jwtBearerOptions =>
                {
                    var c = new JwtOptions();
                    Configuration.GetSection("Token").Bind(c);
                    jwtBearerOptions.RequireHttpsMetadata = false;
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.Zero,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(c.Key)),
                        ValidateAudience = false,
                        ValidateIssuer = false
                    };
                });
            services.AddHttpContextAccessor();
            //Swagger
            services
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo {Title = "LegendaryDashboard", Version = "v1.1"});
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                        Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n
                        Example: 'Bearer 12345abcdef'",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = JwtBearerDefaults.AuthenticationScheme
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,
                            },
                            new List<string>()
                        }
                    });
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
            
            app.UseAuthentication();
            
            app.UseRouting();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}