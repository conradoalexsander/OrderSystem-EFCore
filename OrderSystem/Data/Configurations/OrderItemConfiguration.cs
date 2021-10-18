using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderSystem.Domain;

namespace OrderSystem.Data.Configurations
{
    class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItem");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Quantity).HasDefaultValue(1).ValueGeneratedOnAdd();
            builder.Property(p => p.Value).IsRequired();
            builder.Property(p => p.Discount).IsRequired();
        }
    }
}
