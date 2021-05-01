using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Authentication;
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
using LegendaryDashboard.Contracts.Contracts.AdvertImage.Requests;
using LegendaryDashboard.Domain.Common;
using LegendaryDashboard.Domain.Models;
using LegendaryDashboard.Infrastructure.AdvertSpecification.Implementations;
using LegendaryDashboard.Infrastructure.AdvertSpecification.Implementations.Specifications;
using LegendaryDashboard.Infrastructure.IRepositories;
using Microsoft.AspNetCore.Http;
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
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _accessor;
        private readonly IFileService _fileService;
        
        public AdvertService(IMapper mapper, 
            IAdvertRepository advertRepository, 
            IAdvertImageRepository advertImageRepository, 
            IUserAdvertRepository userAdvertRepository, 
            IUserRepository userRepository,
            IHttpContextAccessor accessor, 
            IFileService fileService)
        {
            _mapper = mapper;
            _advertRepository = advertRepository;
            _advertImageRepository = advertImageRepository;
            _userAdvertRepository = userAdvertRepository;
            _userRepository = userRepository;
            _accessor = accessor;
            _fileService = fileService;
        }

        
        public async Task<int> Create(CreateAdvertRequest request, CancellationToken cancellationToken)
        {
            Console.WriteLine(ClaimsPrincipalExtensions.GetUserId(_accessor));
            if (!(await _userRepository.Exist(ClaimsPrincipalExtensions.GetUserId(_accessor), cancellationToken)))
            {
                throw new AuthenticationException("Пользователь не найден!");
            }
            if (request == null) throw new ArgumentNullException("Запрос пуст!");
            var advert = _mapper.Map<Advert>(request);
            advert.CreationDate = DateTime.UtcNow;
            advert.Views = 0;
            advert.UsersAdverts.Add(new UserAdvert
            {
                AdvertId = advert.Id,
                UserId = ClaimsPrincipalExtensions.GetUserId(_accessor),
                ConnectionType = AdvertUserConnectionTypes.OwnerConnection
            });
            var path = Path.Combine(ImagesPath);
            if (request.Files != null) foreach (var file in request.Files)
            {
                advert.AdvertImages.Add(new AdvertImage
                {
                    Id = await _fileService.Create(file, path, cancellationToken),
                    FileName = file.FileName,
                    FilePath = path,
                    DateCreate = DateTime.UtcNow,
                    AdvertId = advert.Id
                });
            }
            
            await _advertRepository.Save(advert, cancellationToken);

            return advert.Id;
        }

        public async Task Delete(int advertId, CancellationToken cancellationToken)
        {
            var advert = await _advertRepository.FindById(advertId, cancellationToken);
            
            if (advert == null) throw new Exception("Advert not found");

            if (!ClaimsPrincipalExtensions.IsAdminOrOwner(_accessor,
                await _userAdvertRepository.GetOwnerId(advertId, cancellationToken)))
                throw new Exception("Advert не пренадлежит текущему пользователю");
            
            await _advertRepository.Delete(advertId, cancellationToken);
            
            advert.AdvertImages.ForAll(async image =>
                await _fileService.Delete(Path.Combine(image.FilePath, image.Id), cancellationToken));
         
        }

        public async Task<IEnumerable<AdvertImageDto>> GetAdvertImages(int advertId, CancellationToken cancellationToken)
        {
            var images = new List<AdvertImageDto>();
            foreach (var advertImage in await _advertImageRepository.GetByAdvertId(advertId, cancellationToken))
            {
                var advertImageDto = _mapper.Map<AdvertImageDto>(advertImage);
                advertImageDto.data = await _fileService.Get(
                    Path.Combine(advertImage.FilePath, advertImage.Id), cancellationToken);
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
            return dto;
        }

        public async Task<PagedResponse<AdvertDto>> GetPaged(PagedAdvertsRequest request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException("Запрос пуст!");
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
        
        public async Task AddImages(AdvertImageCreateRequest request, CancellationToken cancellationToken)
        {
            var advert = await _advertRepository.FindById(request.Id, cancellationToken);
            if (advert == null) throw new Exception("Advert not found");
            
            if (!ClaimsPrincipalExtensions.IsAdminOrOwner(_accessor,
                await _userAdvertRepository.GetOwnerId(request.Id, cancellationToken)))
                throw new Exception("Advert не пренадлежит текущему пользователю");
            
            
            var path = Path.Combine(ImagesPath);
            foreach (var file in request.Files)
            {
                await _advertImageRepository.Save(new AdvertImage
                {
                    Id = await _fileService.Create(file, path, cancellationToken),
                    FileName = file.FileName,
                    FilePath = path,
                    DateCreate = DateTime.UtcNow,
                    AdvertId = request.Id
                }, cancellationToken);
            }
        }

        public async Task DeleteImage(string imageId, CancellationToken cancellationToken)
        {
            var image = await _advertImageRepository.FindById(imageId, cancellationToken);
            if (image == null) throw new Exception("Изображение не найдено");
            var advert = await _advertRepository.FindById(image.AdvertId, cancellationToken);
            
            if (!ClaimsPrincipalExtensions.IsAdminOrOwner(_accessor, advert.CategoryId))
                throw new Exception("Advert не пренадлежит текущему пользователю");
            
            await _fileService.Delete(Path.Combine(image.FilePath, image.Id), cancellationToken);
            await _advertImageRepository.Delete(imageId, cancellationToken);
        }

        public async Task DeleteImagesByAdvertId(int advertId, CancellationToken cancellationToken)
        {
            var images = await _advertImageRepository.GetByAdvertId(advertId, cancellationToken);
            if (images == null) throw new Exception("Изображения не найдены");
            var advert = await _advertRepository.FindById(advertId, cancellationToken);
            
            if (!ClaimsPrincipalExtensions.IsAdminOrOwner(_accessor,
                await _userAdvertRepository.GetOwnerId(advertId, cancellationToken)))
                throw new Exception("Advert не пренадлежит текущему пользователю");
            await _advertImageRepository.DeleteByAdvertId(advertId, cancellationToken);
            images.ForAll(async a => { await _fileService.Delete(
                Path.Combine(a.FilePath, a.Id), cancellationToken); });
        }

        public async Task<AdvertImageDto> GetImage(string imageId, CancellationToken cancellationToken)
        {
            var advertImage = await _advertImageRepository.FindById(imageId, cancellationToken);
            var advertImageDto = _mapper.Map<AdvertImageDto>(advertImage);
            advertImageDto.data = await _fileService.Get(
                Path.Combine(advertImage.FilePath, advertImage.Id), cancellationToken);
            return advertImageDto;
        }

        public async Task<IEnumerable<string>> GetImagesByAdvertId(int advertId, CancellationToken cancellationToken)
        {
            var advertImages = await _advertImageRepository.GetByAdvertId(advertId, 
                cancellationToken);
            return advertImages.Select(advertImage => advertImage.Id).ToList();
        }

        public async Task Update(UpdateAdvertsRequest request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException("Запрос пуст!");
            if (!ClaimsPrincipalExtensions.IsAdminOrOwner(_accessor,
                await _userAdvertRepository.GetOwnerId(request.Id, cancellationToken)))
                throw new Exception("Advert не пренадлежит текущему пользователю");
            
            var advert = _mapper.Map<Advert>(request);
            advert.Views = (await _advertRepository.FindById(advert.Id, cancellationToken)).Views;
            advert.CreationDate = (await _advertRepository.FindById(advert.Id, cancellationToken)).CreationDate;
            await _advertRepository.Update(advert, cancellationToken);
        }

        public async Task AddView(int advertId, CancellationToken cancellationToken)
        {
            var advert = await _advertRepository.FindById(advertId, cancellationToken);
            if (advert == null) throw new Exception("Объявление не найдено!");
            advert.Views += 1;
            await _advertRepository.Update(advert, cancellationToken);
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