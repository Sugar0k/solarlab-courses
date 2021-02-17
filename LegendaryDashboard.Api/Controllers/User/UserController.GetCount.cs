using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.UserService.Interfaces;
using LegendaryDashboard.Contracts.Contracts.User;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.User
{
    public partial class UserController
    {
        [HttpGet("count")]
        public async Task<IActionResult> GetCount(
            [FromServices] IUserService service,
            CancellationToken cancellationToken
        )
        {
            var count = await service.Count(cancellationToken);
            return Ok(count);
        }
    }
}