namespace Talabat.Core.Entities
{
    public class CartItem
    {
        public int ItemId { get; set; }
        public string? ProductName { get; set; }
        public string? PicUrl { get; set; }
        public string? BrandName { get; set; }
        public string? CategoryName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}