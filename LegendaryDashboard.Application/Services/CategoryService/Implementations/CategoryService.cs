using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
            if (request == null) throw new ArgumentNullException("Запрос пуст!");
            if (request.ParentCategoryId != null &&
                await _repository.FindById((int) request.ParentCategoryId, cancellationToken) == null
                )
                throw new Exception(
                    $"Не найдена категория с id = {request.ParentCategoryId} для объявления ее как родителя");

            Category category = _mapper.Map<Category>(request);
            await _repository.Save(category, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _repository.Delete(id, cancellationToken);  
        }

        public async Task Update(CategoryDto request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException("Запрос пуст!");
            if (request.ParentCategoryId != null && 
                (request.Id == request.ParentCategoryId ||
                 await _repository.FindById((int) request.ParentCategoryId, cancellationToken) == null
                 )
                ) 
                throw new Exception(
                    "Невозможно поменять id родителя на так как искомый id не найден или ссылается на себя"
                    );

            Category category = _mapper.Map<Category>(request);
            await _repository.Update(category, cancellationToken);
        }
        
        public async Task<int> Count(CancellationToken cancellationToken)
        {
            return await _repository.Count(cancellationToken);
        }

        public async Task<CategoryDto> FindById(int id, CancellationToken cancellationToken)
        {
            Category category = await _repository.FindById(id, cancellationToken);
            if (category == null) throw new Exception("Категория не найдена");
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<PagedResponse<CategoryDto>> GetPaged(int offset, int limit, CancellationToken cancellationToken)
        {
            var categories = await _repository.GetPaged(offset, limit, cancellationToken);
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.EntityList);
            return new PagedResponse<CategoryDto>
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

        public async Task<List<CategoryDto>> GetParentsCategories(int limit, int offset, CancellationToken cancellationToken)
        {
            var categories = await _repository.GetParentsCategories(limit, offset, cancellationToken);
            if (categories == null) throw new Exception("Категории не найдены");
            return _mapper.Map<List<CategoryDto>>(categories);
        }

        public async Task<IEnumerable<CategoryDto>> GetFullParents(int id, CancellationToken cancellationToken)
        {
            var list = new List<CategoryDto>();
            
            var category = await _repository.FindById(id, cancellationToken);
            if (category == null) throw new Exception("Категоря не найдена");

            list.Add(_mapper.Map<CategoryDto>(category));
            
            while (category.ParentCategoryId != null)
            {
                category = await _repository.FindById(category.ParentCategoryId.Value, cancellationToken);
                list.Add(_mapper.Map<CategoryDto>(category));
            }

            list.Reverse();
            return list;
        }
    }
}