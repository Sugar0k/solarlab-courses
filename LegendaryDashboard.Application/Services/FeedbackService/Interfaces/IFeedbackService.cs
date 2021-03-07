using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Contracts.Contracts;
using LegendaryDashboard.Contracts.Contracts.Feedback;
using LegendaryDashboard.Contracts.Contracts.Feedback.Requests;
using LegendaryDashboard.Domain.Models;

namespace LegendaryDashboard.Application.Services.FeedbackService.Interfaces
{
    public interface IFeedbackService
    {
        Task Create(FeedbackCreateRequest createRequest, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task<PagedResponce<FeedbackDto>> Get(FeedbackGetRequest getRequest, CancellationToken cancellationToken);
        // Task Update(FeedbackUpdateRequest updateRequest, CancellationToken cancellationToken);
        Task<int> Count(Expression<Func<Feedback, bool>> predicate, CancellationToken cancellationToken);
    }
}