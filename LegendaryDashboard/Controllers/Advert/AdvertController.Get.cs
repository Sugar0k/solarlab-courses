using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Controllers.Advert
{
    public partial class AdvertController
    {
        [HttpGet("id/{id}")]
        public Contracts.Contracts.Advert.AdvertDto GetById(int id)
        {
            return _db.Adverts.FirstOrDefault(c => c.Id == id).ToDto();
        }
        
        [HttpGet("all")]
        public List<Contracts.Contracts.Advert.AdvertDto> GetAll()
        {
            return _db.Adverts.Select(x => x.ToDto()).ToList();
        }

    }
}