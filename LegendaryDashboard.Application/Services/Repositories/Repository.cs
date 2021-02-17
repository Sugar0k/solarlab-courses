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
    public class Repository<TEntity, TId> : IRepository<TEntity, TId> 
        where TEntity : BaseEntity<TId>
    {
        protected readonly DashboardContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(DashboardContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }
        
        public async Task Save(TEntity entity, CancellationToken cancellationToken)
        {
            await DbSet.AddAsync(entity, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);
        }

        public async Task<TEntity> FindById(TId id, CancellationToken cancellationToken)
        {
            return await DbSet.FindAsync(id);
        }

        async Task<int> IRepository<TEntity, TId>.Count(CancellationToken cancellationToken)
        {
            return DbSet.Count();
        }

        public async Task Delete(TId id, CancellationToken cancellationToken)
        {
            var entity  = DbSet.FindAsync(id, cancellationToken);
            DbSet.Remove(await entity);
        }
    }
}