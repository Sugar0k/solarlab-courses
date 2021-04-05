using LegendaryDashboard.Application.Services.AdvertService.Implementations;
using LegendaryDashboard.Application.Services.AdvertService.Interfaces;
using LegendaryDashboard.Application.Services.CategoryService.Implementations;
using LegendaryDashboard.Application.Services.CategoryService.Interfaces;
using LegendaryDashboard.Application.Services.FeedbackService.Implementations;
using LegendaryDashboard.Application.Services.FeedbackService.Interfaces;
using LegendaryDashboard.Application.Services.FileService.Implementation;
using LegendaryDashboard.Application.Services.FileService.Interfaces;
using LegendaryDashboard.Application.Services.Repositories;
using LegendaryDashboard.Application.Services.UserService.Implementations;
using LegendaryDashboard.Application.Services.UserService.Interfaces;
using LegendaryDashboard.Infrastructure.IRepositories;
using LegendaryDashboard.Infrastructure.MapperProfiles;
using Microsoft.Extensions.DependencyInjection;

namespace LegendaryDashboard.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            //добавление сервисов и репозиториев Категорий
            services
                .AddScoped<ICategoryService, CategoryService>()
                .AddScoped<ICategoryRepository, CategoryRepository>()
                
                //подключение автомаппера
                .AddAutoMapper(typeof(CategoryMapperProfile).Assembly);
            
            //добавление сервисов и репозиториев Пользователя
            services
                .AddScoped<IUserService, UserService>()
                .AddScoped<IUserRepository, UserRepository>();

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
            //добавление сервисов работы с файлами
            services
                .AddScoped<IFileService, FileService>();
            //добавление репозитория картинок для объявления
            services
                .AddScoped<IAdvertImageRepository, AdvertImageRepository>();
            //добавление сервиса Объявления
            services
                .AddScoped<IAdvertService, AdvertService>();

            return services;
        }
        
    }
}