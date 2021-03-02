using LegendaryDashboard.Infrastructure.DbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.User
{
    [ApiController]
    [Authorize]
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