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
using LegendaryDashboard.Domain.Exceptions;
using LegendaryDashboard.Domain.Models;
using LegendaryDashboard.Infrastructure.IRepositories;
using Microsoft.AspNetCore.Http;

namespace LegendaryDashboard.Application.Services.FeedbackService.Implementations
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;


        public FeedbackService(IFeedbackRepository repository, IMapper mapper, IHttpContextAccessor accessor)
        {
            _repository = repository;
            _mapper = mapper;
            _accessor = accessor;
        }

        public async Task Create(FeedbackCreateRequest createRequest, CancellationToken cancellationToken)
        {
            int commentatorId = ClaimsPrincipalExtensions.GetUserId(_accessor);
            var feedback = _mapper.Map<Feedback>(createRequest);
            feedback.CommentatorId = commentatorId;
            feedback.CreateDate = DateTime.UtcNow;
            await _repository.Save(feedback, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var feedback = await _repository.GetById(id, cancellationToken);
            if (!ClaimsPrincipalExtensions.IsAdminOrOwner(_accessor, feedback.CommentatorId))
                throw new Exception("Feedback не пренадлежит текущему пользователю");
            
            await _repository.Delete(id, cancellationToken);
        }

        public async Task<PagedResponse<FeedbackDto>> GetPaged( 
            int id,
            int offset, 
            int limit, 
            CancellationToken cancellationToken)
        {
            var feedbacks = await _repository.GetPaged(
                (f => f.UserId == id),
                offset, limit, cancellationToken);
            var feedbacksDto = _mapper.Map<List<FeedbackDto>>(feedbacks.EntityList);
            return new PagedResponse<FeedbackDto>
            {
                Count = feedbacks.Count,
                EntityList = feedbacksDto
            };
        }
        public async Task<FeedbackDto> GetById(int id, CancellationToken cancellationToken)
        {
            var feedback = await _repository.GetById(id, cancellationToken);
            return _mapper.Map<FeedbackDto>(feedback);
        }

        public async Task Update(FeedbackUpdateRequest updateRequest, CancellationToken cancellationToken)
        {
            var feedback = await _repository.GetById(updateRequest.Id, cancellationToken); 
            if (feedback == null)
                throw new EntityNotFoundException("Обновляемый элемент не найден");
            
            if (!ClaimsPrincipalExtensions.IsAdminOrOwner(_accessor, feedback.CommentatorId))
                throw new Exception("Feedback не пренадлежит текущему пользователю");
            
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