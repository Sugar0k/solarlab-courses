using System.Collections.Generic;

namespace LegendaryDashboard.Contracts.FeedbackDto
{
    public class FeedbackTotalDto
    {
        public int Total { get; set; }
        public IEnumerable<FeedbackDto> Items { get; set; }
    }
}