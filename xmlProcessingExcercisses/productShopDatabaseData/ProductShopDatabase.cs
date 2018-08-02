using Microsoft.EntityFrameworkCore;

namespace productShopDatabaseData
{
    using productShopDatabase.Data.Configurations;
    using productShopDatabase.Models;

    public class ProductShopDatabase : DbContext
    {
        public ProductShopDatabase(DbContextOptions options) : base(options)
        {}

        public ProductShopDatabase(){}

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);

            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConfigString);
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new CategoryProductConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
