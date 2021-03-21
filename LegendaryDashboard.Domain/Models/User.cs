using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LegendaryDashboard.Domain.Models
{
    public class User : BaseEntity<int>
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime RegisterDate { get; set; }
        
        public string Role { get; set; }

        public ICollection<UserAdvert> UsersAdverts { get; set; } = new HashSet<UserAdvert>();//связь со списком объявлений
        
        /// <summary>
        /// Отправленные отзывы.
        /// </summary>
        public ICollection<Feedback> SentFeedbacks { get; set; } = new HashSet<Feedback>();
        
        /// <summary>
        /// Полученные отзывы.
        /// </summary>
        public ICollection<Feedback> TakenFeedbacks { get; set; } = new HashSet<Feedback>();
        
        
    }
}