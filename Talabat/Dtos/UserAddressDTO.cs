using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Talabat.Dtos
{
    public class UserAddressDTO
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Street { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? Country { get; set; }
    }
}
