using System.Collections.Generic;
using System.Linq;
using LegendaryDashboard.Contracts.Contracts.Role;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Role
{
    public partial class RoleController
    {
        [HttpGet("{id}")]
        public RoleDto Get(int id)
        {
            return _db.Roles.FirstOrDefault(c => c.Id == id).ToDto();
        }
    }
}