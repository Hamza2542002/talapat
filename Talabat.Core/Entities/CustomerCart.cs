namespace Talabat.Core.Entities
{
    public class CustomerCart
    {
        public string? Id { get; set; }
        public List<CartItem>? CartItems { get; set; } = [];
    }
}
