using LegendaryDashboard.Api.DbContext;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Role
{
    [ApiController]
    [Route("api/role/")]
    public partial class RoleController : ControllerBase
    {
        private readonly DashboardContext _db;
        public RoleController(DashboardContext context)
        {
            _db = context;
        }
    }
}