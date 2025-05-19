namespace Talabat.Core.Entities
{
    public class CustomerCart
    {
        public string? Id { get; set; }
        //public string? PaymentIntentId { get; set; }
        //public string? ClientSecret { get; set; }
        //public int DeliveryMethodId { get; set; }
        //public decimal DeliveryMethodCost { get; set; }

        public List<CartItem>? CartItems { get; set; } = [];
    }
}
