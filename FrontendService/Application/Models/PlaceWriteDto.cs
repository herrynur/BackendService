using System.ComponentModel.DataAnnotations;

namespace FrontendService.Application.Models
{
    public class PlaceWriteDto
    {
        [Required]
        public string? OwnerName { get; set; }
        [Required]
        public string? PlaceName { get; set; }
        public string? Longitude { get; set; }
        public string? Lattitude { get; set; }
        [Required]
        public string? Address { get; set; }
    }
}
