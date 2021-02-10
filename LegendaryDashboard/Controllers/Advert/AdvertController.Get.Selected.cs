using System.Collections.Generic;
using System.Linq;
using LegendaryDashboard.Contracts.Contracts.Advert.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LegendaryDashboard.Controllers.Advert
{
    public partial class AdvertController
    {
        [HttpPost("selected")]
        public List<Contracts.Contracts.Advert.AdvertDto> GetSelectedAdverts(GetAdvertsRequest request)
        {
            var query = _db.Adverts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                query = query.Where(x => EF.Functions.Like(x.Title, $"%{request.Title}%"));
            }

            if (request.MinPrice.HasValue)
            {
                query = query.Where(x => x.Price != null && x.Price.Value >= request.MinPrice.Value);
            }

            if (request.MaxPrice.HasValue)
            {
                query = query.Where(x => x.Price != null && x.Price.Value <= request.MaxPrice.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.State))
            {
                query = query.Where(x => EF.Functions.Like(x.State, $"%{request.State}%"));
            }

            if (!string.IsNullOrWhiteSpace(request.City))
            {
                query = query.Where(x => EF.Functions.Like(x.City, $"%{request.City}%"));
            }

            return query.Select(x => x.ToDto()).ToList();
        }
    }
}