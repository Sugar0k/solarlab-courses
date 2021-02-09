using LegendaryDashboard.DbContext;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Controllers.User
{
    [ApiController]
    [Route("api/user/")]
    public partial class UserController : ControllerBase
    {
        private readonly DashboardContext _db;
        public UserController(DashboardContext context)
        {
            _db = context;
        }
    }
}