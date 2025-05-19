namespace Talabat.Core.Entities.Order_Aggregate
{
    public class OrderedItemProduct
    {
        public OrderedItemProduct()
        {
            
        }

        public OrderedItemProduct(int productId, string? productName, string? productPicUrl)
        {
            ProductId = productId;
            ProductName = productName;
            ProductPicUrl = productPicUrl;
        }

        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductPicUrl { get; set; }
    }
}