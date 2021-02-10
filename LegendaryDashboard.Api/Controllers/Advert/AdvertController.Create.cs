using LegendaryDashboard.Contracts.Contracts.Advert.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Advert
{
    public partial class AdvertController
    {
        
        [HttpPost]
        public IActionResult Create(CreateAdvertRequest request)
        {
            
            var newAdvert = new Domain.Models.Advert
            {
                CategoryId = request.CategoryId,
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                State = request.State,
                City = request.City,
                Street = request.Street,
                House = request.House,                    
            };

            _db.Adverts.Add(newAdvert);
            _db.SaveChangesAsync();

            return Created($"api/advert/id/{newAdvert.Id}", newAdvert.ToDto());
        }

    }
}