using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LegendaryDashboard.Models
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime RegisterDate { get; set; }
        
        public int RoleId { get; set; }      // внешний ключ
        public Role Role { get; set; }      // навигационное свойство

        public List<UserAdvert> UsersAdverts { get; set; } //связь со списком объявлений
        
        /// <summary>
        /// Отправленные отзывы.
        /// </summary>
        public List<Feedback> SentFeedbacks { get; set; } 
        
        /// <summary>
        /// Полученные отзывы.
        /// </summary>
        public List<Feedback> TakenFeedbacks { get; set; } 
        
        
    }
}