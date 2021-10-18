using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderSystem.Domain;

namespace OrderSystem.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.StartedIn).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
            builder.Property(p => p.FinishedIn);
            builder.Property(p => p.DeliveryType).HasConversion<string>();
            builder.Property(p => p.Status).HasConversion<string>();
            builder.Property(p => p.Observation).HasColumnType("VARCHAR(512)");
            
            builder.HasMany(p => p.Items)
                    .WithOne(p => p.Order)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
