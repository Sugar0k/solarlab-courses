using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LegendaryDashboard.Application.Services.FeedbackService.Interfaces;
using LegendaryDashboard.Contracts.Contracts;
using LegendaryDashboard.Contracts.Contracts.Feedback;
using LegendaryDashboard.Contracts.Contracts.Feedback.Requests;
using LegendaryDashboard.Domain.Models;
using LegendaryDashboard.Infrastructure.IRepositories;

namespace LegendaryDashboard.Application.Services.FeedbackService.Implementations
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

        public async Task<PagedResponce<FeedbackDto>> Get(
            FeedbackGetRequest getRequest, 
            CancellationToken cancellationToken)
        {
            var feedbacks = await _repository.GetPaged(getRequest.Offset, getRequest.Limit, cancellationToken);
            var feedbacksDto = _mapper.Map<List<FeedbackDto>>(feedbacks.EntityList);
            return new PagedResponce<FeedbackDto>
            {
                Count = feedbacks.Count,
                EntityList = feedbacksDto
            };
        }

        // public async Task Update(FeedbackUpdateRequest updateRequest, CancellationToken cancellationToken)
        // {
        //     var feedback = await _repository.GetById(updateRequest.Id, cancellationToken);
        //     if (feedback == null) throw new EntityNotFoundException("Обновляемый элемент не найден");
        //     feedback.Text = updateRequest.Text;
        //     feedback.Rating = updateRequest.Rating;
        //     await _repository.Update(feedback, cancellationToken);
        // }

        public async Task<int> Count(Expression<Func<Feedback, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _repository.Count(predicate, cancellationToken);
        }
    }
}