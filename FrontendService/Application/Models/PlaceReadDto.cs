namespace FrontendService.Application.Models
{
    public class PlaceReadDto
    {
        public Guid Id { get; set; }
        public string? OwnerName { get; set; }
        public string? PlaceName { get; set; }
        public string? Longitude { get; set; }
        public string? Lattitude { get; set; }
        public string? Address { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
