using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace LegendaryDashboard.Contracts.Contracts.AdvertImage.Requests
{
    public class AdvertImageCreateRequest
    {
        [Required(ErrorMessage = "1 required ")]
        public int Id { get; set; } 
        [Required(ErrorMessage = "2 required ")]
        public IFormFile File { get; set; }   
    }
}