using System.ComponentModel.DataAnnotations;

namespace LegendaryDashboard.Contracts
{
    public class AdvertImageCreateRequest
    {
        [Required(ErrorMessage = "UserId required ")] // необходимо название изображения
        public string ImageGuid { get; set; }      
        
        [Required(ErrorMessage = "UserId required ")] // необходим id объявления
        public int AdvertId { get; set; }     
    }
}