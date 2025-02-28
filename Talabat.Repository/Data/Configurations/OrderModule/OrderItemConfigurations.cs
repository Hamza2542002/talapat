using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repository.Data.Configurations.OrderModule
{
    internal class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.OrderedItemProduct.ProductId).IsRequired();
            builder.Property(o => o.OrderedItemProduct.ProductName).IsRequired();
            builder.Property(o => o.Price).IsRequired();
            builder.Property(o => o.Price).HasColumnType("decimal(18,2)");
            builder.Property(o => o.Quantity).IsRequired();

            builder.OwnsOne(o => o.OrderedItemProduct, x => x.WithOwner());
        }
    }
}
