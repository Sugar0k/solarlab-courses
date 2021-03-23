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

        public async Task Update(User user, CancellationToken cancellationToken)
        {
            DbSet.Update(user);
            await Context.SaveChangesAsync(cancellationToken);
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