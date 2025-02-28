using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repository.Data.Configurations.OrderModule
{
    internal class DeleveryMethodConfigurations : IEntityTypeConfiguration<DeleveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeleveryMethod> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.DeliveryTime).IsRequired();
            builder.Property(m => m.ShortName).IsRequired();
            builder.Property(m => m.Cost).IsRequired();
            builder.Property(m => m.Cost).HasColumnType("decimal(18,2)");
        }
    }
}
