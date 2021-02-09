using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Controllers.Role
{
    public partial class RoleController
    {
        [HttpGet("all")]
        public List<Contracts.RoleDto.RoleDto> Get()
        {
            return _db.Roles.Select(x => x.ToDto()).ToList();
        }
    }
}