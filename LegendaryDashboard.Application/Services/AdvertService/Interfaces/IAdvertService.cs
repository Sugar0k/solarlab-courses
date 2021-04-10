using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Contracts.Contracts;
using LegendaryDashboard.Contracts.Contracts.Advert;
using LegendaryDashboard.Contracts.Contracts.Advert.Requests;
using LegendaryDashboard.Contracts.Contracts.AdvertImage;
using LegendaryDashboard.Domain.Common;
using Microsoft.AspNetCore.Http;

namespace LegendaryDashboard.Application.Services.AdvertService.Interfaces
{
    public interface IAdvertService
    {
        /// <summary>
        /// Создание объявления
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> Create(
            CreateAdvertRequest request, 
            CancellationToken cancellationToken);
        
        /// <summary>
        /// Удаление объявления
        /// </summary>
        /// <param name="advertId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task Delete(
            int advertId, 
            CancellationToken cancellationToken);
        
        /// <summary>
        /// Вывод общего количества объявлений
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> Count(
            CancellationToken cancellationToken);
        
        /// <summary>
        /// Вывод объявления по его Id
        /// </summary>
        /// <param name="advertId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<AdvertDto> FindById(
            int advertId, 
            CancellationToken cancellationToken);
        
        /// <summary>
        /// Постраничный вывод списка объявлений, в зависимости
        /// от поступающих параметров
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PagedResponse<AdvertDto>> GetPaged(
            PagedAdvertsRequest request,
            CancellationToken cancellationToken);
        
        /// <summary>
        /// Добавление объявления в избранное
        /// </summary>
        /// <param name="advertId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddFollow(int advertId, CancellationToken cancellationToken);

        //изображения
        
        /// <summary>
        /// Получение всех изображений объявления
        /// </summary>
        /// <param name="advertId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IEnumerable<AdvertImageDto>> GetAdvertImages(int advertId, CancellationToken cancellationToken);

        /// <summary>
        /// Добавление изображения
        /// </summary>
        /// <param name="advertId"></param>
        /// <param name="files"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task AddImages(int advertId, IFormFile files, CancellationToken cancellationToken);

        /// <summary>
        /// Удаление изображения по id
        /// </summary>
        /// <param name="advertId"></param>
        /// <param name="imageId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task DeleteImage(string imageId, CancellationToken cancellationToken);

        /// <summary>
        /// Удаление изображения по advertId
        /// </summary>
        /// <param name="advertId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task DeleteImagesByAdvertId(int advertId, CancellationToken cancellationToken);

        /// <summary>
        /// Получение изображения по id
        /// </summary>
        /// <param name="imageId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<AdvertImageDto> GetImage(string imageId, CancellationToken cancellationToken);
        
        /// <summary>
        /// Получение списка изображений по id объявления.
        /// </summary>
        /// <param name="advertId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IEnumerable<string>> GetImagesByAdvertId(int advertId, CancellationToken cancellationToken);
        
        /// <summary>
        /// Обновление объявления
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task Update(UpdateAdvertsRequest request, CancellationToken cancellationToken);
        
        /// <summary>
        /// Добавление просмотра на объявление
        /// </summary>
        /// /// <param name="advertId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddView(int advertId, CancellationToken cancellationToken);
    }
}