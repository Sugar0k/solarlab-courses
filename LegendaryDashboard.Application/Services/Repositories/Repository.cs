using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LegendaryDashboard.Contracts.Contracts;
using LegendaryDashboard.Domain.Exceptions;
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
            return await DbSet.FindAsync(new object[]{id}, cancellationToken);
        }

        public async Task<bool> Exist(TId id, CancellationToken cancellationToken)
        {
            return await DbSet.AnyAsync(x => Equals(x.Id, id), cancellationToken);
        }

        async Task<int> IRepository<TEntity, TId>.Count(CancellationToken cancellationToken)
        {
            return await DbSet.CountAsync(cancellationToken);
        }
        
        public async Task Delete(TId id, CancellationToken cancellationToken)
        {
            var entity  = await DbSet.FindAsync(new object[]{id}, cancellationToken);
            if (entity == null) throw new EntityNotFoundException("Удаляемый элемент не найден");
            DbSet.Remove(entity);
            await Context.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(TEntity entity, CancellationToken cancellationToken)
        {
            if (await DbSet.FindAsync(new object[]{entity.Id}, cancellationToken) == null) 
                throw new Exception($"Для {entity.GetType()} обновляемый элемент не найден");
            Context.Entry(entity).State = EntityState.Modified;
            DbSet.Update(entity);
            await Context.SaveChangesAsync(cancellationToken);
        }

        public async Task<PagedResponse<TEntity>> GetPaged(int offset, int limit, CancellationToken cancellationToken)
        {
            var list = await DbSet
                .OrderBy(u => u.Id)
                .Skip(offset)
                .Take(limit)
                .ToListAsync(cancellationToken: cancellationToken);
            var count = await DbSet.CountAsync(cancellationToken);
            return new PagedResponse<TEntity>
            {
                Count = count,
                EntityList = list
            };
        }

        public async Task<PagedResponse<TEntity>> GetPaged(
            Expression<Func<TEntity, bool>> predicate,
            int offset,
            int limit,
            CancellationToken cancellationToken)
        {
            var list = DbSet
                .Where(predicate)
                .OrderBy(entity => entity.Id);

            var count = await list.CountAsync(cancellationToken);
            return new PagedResponse<TEntity>
            {
                Count = count,
                EntityList = await list
                    .Skip(offset)
                    .Take(limit)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}