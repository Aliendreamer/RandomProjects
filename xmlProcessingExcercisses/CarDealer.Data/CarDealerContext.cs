namespace CarDealer.Data
{   
    using Configurations;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class CarDealerContext:DbContext
    {
        public CarDealerContext(){}

        public CarDealerContext(DbContextOptions options)
            : base(options){}

        public DbSet<Car> Cars { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<PartCar> PartCars { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Sale> Sales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);

            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConfigString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new CarConfiguration());
            builder.ApplyConfiguration(new PartConfiguration());
            builder.ApplyConfiguration(new PartCarConfiguration());
            builder.ApplyConfiguration(new CustomerConfiguration());
            builder.ApplyConfiguration(new SupplierConfiguration());
            builder.ApplyConfiguration(new SaleConfiguration());
        }
    }
}
