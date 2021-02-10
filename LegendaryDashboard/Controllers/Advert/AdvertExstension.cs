namespace LegendaryDashboard.Controllers.Advert
{
    public static class AdvertExtension
    {
        public static Contracts.Contracts.Advert.AdvertDto ToDto(this Domain.Models.Advert advert)
        {
            return new()
            {
                Id = advert.Id,
                CategoryId = advert.CategoryId,
                Title = advert.Title,
                Description = advert.Description,
                CreationDate = advert.CreationDate,
                Price = advert.Price,
                State = advert.State,
                City = advert.City,
                Street = advert.Street,
                House = advert.House,
                Views = advert.Views
            };
        }
    }
}