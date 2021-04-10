using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace LegendaryDashboard.Contracts.Contracts.AdvertImage.Requests
{
    public class AdvertImageCreateRequest
    {
        public int Id { get; set; }
        public IFormFileCollection Files { get; set; }
    }
}