using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.UserService.Interfaces;
using LegendaryDashboard.Contracts.Contracts.User.Requests;
using Microsoft.AspNetCore.Http;

namespace LegendaryDashboard.Api.Controllers.User
{
    public partial class UserController
    {
        //TODO: Добавить Authorize(Roles = "admin")
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(
            [FromBody] CreateUserRequest request,
            [FromServices] IUserService service,
            CancellationToken cancellationToken
        )
        {
            
            var response = await service.Register(new CreateUserRequest()
            {
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                Phone = request.Phone,
                Email = request.Email,
                RoleId = request.RoleId
            }, cancellationToken);
            
            return Created($"api/v1/users/{response.Id}", response);
            /*if (_db.Users.Any(c => c.Phone == request.Phone || c.Email == request.Email))
            {
                return BadRequest("Пользователь уже существует");
            }

            var newUser = new Domain.Models.User
            {
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                Phone = request.Phone,
                Email = request.Email,
                PasswordHash = request.PasswordHash,
                RegisterDate = DateTime.UtcNow,
                RoleId = request.RoleId
            };

            _db.Users.Add(newUser);
            _db.SaveChangesAsync();

            return Created($"api/user/id/{newUser.Id}", newUser.ToDto());*/
        }
    }
}