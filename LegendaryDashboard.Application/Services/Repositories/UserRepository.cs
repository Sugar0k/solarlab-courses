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
            // var users = DbSet
            //     .OrderBy(u => u.Id)
            //     .Skip(offset)
            //     .Take(limit);
            return DbSet.ToList();
        }
        public async Task<int> Count(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
        {
            var compiled = predicate.Compile();
            return DbSet
                .Where(compiled)
                .Count();
        }
        
        
    }
}