using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Api.DbContext;
using LegendaryDashboard.Domain.Models;
using LegendaryDashboard.Infrastructure.Repository;
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

        public async Task<TEntity> FindById(int Id, CancellationToken cancellationToken)
        {
            var obj  = await DbSet.FindAsync(Id, cancellationToken);
            if (obj == null)
            {
                throw new Exception("Такого пользователя нет!");
            }
            return obj;
        }

        public async Task DeleteById(int Id, CancellationToken cancellationToken)
        {
            var obj  = await DbSet.FindAsync(Id, cancellationToken);
            if (obj == null)
            {
                throw new Exception("Такого объекта нет!");
            }
            DbSet.Remove(obj);
        }
    }
}