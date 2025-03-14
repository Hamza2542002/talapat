using System.ComponentModel.DataAnnotations;

namespace Talabat.Dtos
{
    public class CustomerCartDTO
    {
        [Required]
        public string? Id { get; set; }
        public int DeliveryMethodId { get; set; }
        public List<CartItemDTO>? CartItems { get; set; } = [];
    }
}
