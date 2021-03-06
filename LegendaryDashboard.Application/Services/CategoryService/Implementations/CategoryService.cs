using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LegendaryDashboard.Application.Services.CategoryService.Interfaces;
using LegendaryDashboard.Contracts.Contracts.Category;
using LegendaryDashboard.Contracts.Contracts.Category.Requests;
using LegendaryDashboard.Contracts.Contracts.Feedback;
using LegendaryDashboard.Contracts.Contracts.Feedback.Requests;
using LegendaryDashboard.Domain.Exceptions;
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

        public async Task<CategoryListDto> GetAll(CancellationToken cancellationToken)
        {
            var categories = await _repository.GetAll(cancellationToken);
            if (categories == null) throw new Exception("Категории не найдены");
            var dtoCategories = _mapper.Map<List<CategoryDto>>(categories);
            var count = await _repository.Count(cancellationToken);
            return new CategoryListDto(count, dtoCategories);
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