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
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<List<CategoryDto>> GetPaged(int offset, int limit, CancellationToken cancellationToken)
        {
            var categories = await _repository.GetPaged(offset, limit, cancellationToken);
            return _mapper.Map<List<CategoryDto>>(categories);
        }

        public async Task<List<CategoryDto>> GetByTitles(string approximateName, CancellationToken cancellationToken)
        {
            var categories = await _repository.GetByTitles(approximateName, cancellationToken);
            return _mapper.Map<List<CategoryDto>>(categories);
        }

        public async Task<List<CategoryDto>> GetByParentCategoryId(int id, CancellationToken cancellationToken)
        {
            var categories = await _repository.GetByParentCategoryId(id, cancellationToken);
            return _mapper.Map<List<CategoryDto>>(categories);
        }
    }
}