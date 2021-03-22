using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Internal;
using Castle.Core.Internal;
using LegendaryDashboard.Application.Services.AdvertService.Interfaces;
using LegendaryDashboard.Application.Services.FileService.Interfaces;
using LegendaryDashboard.Contracts.Contracts;
using LegendaryDashboard.Contracts.Contracts.Advert;
using LegendaryDashboard.Contracts.Contracts.Advert.Requests;
using LegendaryDashboard.Contracts.Contracts.AdvertImage;
using LegendaryDashboard.Contracts.Contracts.UserAdvert.Requests;
using LegendaryDashboard.Domain.Common;
using LegendaryDashboard.Domain.Models;
using LegendaryDashboard.Infrastructure.AdvertSpecification.Implementations;
using LegendaryDashboard.Infrastructure.AdvertSpecification.Implementations.Specifications;
using LegendaryDashboard.Infrastructure.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Category = LegendaryDashboard.Infrastructure.AdvertSpecification.Implementations.Specifications.Category;

namespace LegendaryDashboard.Application.Services.AdvertService.Implementations
{
    public class AdvertService : IAdvertService
    {
        private const string ImagesPath = "advertImages";
        
        private readonly IMapper _mapper;
        private readonly IAdvertRepository _advertRepository;
        private readonly IAdvertImageRepository _advertImageRepository;
        private readonly IUserAdvertRepository _userAdvertRepository;
        private readonly IHttpContextAccessor _accessor;
        private readonly IFileService _fileService;
        
