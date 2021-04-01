using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.UserService.Interfaces;
using LegendaryDashboard.Contracts.Contracts.User;
using LegendaryDashboard.Contracts.Contracts.User.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace LegendaryDashboard.Api.Controllers.User
{
    public partial class UserController
    {
        [HttpPatch]
        [Route("update")]
        public async Task<IActionResult> Update(
            [FromBody] UpdateUserRequest request,
            CancellationToken cancellationToken)
        {
            await _userService.Update(request, cancellationToken);
            return Ok();
        }
    }
}