using LegendaryDashboard.Api.DbContext;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.UserAdvert
{
    [ApiController]
    [Route("api/user_advert/")]
    public partial class UserAdvertController : ControllerBase
    {
        private readonly DashboardContext _db;
        public UserAdvertController(DashboardContext context)
        {
            _db = context;
        }
    }
}