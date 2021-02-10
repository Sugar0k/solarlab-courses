using System.Collections.Generic;

namespace LegendaryDashboard.Contracts.Contracts.Feedback
{
    public class FeedbackTotalDto
    {
        public int Total { get; set; }
        public IEnumerable<Contracts.Feedback.FeedbackDto> Items { get; set; }
    }
}