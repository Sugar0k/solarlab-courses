using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Contracts.Contracts;
using LegendaryDashboard.Contracts.Contracts.Feedback.Requests;
using LegendaryDashboard.Domain.Models;
using LegendaryDashboard.Infrastructure.DbContext;
using LegendaryDashboard.Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace LegendaryDashboard.Application.Services.Repositories
{
    public sealed class FeedbackRepository : Repository<Feedback, int>, IFeedbackRepository
    {
        public FeedbackRepository(DashboardContext context) : base(context)
        {
        }
        
        /*public async Task<PagedResponse<Feedback>> GetPaged(
            Expression<Func<Feedback, bool>> predicate, 
            int offset, 
            int limit, 
            CancellationToken cancellationToken)
        {
            var feedbacks = DbSet.Where(predicate)
                .OrderBy(f => f.CreateDate);
                
            return new PagedResponse<Feedback>
            {
                Count = await feedbacks.CountAsync(cancellationToken),
                EntityList = await feedbacks.Skip(offset)
                    .Take(limit)
                    .ToListAsync(cancellationToken)
            };
        }*/

        public async Task<List<Feedback>> GetPaged(FeedbackGetRequest getRequest, CancellationToken cancellationToken)
        {
            return await DbSet
                .Where(f=> f.UserId == getRequest.UserId)
                .OrderBy(f => f.CreateDate) 
                .Skip(getRequest.Offset)
                .Take(getRequest.Limit)
                .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<int> Count(Expression<Func<Feedback, bool>> predicate, CancellationToken cancellationToken)
        {
            return await DbSet
                .Where(predicate)
                .CountAsync();
        }

        // public async Task Update(Feedback feedback, CancellationToken cancellationToken)
        // { 
        //      DbSet.Update(feedback);
        //      await Context.SaveChangesAsync(cancellationToken);
        // }

        public async Task<Feedback> GetById(int id, CancellationToken cancellationToken)
        {
            var feedback = await DbSet.FirstOrDefaultAsync(f => f.Id == id, cancellationToken);
            return feedback;

        }
    }
}