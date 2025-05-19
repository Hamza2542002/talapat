using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Dtos
{
    public class OrderToReturnDTO
    {
        public int Id { get; set; }
        public string CustomerEmail { get; set; } = string.Empty;
        public string Status { get; set; }
        public ICollection<OrderItemDTO> OredrItems { get; set; } = [];
        public Address OrderAddress { get; set; } = new();
        public string DeleveryMethodName { get; set; } = string.Empty;
        public int DeleveryMethodCost { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public string PaymentIntentId { get; set; } = string.Empty;
        public string CartId { get; set; }
        public string ClientSecret { get; set; }
    }
}
