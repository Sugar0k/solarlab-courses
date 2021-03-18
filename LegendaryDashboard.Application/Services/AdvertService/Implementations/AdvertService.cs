using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Internal;
using LegendaryDashboard.Application.Services.AdvertService.Interfaces;
using LegendaryDashboard.Application.Services.FileService.Interfaces;
using LegendaryDashboard.Application.Services.Repositories;
using LegendaryDashboard.Contracts.Contracts;
using LegendaryDashboard.Contracts.Contracts.Advert;
using LegendaryDashboard.Contracts.Contracts.Advert.Requests;
using LegendaryDashboard.Contracts.Contracts.AdvertImage;
using LegendaryDashboard.Domain.Common;
using LegendaryDashboard.Domain.Models;
using LegendaryDashboard.Infrastructure.IRepositories;
using Microsoft.AspNetCore.Http;

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

            var path = Path.Combine(ImagesPath, advert.Id.ToString());
            request.Images.ForAll(async iFormFile =>
            {
                await _fileService.Create(iFormFile, path, cancellationToken);
                await _advertImageRepository.Save(new AdvertImage
                {
                    FileName = iFormFile.Name,
                    FilePath = path,
                    DateCreate = DateTime.UtcNow,
                    AdvertId = advert.Id
                }, cancellationToken); 
            });
            
        }

        public async Task Delete(int advertId, CancellationToken cancellationToken)
        {
            var advert = await _advertRepository.FindById(advertId, cancellationToken);

            if (!ClaimsPrincipalExtensions.IsAdminOrOwner(_accessor, advert.CategoryId))
                throw new Exception("Advert не пренадлежит текущему пользователю");
            
            if (advert == null) throw new Exception("Advert not found");
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
            return _mapper.Map<AdvertDto>(advert);
        }

        public async Task<PagedResponse<AdvertDto>> GetPaged(PagedAdvertsRequest request, CancellationToken cancellationToken)
        {
            //TODO: Починить под ownerId & followerId
            return _mapper.Map<PagedResponse<AdvertDto>>(await _advertRepository.GetPaged(request.Offset, request.Limit, cancellationToken));
        }
        
        public async Task AddImage(int advertId, IFormFile file, CancellationToken cancellationToken)
        {
            var advert = await _advertRepository.FindById(advertId, cancellationToken);

            if (!ClaimsPrincipalExtensions.IsAdminOrOwner(_accessor, advert.CategoryId))
                throw new Exception("Advert не пренадлежит текущему пользователю");
            
            var path = Path.Combine(ImagesPath, advertId.ToString());
            await _fileService.Create(file, path, cancellationToken);
            await _advertImageRepository.Save(new AdvertImage
            {
                FileName = file.Name,
                FilePath = path,
                DateCreate = DateTime.UtcNow,
                AdvertId = advertId
            }, cancellationToken);
        }

        public async Task DeleteImage(int advertId, string imageId, CancellationToken cancellationToken)
        {
            var advert = await _advertRepository.FindById(advertId, cancellationToken);

            if (!ClaimsPrincipalExtensions.IsAdminOrOwner(_accessor, advert.CategoryId))
                throw new Exception("Advert не пренадлежит текущему пользователю");
            
            var path = Path.Combine(ImagesPath, advertId.ToString());
            await _fileService.Delete(imageId, path, cancellationToken);
            await _advertImageRepository.Delete(imageId, cancellationToken);
        }
        
        public async Task<AdvertImageDto> GetImage(int advertId, string imageId, CancellationToken cancellationToken)
        {
            var path = Path.Combine(ImagesPath, imageId);
            var advertImage = await _advertImageRepository.FindById(imageId, cancellationToken);
            var advertImageDto = _mapper.Map<AdvertImageDto>(advertImage);
            advertImageDto.data = await _fileService.Get(advertImage.Id, path, cancellationToken);
            return advertImageDto;
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