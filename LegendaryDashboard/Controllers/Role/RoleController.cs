using LegendaryDashboard.DbContext;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Controllers.Role
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