﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Contracts.Contracts.Category;
using LegendaryDashboard.Contracts.Contracts.Category.Requests;
using LegendaryDashboard.Domain.Models;

namespace LegendaryDashboard.Application.Services.CategoryService.Interfaces
{
    public interface ICategoryService
    {
        Task Save(CreateCategoryRequest request, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task<int> Count(CancellationToken cancellationToken);
        Task<CategoryDto> FindById(int id, CancellationToken cancellationToken);
        Task<CategoryListDto> GetAll(CancellationToken cancellationToken);
        Task<List<CategoryDto>> GetByTitles(string approximateName, CancellationToken cancellationToken);
        Task<List<CategoryDto>> GetByParentCategoryId(int id, CancellationToken cancellationToken);
    }
}