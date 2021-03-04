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
    public sealed class AdvertRepository : Repository<Advert, int>, IAdvertRepository
    {
        public AdvertRepository( DashboardContext context): base(context)
        {
        }

        public async Task<List<Advert>> GetPaged(int offset, int limit, CancellationToken cancellationToken)
        {
            return await DbSet
                .OrderBy(u => u.Id)
                .Skip(offset)
                .Take(limit)
                .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<List<Advert>> GetByCategoryId(int categoryId, int offset, int limit, CancellationToken cancellationToken)
        {
            return await DbSet
                .Where(c => c.CategoryId == categoryId)
                .OrderBy(u => u.Id)
                .Skip(offset)
                .Take(limit)
                .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<List<Advert>> GetByState(string state, int offset, int limit, CancellationToken cancellationToken)
        {
            return await DbSet
                .Where(c => c.State.Equals(state))
                .OrderBy(u => u.Id)
                .Skip(offset)
                .Take(limit)
                .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<List<Advert>> GetByCity(string city, int offset, int limit, CancellationToken cancellationToken)
        {
            return await DbSet
                .Where(c => c.City.Equals(city))
                .OrderBy(u => u.Id)
                .Skip(offset)
                .Take(limit)
                .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<List<Advert>> GetByTitle(string title, int offset, int limit, CancellationToken cancellationToken)
        {
            return await DbSet
                .Where(c => c.Title.Contains(title))
                .OrderBy(u => u.Id)
                .Skip(offset)
                .Take(limit)
                .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<int> NumberOfViews(int id, CancellationToken cancellationToken)
        {
            var advert = FindById(id, cancellationToken).Result;
            return advert.Views;
        }

        public async Task AddView(int id, CancellationToken cancellationToken)
        {
            var advert = FindById(id, cancellationToken).Result;
            if (advert == null) throw new Exception("Объявление не найдено!");
            advert.Views++;
            await Save(advert, cancellationToken);
        }

        public async Task<int> Count(Expression<Func<Advert, bool>> predicate, CancellationToken cancellationToken)
        {
            var compiled = predicate.Compile();
            return DbSet
                .Where(compiled)
                .Count();
        }

    }
}