        public AdvertService(IMapper mapper, 
            IAdvertRepository advertRepository, 
            IAdvertImageRepository advertImageRepository, 
            IUserAdvertRepository userAdvertRepository, 
            IHttpContextAccessor accessor, 
            IFileService fileService)
        {
            _mapper = mapper;
            _advertRepository = advertRepository;
            _advertImageRepository = advertImageRepository;
            _userAdvertRepository = userAdvertRepository;
            _accessor = accessor;
            _fileService = fileService;
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
                AdvertId = advert.Id,
                UserId = ClaimsPrincipalExtensions.GetUserId(_accessor),
                ConnectionType = AdvertUserConnectionTypes.OwnerConnection
            },cancellationToken);
        }

        public async Task Delete(int advertId, CancellationToken cancellationToken)
        {
            var advert = await _advertRepository.FindById(advertId, cancellationToken);
            
            if (advert == null) throw new Exception("Advert not found");

            if (!ClaimsPrincipalExtensions.IsAdminOrOwner(_accessor,
                await _userAdvertRepository.GetOwnerId(advertId, cancellationToken)))
                throw new Exception("Advert не пренадлежит текущему пользователю");
            
            await _advertRepository.Delete(advertId, cancellationToken);
            
            var path = Path.Combine(ImagesPath, advert.Id.ToString());
            advert.AdvertImages.ForAll(async image =>
            {
                await _fileService.Delete(image.Id, path, cancellationToken);
                await _advertImageRepository.Delete(image.Id, cancellationToken);
            });
            await _advertImageRepository.DeleteByAdvertId(advertId, cancellationToken);
            await _userAdvertRepository.DeleteByAdvertId(advertId, cancellationToken);
        }

        public async Task<IEnumerable<AdvertImageDto>> GetAdvertImages(int advertId, CancellationToken cancellationToken)
        {
            var images = new List<AdvertImageDto>();
            foreach (var advertImage in await _advertImageRepository.GetByAdvertId(advertId, cancellationToken))
            {
                var advertImageDto = _mapper.Map<AdvertImageDto>(advertImage);
                advertImageDto.data = await _fileService.Get(advertImageDto.id, 
                    Path.Combine(ImagesPath, advertId.ToString()), cancellationToken);
                images.Add(advertImageDto);
            }

            return images;
        }

        public async Task<int> Count(CancellationToken cancellationToken)
        {
            return await _advertRepository.Count(cancellationToken);
        }

        public async Task<AdvertDto> FindById(int id, CancellationToken cancellationToken)
        {
            var advert = await _advertRepository.FindById(id, cancellationToken);
            if (advert == null) throw new Exception("Объявление не найдено");
            var dto =  _mapper.Map<AdvertDto>(advert);
            dto.Images = await GetImagesByAdvertId(id, cancellationToken);
            //todo: добавить +1 просмотр
            return dto;
        }

        public async Task<PagedResponse<AdvertDto>> GetPaged(PagedAdvertsRequest request, CancellationToken cancellationToken)
        {
            var spec = (Specification<Advert>) new TrueSpecification<Advert>();
            if (request.FollowerId != null) spec &= Follower.New(request.FollowerId.Value);

            if (request.OwnerId != null) spec &= Owner.New(request.OwnerId.Value);

            if (!request.City.IsNullOrEmpty()) spec &= City.New(request.City);

            if (!request.State.IsNullOrEmpty()) spec &= State.New(request.State);

            if (request.MinPrice != 0 || request.MaxPrice != int.MaxValue) spec &= Price.New(request.MinPrice, request.MaxPrice);

            if (request.CategoryId != null) spec &= Category.New(request.CategoryId.Value);
            
            if (!request.Title.IsNullOrEmpty()) spec &= Title.New(request.Title);
            
            var adverts = await _advertRepository.GetPaged(
                    spec,
                    request.Offset, request.Limit, cancellationToken);
            var list = adverts.EntityList;
            var dtos = _mapper.Map<List<AdvertDto>>(list);
            foreach (var dto in dtos)
            {
                dto.Images = await GetImagesByAdvertId(dto.Id, cancellationToken);
            }

            return new PagedResponse<AdvertDto>
            {
                Count = dtos.Count,
                EntityList = dtos
            };
        }
        
        public async Task AddImage(int advertId, IFormFile file, CancellationToken cancellationToken)
        {
            var advert = await _advertRepository.FindById(advertId, cancellationToken);
            if (advert == null) throw new Exception("Advert not found");
            
            if (!ClaimsPrincipalExtensions.IsAdminOrOwner(_accessor,
                await _userAdvertRepository.GetOwnerId(advertId, cancellationToken)))
                throw new Exception("Advert не пренадлежит текущему пользователю");
            
            var path = Path.Combine(ImagesPath, advertId.ToString());
            await _advertImageRepository.Save(new AdvertImage
            {
                Id = await _fileService.Create(file, path, cancellationToken),
                FileName = file.FileName,
                FilePath = path,
                DateCreate = DateTime.UtcNow,
                AdvertId = advertId
            }, cancellationToken);
        }

        public async Task DeleteImage(string imageId, CancellationToken cancellationToken)
        {
            var image = await _advertImageRepository.FindById(imageId, cancellationToken);
            var advert = await _advertRepository.FindById(image.AdvertId, cancellationToken);
            
            if (!ClaimsPrincipalExtensions.IsAdminOrOwner(_accessor, advert.CategoryId))
                throw new Exception("Advert не пренадлежит текущему пользователю");
            
            await _fileService.Delete(imageId, image.FilePath, cancellationToken);
            await _advertImageRepository.Delete(imageId, cancellationToken);
        }

        public async Task DeleteImagesByAdvertId(int advertId, CancellationToken cancellationToken)
        {
            var images = await _advertImageRepository.GetByAdvertId(advertId, cancellationToken);
            var advert = await _advertRepository.FindById(advertId, cancellationToken);
            
            if (!ClaimsPrincipalExtensions.IsAdminOrOwner(_accessor, advert.CategoryId))
                throw new Exception("Advert не пренадлежит текущему пользователю");
            
            images.ForAll(async a => { await _fileService.Delete(a.Id, a.FilePath, cancellationToken); });
            await _advertImageRepository.DeleteByAdvertId(advertId, cancellationToken);
        }

        public async Task<AdvertImageDto> GetImage(string imageId, CancellationToken cancellationToken)
        {
            var advertImage = await _advertImageRepository.FindById(imageId, cancellationToken);
            var advertImageDto = _mapper.Map<AdvertImageDto>(advertImage);
            advertImageDto.data = await _fileService.Get(advertImage.Id, advertImage.FilePath, cancellationToken);
            return advertImageDto;
        }

        public async Task<IEnumerable<string>> GetImagesByAdvertId(int advertId, CancellationToken cancellationToken)
        {
            var advertImages = await _advertImageRepository.GetByAdvertId(advertId, 
                cancellationToken);
            return advertImages.Select(advertImage => advertImage.Id).ToList();
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