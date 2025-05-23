﻿namespace Talabat.Core.Entities.Order_Aggregate
{
    public class OrderItem : BaseEntity
    {
        public OrderItem()
        {
        }

        public OrderItem(OrderedItemProduct orderedItemProduct, decimal price, int qauntity)
        {
            OrderedItemProduct = orderedItemProduct;
            Price = price;
            Quantity = qauntity;
        }

        public OrderedItemProduct OrderedItemProduct { get; set; } = new();
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int? OrderId { get; set; }
        public Order? Order { get; set; }
    }
}
