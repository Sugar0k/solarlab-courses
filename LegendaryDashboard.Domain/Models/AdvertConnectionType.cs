using System.Collections.Generic;

namespace LegendaryDashboard.Domain.Models
{
    public class AdvertConnectionType : BaseEntity<int>
    {
        public string Heading { get; set; }
        
        public List<UserAdvert> UsersAdverts { get; set; }
    }
}