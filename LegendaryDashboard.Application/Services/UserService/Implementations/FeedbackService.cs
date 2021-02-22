using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LegendaryDashboard.Application.Services.UserService.Interfaces;
using LegendaryDashboard.Contracts.Contracts.Feedback;
using LegendaryDashboard.Contracts.Contracts.Feedback.Requests;
using LegendaryDashboard.Domain.Models;
using LegendaryDashboard.Infrastructure.IRepositories;

namespace LegendaryDashboard.Application.Services.UserService.Implementations
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _repository;
        private readonly IMapper _mapper;

        public FeedbackService(IFeedbackRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Create(FeedbackCreateRequest createRequest, CancellationToken cancellationToken)
        {
            var feedback = _mapper.Map<Feedback>(createRequest);
            feedback.CreateDate = DateTime.UtcNow;
            await _repository.Save(feedback, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _repository.Delete(id, cancellationToken);
        }

        public async Task<IEnumerable<FeedbackDto>> Get(
            FeedbackGetRequest getRequest, 
            CancellationToken cancellationToken)
        {
            var feedbacks= await _repository.GetPaged(getRequest, cancellationToken);
            return _mapper.Map<List<FeedbackDto>>(feedbacks);
        }

        public async Task Update(FeedbackUpdateRequest updateRequest, CancellationToken cancellationToken)
        {
            var feedback = await _repository.GetById(updateRequest.Id, cancellationToken);
            if (feedback == null) throw new Exception("Обновляемый элемент не найден");
            feedback.Text = updateRequest.Text;
            feedback.Rating = updateRequest.Rating;
            await _repository.Update(feedback, cancellationToken);
        }

        public async Task<int> Count(Expression<Func<Feedback, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _repository.Count(predicate, cancellationToken);
        }
    }
}