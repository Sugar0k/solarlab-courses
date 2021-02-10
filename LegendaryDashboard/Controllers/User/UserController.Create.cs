using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using LegendaryDashboard.Contracts.Contracts.User.Requests;

namespace LegendaryDashboard.Controllers.User
{
    public partial class UserController
    {
        //TODO: Добавить Authorize(Roles = "admin")
        [HttpPost]
        public IActionResult Create(CreateUserRequest request)
        {
            if (_db.Users.Any(c => c.Phone == request.Phone || c.Email == request.Email))
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

            return Created($"api/user/id/{newUser.Id}", newUser.ToDto());
        }
    }
}