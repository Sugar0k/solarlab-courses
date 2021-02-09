using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Controllers.Role
{
    public partial class RoleController
    {
        [HttpGet("{id}")]
        public Contracts.RoleDto.RoleDto Get(int id)
        {
            return _db.Roles.FirstOrDefault(c => c.Id == id).ToDto();
        }
    }
}