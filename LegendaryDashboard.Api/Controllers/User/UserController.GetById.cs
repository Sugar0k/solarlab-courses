using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.UserService.Interfaces;
using LegendaryDashboard.Contracts.Contracts.User;
using LegendaryDashboard.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.User
{
    public partial class UserController
    {
        [Authorize]
        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(
            int id,
            [FromServices] IUserService service,
            CancellationToken cancellationToken
        )
        {
            var user = await service.FindById(id, cancellationToken);
            return Ok(user);
        }
    }
}