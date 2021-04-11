using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Domain.Models;
using LegendaryDashboard.Infrastructure.DbContext;
using LegendaryDashboard.Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace LegendaryDashboard.Application.Services.Repositories
{
    public sealed class CategoryRepository : Repository<Category, int>, ICategoryRepository
    {
        public CategoryRepository(DashboardContext context) : base(context)
        {
        }
        
        public async Task<List<Category>> GetByTitles(string approximateName,
            CancellationToken cancellationToken)
        {
            return await DbSet.Where(c => c.Title.Contains(approximateName))
                .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<List<Category>> GetAll(CancellationToken cancellationToken)
        {
            return await DbSet
                .OrderBy(u => u.Id)
                .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<List<Category>> GetByParentCategoryId(int id, CancellationToken cancellationToken)
        {
            return await DbSet.Where(c => c.ParentCategoryId == id)
                .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<List<Category>> GetParentsCategories(int limit, int offset, CancellationToken cancellationToken)
        {
            return await DbSet
                .Where(c => c.ParentCategoryId == null)
                .Skip(offset)
                .Take(limit)
                .OrderBy(u => u.Id)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}