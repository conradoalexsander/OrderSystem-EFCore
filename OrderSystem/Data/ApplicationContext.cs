using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OrderSystem.Domain;

namespace OrderSystem.Data
{
    public class ApplicationContext: DbContext
    {
        //public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var connectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource= "(localdb)\\MSSQLLocalDB",
                InitialCatalog = "OrderSystem",
                IntegratedSecurity = true, //use the logged in user's system integrated security
            };

            optionsBuilder.UseSqlServer(connectionStringBuilder.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // It is better to segregate the configuration in other file. However, this is the way if you don't want it
            // modelBuilder.Entity<OrderItem>(p =>
            // {
            //     p.ToTable("OrderItem");
            //     p.HasKey(p => p.Id);
            //     p.Property(p => p.Quantity).HasDefaultValue(1).ValueGeneratedOnAdd();
            //     p.Property(p => p.Value).IsRequired();
            //     p.Property(p => p.Discount).IsRequired();
            // });

            //Segregating Entity Mapping 

            //First way, apply each configuration mannually
            //modelBuilder.ApplyConfiguration(new CustomerConfiguration());

            //Easy way, load all configurations in assembly

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        }

    }
}
