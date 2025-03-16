using System.ComponentModel.DataAnnotations;

namespace Talabat.Dtos
{
    public class CustomerCartDTO
    {
        [Required]
        public string? Id { get; set; }
        //public int DeliveryMethodId { get; set; }
        //[Required]
        //public string? PaymentIntentId { get; set; }
        //[Required]
        //public string? ClientSecret { get; set; }
        public List<CartItemDTO>? CartItems { get; set; } = [];
    }
}
