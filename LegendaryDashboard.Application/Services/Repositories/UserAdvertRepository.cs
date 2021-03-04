using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Contracts.Contracts.UserAdvert.Requests;
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

        public async Task<List<UserAdvert>> GetConnectionsByUserId(GetConnectionsRequest request, CancellationToken cancellationToken)
        {
            return await DbSet
                .Where(c => c.UserId == request.Id)
                .OrderBy(u => u.Id)
                .Skip(request.Offset)
                .Take(request.Limit)
                .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<List<UserAdvert>> GetConnectionsByAdvertId(GetConnectionsRequest request, CancellationToken cancellationToken)
        {
            return await DbSet
                .Where(c => c.AdvertId == request.Id)
                .OrderBy(u => u.Id)
                .Skip(request.Offset)
                .Take(request.Limit)
                .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<List<UserAdvert>> GetConnectionsByAdvertIdAndType(GetConnectionsWithTypeRequest request,
            CancellationToken cancellationToken)
        {
            return await DbSet
                .Where(c => c.AdvertId == request.Id)
                .Where(c => c.ConnectionType.Equals(request.Type))
                .OrderBy(u => u.Id)
                .Skip(request.Offset)
                .Take(request.Limit)
                .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<List<UserAdvert>> GetConnectionsByUserIdAndType(GetConnectionsWithTypeRequest request, CancellationToken cancellationToken)
        {
            return await DbSet
                .Where(c => c.UserId == request.Id)
                .Where(c => c.ConnectionType.Equals(request.Type))
                .OrderBy(u => u.Id)
                .Skip(request.Offset)
                .Take(request.Limit)
                .ToListAsync(cancellationToken: cancellationToken);
        }

    }
}