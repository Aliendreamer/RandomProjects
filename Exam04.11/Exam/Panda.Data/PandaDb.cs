namespace Panda.Data
{
    using Domain.Models;
    using Microsoft.EntityFrameworkCore;

    public class PandaDb : DbContext
    {
        private const string ConnectionString = @"Server=(LocalDb)\.;Database=PandaDb;Integrated Security=True;";

        public PandaDb(DbContextOptions options) : base(options)
        {
        }

        public PandaDb()
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Receipt> Receipts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Receipt>().HasOne(a => a.Recipient).WithMany(x => x.Receipts)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<User>().HasMany(a => a.Packages).WithOne(x => x.Recipient)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}