using System.Collections.Generic;
using System.Linq;
using LegendaryDashboard.Contracts.Feedback.Requests;
using LegendaryDashboard.Contracts.FeedbackDto;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Controllers.Feedback
{
    public partial class FeedbackController
    {
        [HttpPost]
        [Route("get")]
        public FeedbackTotalDto Get(FeedbackGetRequest request)
        {
            var feedbacks = _db.Feedbacks.Where(c => c.UserId == request.UserId);
            return new FeedbackTotalDto
            {
                Total = feedbacks.Count(),
                Items = feedbacks.Skip(request.ToSkip)
                    .Take(request.ToTake)
                    .Select(feedback => new FeedbackDto
                    {
                        Id = feedback.Id,
                        UserId = feedback.UserId,
                        CommentatorId = feedback.CommentatorId,
                        Text = feedback.Text,
                        Rating = feedback.Rating
                    }).ToList()
            };
        }
    }
}