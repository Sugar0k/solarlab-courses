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
        [HttpGet("phone/{phone}")]
        public async Task<IActionResult> GetByPhone(
            string phone,
            [FromServices] IUserService service,
            CancellationToken cancellationToken
        )
        {
            var user = await service.GetByPhone(phone, cancellationToken);
            return Ok(user);
        }
    }
}