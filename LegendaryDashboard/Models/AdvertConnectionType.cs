using System.Collections.Generic;

namespace LegendaryDashboard.Models
{
    public class AdvertConnectionType : BaseEntity
    {
        public string Heading { get; set; }
        
        public List<UserAdvert> UsersAdverts { get; set; }
    }
}