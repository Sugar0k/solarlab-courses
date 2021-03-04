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
    public sealed class UserAdvertRepository : Repository<UserAdvert, int>, IUserAdvertRepository
    {
        public UserAdvertRepository( DashboardContext context): base(context)
        {
        }

        public async Task<List<UserAdvert>> GetPaged(int offset, int limit, CancellationToken cancellationToken)
        {
            return await DbSet
                .OrderBy(u => u.Id)
                .Skip(offset)
                .Take(limit)
                .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<List<UserAdvert>> GetConnectionsByUserId(int userId, int offset, int limit, CancellationToken cancellationToken)
        {
            return await DbSet
                .Where(c => c.UserId == userId)
                .OrderBy(u => u.Id)
                .Skip(offset)
                .Take(limit)
                .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<List<UserAdvert>> GetConnectionsByAdvertId(int advertId, int offset, int limit, CancellationToken cancellationToken)
        {
            return await DbSet
                .Where(c => c.AdvertId == advertId)
                .OrderBy(u => u.Id)
                .Skip(offset)
                .Take(limit)
                .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<List<UserAdvert>> GetConnectionsByAdvertIdAndType(int advertId, string type, int offset, int limit,
            CancellationToken cancellationToken)
        {
            return await DbSet
                .Where(c => c.AdvertId == advertId)
                .Where(c => c.ConnectionType.Equals(type))
                .OrderBy(u => u.Id)
                .Skip(offset)
                .Take(limit)
                .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<List<UserAdvert>> GetConnectionsByUserIdAndType(int userId, string type, int offset, int limit, CancellationToken cancellationToken)
        {
            return await DbSet
                .Where(c => c.UserId == userId)
                .Where(c => c.ConnectionType.Equals(type))
                .OrderBy(u => u.Id)
                .Skip(offset)
                .Take(limit)
                .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<int> Count(Expression<Func<UserAdvert, bool>> predicate, CancellationToken cancellationToken)
        {
            var compiled = predicate.Compile();
            return DbSet
                .Where(compiled)
                .Count();
        }

    }
}