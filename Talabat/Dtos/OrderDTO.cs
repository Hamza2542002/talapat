using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Dtos
{
    public class OrderDTO
    {
        [Required]
        public string? CustomerEmail { get; set; }
        [Required]
        public string? CartId { get; set; }
        [Required]
        public AddressDTO? OrderAddress { get; set; }
        [Required]
        public int DelevryMethodId { get; set; }
    }
}
