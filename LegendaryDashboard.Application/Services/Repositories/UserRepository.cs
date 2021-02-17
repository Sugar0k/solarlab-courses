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
    public sealed class UserRepository : Repository<User, int>, IUserRepository
    {
        public UserRepository( DashboardContext context): base(context)
        {
        }

        public async Task<List<User>> GetPaged(int offset, int limit, CancellationToken cancellationToken)
        {
            return await DbSet
                .OrderBy(u => u.Id)
                .Skip(offset)
                .Take(limit)
                .ToListAsync(cancellationToken: cancellationToken);
        }
        public async Task<int> Count(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
        {
            var compiled = predicate.Compile();
            return DbSet
                .Where(compiled)
                .Count();
        }
        public async Task<User> GetByEmail(string email, CancellationToken cancellationToken)
        {
            return await DbSet.FirstOrDefaultAsync(c => c.Email == email, cancellationToken);
        }
        public async Task<User> GetByPhone(string phone, CancellationToken cancellationToken)
        {
            return await DbSet.FirstOrDefaultAsync(c => c.Phone == phone, cancellationToken);
        }
    }
}