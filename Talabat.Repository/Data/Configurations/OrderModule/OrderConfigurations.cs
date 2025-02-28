using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repository.Data.Configurations.OrderModule
{
    internal class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.CustomerEmail).IsRequired();
            builder.Property(o => o.SubTotal).HasColumnType("decimal(18,2)");


            builder.OwnsOne(o => o.OrderAddress, a => a.WithOwner()); // [1:1] Total

            builder.Property(o => o.Status)
                .HasConversion(
                    oStatus => oStatus.ToString(),
                    oStatus => Enum.Parse<OrderStatus>(oStatus)
                );

            builder.HasOne(o => o.DeleveryMethod).WithMany();



        }
    }
}
