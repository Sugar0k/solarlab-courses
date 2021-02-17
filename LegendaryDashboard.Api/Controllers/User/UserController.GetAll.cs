using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.UserService.Implementations;
using LegendaryDashboard.Application.Services.UserService.Interfaces;
using LegendaryDashboard.Contracts.Contracts.User;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.User
{
    public partial class UserController
    {
        [HttpGet("all")]
        public async Task<IActionResult> Get(
            int offset,  int limit,
            [FromServices] IUserService service,
            CancellationToken cancellationToken
        )
        {
            var users = await service.GetPaged(offset, limit, cancellationToken);
            return Ok(users);
        }
    }
}