using System.ComponentModel.DataAnnotations.Schema;

namespace LegendaryDashboard.Models
{
    public class Feedback : BaseEntity
    {
        public int UserId { get; set; } //Кому пишут комментарий
        public User User { get; set; }
        
        
        public int CommentatorId { get; set; } //Кто пишет комментарий
        public User Commentator { get; set; }
        
        public string Text { get; set; }
        
        public byte Rating { get; set; } //Значение от 1 до 5 
    }
}