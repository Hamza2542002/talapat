namespace Talabat.Core.Entities.Order_Aggregate
{
    public class Order : BaseEntity
    {
        public Order(
            string customerEmail,
            ICollection<OrderItem> oredrItems,
            Address orderAddress,
            DeleveryMethod deleveryMethod,
            decimal subTotal,
            string paymentIntentId
            )
        {
            CustomerEmail = customerEmail;
            OredrItems = oredrItems;
            OrderAddress = orderAddress;
            DeleveryMethod = deleveryMethod;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public Order()
        {
        }

        public string CustomerEmail { get; set; } = string.Empty;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public ICollection<OrderItem> OredrItems { get; set; }
        public Address OrderAddress { get; set; } = new();
        public DeleveryMethod DeleveryMethod { get; set; } = new();
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public decimal SubTotal { get; set; } // price of products only
        public decimal GetTotal()
            => SubTotal + DeleveryMethod.Cost;
        public string PaymentIntentId { get; set; }
    }
}
