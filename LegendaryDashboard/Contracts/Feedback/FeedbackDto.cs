namespace LegendaryDashboard.Contracts.FeedbackDto
{
    public class FeedbackDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CommentatorId { get; set; }
        public string Text { get; set; }
        public byte Rating { get; set; }
    }
}