using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderSystem.Domain;

namespace OrderSystem.Data.Configurations
{
    //Segregating Responsability
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Name).HasColumnType("VARCHAR(80)").IsRequired();
            builder.Property(o => o.Telephone).HasColumnType("CHAR(11)");
            builder.Property(o => o.CEP).HasColumnType("CHAR(8)").IsRequired();
            builder.Property(o => o.State).HasColumnType("CHAR(2)").IsRequired();
            builder.Property(o => o.City).HasMaxLength(60).IsRequired();

            builder.HasIndex(i => i.Telephone).HasDatabaseName("idx_customer_phone");
        }
    }
}
