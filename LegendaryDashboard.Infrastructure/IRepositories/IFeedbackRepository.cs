using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Contracts.Contracts.Feedback.Requests;
using LegendaryDashboard.Domain.Models;

namespace LegendaryDashboard.Infrastructure.IRepositories
{
    public interface IFeedbackRepository : IRepository<Feedback, int>
    {
        Task<List<Feedback>> GetPaged(FeedbackGetRequest getRequest, CancellationToken cancellationToken);
        Task<Feedback> GetById(int id, CancellationToken cancellationToken);
        Task<int> Count(Expression<Func<Feedback, bool>> predicate, CancellationToken cancellationToken);
        Task Update(Feedback feedback, CancellationToken cancellationToken);
        
    }
}