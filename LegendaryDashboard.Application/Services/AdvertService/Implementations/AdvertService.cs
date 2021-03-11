using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LegendaryDashboard.Application.Services.AdvertService.Interfaces;
using LegendaryDashboard.Application.Services.Repositories;
using LegendaryDashboard.Contracts.Contracts;
using LegendaryDashboard.Contracts.Contracts.Advert;
using LegendaryDashboard.Contracts.Contracts.Advert.Requests;
using LegendaryDashboard.Domain.Common;
using LegendaryDashboard.Domain.Models;
using LegendaryDashboard.Infrastructure.IRepositories;
using Microsoft.AspNetCore.Http;

namespace LegendaryDashboard.Application.Services.AdvertService.Implementations
{
    public class AdvertService : IAdvertService
    {
        private readonly IMapper _mapper;
        private readonly IAdvertRepository _advertRepository;
        private readonly IAdvertImageRepository _advertImageRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IUserAdvertRepository _userAdvertRepository;
        private readonly IHttpContextAccessor _accessor;
        
        public AdvertService(IMapper mapper, IAdvertRepository advertRepository, IAdvertImageRepository advertImageRepository, IImageRepository imageRepository, IUserAdvertRepository userAdvertRepository, IHttpContextAccessor accessor)
        {
            _mapper = mapper;
            _advertRepository = advertRepository;
            _advertImageRepository = advertImageRepository;
            _imageRepository = imageRepository;
            _userAdvertRepository = userAdvertRepository;
            _accessor = accessor;
        }

        
        public async Task Create(CreateAdvertRequest request, CancellationToken cancellationToken)
        {
            if (request == null) throw new Exception("Запрос пуст!");

            var advert = _mapper.Map<Advert>(request);
            advert.CreationDate = DateTime.UtcNow;
            advert.Views = 0;
            await _advertRepository.Save(advert, cancellationToken);
            await _userAdvertRepository.Save(new UserAdvert
            {
                //TODO: ЭТО КРИНЖ!!!!!!!!
                AdvertId = _advertRepository.GetByTitle(request.Title,0,1, cancellationToken).Id,
                UserId = ClaimsPrincipalExtensions.GetUserId(_accessor),
                ConnectionType = AdvertUserConnectionTypes.OwnerConnection
            },cancellationToken);
            //TODO: Добавить создание записей о файлах и связях с ними
        }

        public async Task Delete(int advertId, CancellationToken cancellationToken)
        {
            var advert = await _advertRepository.FindById(advertId, cancellationToken);
            if (advert == null) throw new Exception("Advert not found");
            await _advertRepository.Delete(advertId, cancellationToken);
            await _imageRepository.DeleteMany(null //TODO: флаг в руки. нужно получить картинки по адвертАйди
                , cancellationToken);
            await _advertImageRepository.DeleteByAdvertId(advertId, cancellationToken);
            await _userAdvertRepository.DeleteByAdvertId(advertId, cancellationToken);
        }

        public async Task<int> Count(CancellationToken cancellationToken)
        {
            return await _advertRepository.Count(cancellationToken);
        }

        public async Task<AdvertDto> FindById(int id, CancellationToken cancellationToken)
        {
            var advert = await _advertRepository.FindById(id, cancellationToken);
            if (advert == null) throw new Exception("Объявление не найдено");
            return _mapper.Map<AdvertDto>(advert);
        }

        public async Task<PagedResponse<AdvertDto>> GetPaged(int? ownerId, int? followerId, int offset, int limit, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task AddFollow(int advertId, CancellationToken cancellationToken)
        {
            await _userAdvertRepository.Save(new UserAdvert
            {
                AdvertId = advertId,
                UserId = ClaimsPrincipalExtensions.GetUserId(_accessor),
                ConnectionType = AdvertUserConnectionTypes.FollowConnection
            },cancellationToken);
        }
    }
}