using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Domain.Models;

namespace LegendaryDashboard.Infrastructure.IRepositories
{
    public interface ICategoryRepository : IRepository<Category, int>
    {
        Task<List<Category>> GetByTitles(string name, CancellationToken cancellationToken);
        
        Task<List<Category>> GetByParentCategoryId(int id, CancellationToken cancellationToken);
    }
}