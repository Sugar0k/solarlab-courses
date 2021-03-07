using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LegendaryDashboard.Application.Services.CategoryService.Interfaces;
using LegendaryDashboard.Contracts.Contracts;
using LegendaryDashboard.Contracts.Contracts.Category;
using LegendaryDashboard.Contracts.Contracts.Category.Requests;
using LegendaryDashboard.Contracts.Contracts.User;
using LegendaryDashboard.Domain.Models;
using LegendaryDashboard.Infrastructure.IRepositories;

namespace LegendaryDashboard.Application.Services.CategoryService.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Save(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request);
            await _repository.Save(category, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _repository.Delete(id, cancellationToken);  
        }

        public async Task<int> Count(CancellationToken cancellationToken)
        {
            return await _repository.Count(cancellationToken);
        }

        public async Task<CategoryDto> FindById(int id, CancellationToken cancellationToken)
        {
            var category = await _repository.FindById(id, cancellationToken);
            if (category == null) throw new Exception("Категория не найдена");
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<PagedResponce<CategoryDto>> GetPaged(int offset, int limit, CancellationToken cancellationToken)
        {
            var categories = await _repository.GetPaged(offset, limit, cancellationToken);
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);
            return new PagedResponce<CategoryDto>
            {
                Count = categories.Count,
                EntityList = categoriesDto
            };
        }

        public async Task<List<CategoryDto>> GetByTitles(string approximateName, CancellationToken cancellationToken)
        {
            var categories = await _repository.GetByTitles(approximateName, cancellationToken);
            if (categories == null) throw new Exception("Категории не найдены");
            return _mapper.Map<List<CategoryDto>>(categories);
        }

        public async Task<List<CategoryDto>> GetByParentCategoryId(int id, CancellationToken cancellationToken)
        {
            var categories = await _repository.GetByParentCategoryId(id, cancellationToken);
            if (categories == null) throw new Exception("Категории не найдены");
            return _mapper.Map<List<CategoryDto>>(categories);
        }
    }
}