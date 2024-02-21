using BackendService.Helper.Model;
using System.ComponentModel.DataAnnotations;

namespace BackendService.Domain.Entities
{
    public class Place : EntityBase
    {
        [StringLength(100)]
        public string? OwnerName { get; set; }
        [StringLength(100)]
        public string? PlaceName { get; set; }
        [StringLength(50)]
        public string? Longitude { get; set; }
        [StringLength(50)]
        public string? Lattitude { get; set; }
        public string? Address { get; set; }
    }
}
