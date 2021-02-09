using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using LegendaryDashboard.Contracts.UserAdvert.Requests;

namespace LegendaryDashboard.Controllers.UserAdvert
{
    public partial class UserAdvertController
    {
        
        [HttpPost]
        public IActionResult Create(CreateUserAdvertRequest request)
        {
            if (_db.UserAdverts.Any(c => c.UserId == request.UserId && c.AdvertId == request.AdvertId && c.TypeId == request.TypeId))
            {
                return BadRequest("Такая связь уже существует!");
            }

            var newUserAdvert = new Models.UserAdvert
            {
                UserId = request.UserId,
                AdvertId = request.AdvertId,
                TypeId = request.TypeId
            };

            _db.UserAdverts.Add(newUserAdvert);
            _db.SaveChangesAsync();

            return Created($"api/user/id/{newUserAdvert.Id}", newUserAdvert.ToDto());
        }
    }
}