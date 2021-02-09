using System.Collections.Generic;

namespace LegendaryDashboard.Models
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public List<User> Users { get; set; } //Список пользователей с этой ролью
    }
}