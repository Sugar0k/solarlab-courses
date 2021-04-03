using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Contracts.Contracts.UserAdvert.Requests;
using LegendaryDashboard.Domain.Common;
using LegendaryDashboard.Domain.Models;
using LegendaryDashboard.Infrastructure.DbContext;
using LegendaryDashboard.Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace LegendaryDashboard.Application.Services.Repositories
{
    public sealed class UserAdvertRepository : Repository<UserAdvert, int>, IUserAdvertRepository
    {
        public UserAdvertRepository( DashboardContext context): base(context)
        {
        }

        public async Task<int> GetOwnerId(int advertId, CancellationToken cancellationToken)
        {
            return (await DbSet.FirstAsync(
                u => u.AdvertId == advertId && 
                     u.ConnectionType == AdvertUserConnectionTypes.OwnerConnection,
                cancellationToken)).UserId;
        }
        
        public async Task<List<UserAdvert>> GetConnections(
            Expression<Func<UserAdvert, bool>> predicate, 
            CancellationToken cancellationToken)
        {
            return await DbSet
                .Where(predicate)
                .OrderBy(u => u.Id)
                .ToListAsync(cancellationToken: cancellationToken);
        }
        
        public async Task DeleteByAdvertId(int advertId, CancellationToken cancellationToken)
        {
            DbSet.RemoveRange(
                await DbSet.Where(u => u.AdvertId == advertId)
                    .ToArrayAsync(cancellationToken));
            await Context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteByUserId(int userId, CancellationToken cancellationToken)
        {
            DbSet.RemoveRange(
                await DbSet.Where(u => u.UserId == userId)
                    .ToArrayAsync(cancellationToken));
            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}