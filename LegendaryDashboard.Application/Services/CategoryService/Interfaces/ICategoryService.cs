using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Contracts.Contracts;
using LegendaryDashboard.Contracts.Contracts.Category;
using LegendaryDashboard.Contracts.Contracts.Category.Requests;

namespace LegendaryDashboard.Application.Services.CategoryService.Interfaces
{
    public interface ICategoryService
    {
        Task Save(CreateCategoryRequest request, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task Update(CreateCategoryRequest request, CancellationToken cancellationToken);
        Task<int> Count(CancellationToken cancellationToken);
        Task<CategoryDto> FindById(int id, CancellationToken cancellationToken);
        Task<PagedResponse<CategoryDto>> GetPaged(int offset, int limit, CancellationToken cancellationToken);
        Task<List<CategoryDto>> GetByTitles(string approximateName, CancellationToken cancellationToken);
        Task<List<CategoryDto>> GetByParentCategoryId(int id, CancellationToken cancellationToken);
    }
}