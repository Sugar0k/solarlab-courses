using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<List<Category>> GetByMatchingTitles(string approximateName,
            CancellationToken cancellationToken)
        {
            return DbSet.Where(c => c.Title.Contains(approximateName)).ToList();
        }

        public async Task<Category> GetByTitles(string name, CancellationToken cancellationToken)
        {
            return await DbSet.FirstOrDefaultAsync(
                c => c.Title == name, cancellationToken: cancellationToken);
        }

        public async Task<List<Category>> GetByParentCategoryId(int id, CancellationToken cancellationToken)
        {
            return DbSet.Where(c => c.ParentCategoryId == id).ToList();
        }
    }
}