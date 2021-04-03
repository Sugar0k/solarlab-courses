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
        [Route("updatePassword")]
        public async Task<IActionResult> UpdatePassword(
            int userId, string oldPassword, string newPassword,
            CancellationToken cancellationToken)
        {
            await _userService.UpdatePassword(userId, oldPassword, newPassword, cancellationToken);
            return Ok();
        }
    }
}