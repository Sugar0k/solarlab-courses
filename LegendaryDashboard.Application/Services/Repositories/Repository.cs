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
        }

        public async Task<TEntity> FindById(TId id, CancellationToken cancellationToken)
        {
            // Слы, переделай :(
            return await DbSet.FindAsync(id);
        }

        /* Перенести в репозитории модели
         */

        async Task<int> IRepository<TEntity, TId>.Count(CancellationToken cancellationToken)
        {
            return DbSet.Count();
        }

        /* Перенести в репозитории модели
         public async Task<int> Count(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            var compiled = predicate.Compile();
            return DbSet.Select(pair => pair.Value).Where(compiled).Count();
        }*/
        
        public async Task Delete(TId id, CancellationToken cancellationToken)
        {
            var entity  = DbSet.FindAsync(id, cancellationToken);
            DbSet.Remove(await entity);
        }
    }
}