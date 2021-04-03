using LegendaryDashboard.Application.Services.UserService.Interfaces;
using LegendaryDashboard.Infrastructure.DbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.User
{
    [ApiController]
    [Authorize]
    [Route("api/user/")]
    [EnableCors("MyPolicy")]
    public partial class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
    }
}