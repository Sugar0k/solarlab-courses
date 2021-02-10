using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Api.DbContext;
using LegendaryDashboard.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LegendaryDashboard.Application
{
    public class Repository<TEntity> : IRepository<TEntity, int> 
        where TEntity : BaseEntity<int>
    {
        protected readonly DashboardContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(DashboardContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return DbSet.AsQueryable();
        }

        public async Task<TEntity> Save(TEntity entity, CancellationToken ct = default)
        {
            await DbSet.AddAsync(entity, ct);

            return entity;
        }
    }
}