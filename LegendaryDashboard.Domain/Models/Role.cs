using System.Collections.Generic;

namespace LegendaryDashboard.Domain.Models
{
    public class Role : BaseEntity<int>
    {
        public string Name { get; set; }
        public List<User> Users { get; set; } //Список пользователей с этой ролью
    }
}