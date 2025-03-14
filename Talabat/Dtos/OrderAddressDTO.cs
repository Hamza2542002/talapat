using System.ComponentModel.DataAnnotations;

namespace Talabat.Dtos
{
    public class OrderAddressDTO
    {
        [Required] 
        public string? FName { get; set; }
        [Required]
        public string? LName { get; set; }
        [Required]
        public string? Street { get; set; }
        [Required]
        public string? Country { get; set; }
        [Required]
        public string? DepNumber { get; set; }
    }
}