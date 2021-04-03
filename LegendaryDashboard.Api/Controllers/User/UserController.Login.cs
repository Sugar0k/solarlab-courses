using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.UserService.Interfaces;
using LegendaryDashboard.Contracts.Contracts.User.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace LegendaryDashboard.Api.Controllers.User
{
    public partial class UserController
    {
        
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Login(
            [FromBody] LoginUserRequest request,
            CancellationToken cancellationToken
        )
        {
            return Ok(await _userService.Login(request, cancellationToken));
        }
    }
}