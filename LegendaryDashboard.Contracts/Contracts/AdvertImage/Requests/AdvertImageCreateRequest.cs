using System.ComponentModel.DataAnnotations;

namespace LegendaryDashboard.Contracts.Contracts.AdvertImage.Requests
{
    public class AdvertImageCreateRequest
    {
        [Required(ErrorMessage = "UserId required ")] // необходимо название изображения
        public string ImageId { get; set; }      
        
        [Required(ErrorMessage = "UserId required ")] // необходим id объявления
        public int AdvertId { get; set; }     
    }
}