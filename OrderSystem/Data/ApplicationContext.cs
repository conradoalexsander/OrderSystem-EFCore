using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderSystem.Domain;

namespace OrderSystem.Data
{
    public class ApplicationContext: DbContext
    {
        private static readonly ILoggerFactory _logger = LoggerFactory.Create(p => p.AddConsole());

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var connectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource= "(localdb)\\MSSQLLocalDB",
                InitialCatalog = "OrderSystem",
                IntegratedSecurity = true, //use the logged in user's system integrated security
            };

            optionsBuilder
                .UseLoggerFactory(_logger)
                .EnableSensitiveDataLogging()
                .UseSqlServer(connectionStringBuilder.ConnectionString);
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
