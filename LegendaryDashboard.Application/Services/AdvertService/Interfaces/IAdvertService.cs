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
        Task Create(
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
        /// <param name="ownerId"></param>
        /// <param name="followerId"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PagedResponse<AdvertDto>> GetPaged(
            int? ownerId,
            int? followerId,
            int offset, 
            int limit, 
            CancellationToken cancellationToken);
        
        // /// <summary>
        // /// Постраничный вывод списка объявлений владельца 
        // /// </summary>
        // /// <param name="offset"></param>
        // /// <param name="limit"></param>
        // /// <param name="cancellationToken"></param>
        // /// <returns></returns>
        // Task<PagedResponse<AdvertDto>> GetPagedByOwnerId(
        //     int offset, 
        //     int limit, 
        //     CancellationToken cancellationToken);
        //
        // /// <summary>
        // /// Постраничный вывод объявлений избранных объявлений
        // /// </summary>
        // /// <param name="offset"></param>
        // /// <param name="limit"></param>
        // /// <param name="cancellationToken"></param>
        // /// <returns></returns>
        // Task<PagedResponse<AdvertDto>> GetPagedByFollowerId(
        //     int offset, 
        //     int limit, 
        //     CancellationToken cancellationToken);

        /// <summary>
        /// Добавление объявления в избранное
        /// </summary>
        /// <param name="advertId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddFollow(int advertId, CancellationToken cancellationToken);

        //что-то еще 
        public Task<IEnumerable<AdvertImageDto>> GetAdvertImages(int advertId, CancellationToken cancellationToken);

        public Task AddImage(int advertId, IFormFile file, CancellationToken cancellationToken);

        public Task DeleteImage(int advertId, string imageId, CancellationToken cancellationToken);

        public Task<AdvertImageDto> GetImage(int advertId, string imageId, CancellationToken cancellationToken);
        

    }
}