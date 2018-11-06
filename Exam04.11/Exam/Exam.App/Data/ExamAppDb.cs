namespace Exam.App.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class ExamAppDb : DbContext
    {
        private const string ConnectionString = "Server=(LocalDb)\\.;Database=ExamDb;Integrated Security=True;";

        public ExamAppDb(DbContextOptions options)
            : base(options) { }

        public ExamAppDb()
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Receipt> Receipts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Receipt>().HasOne(a => a.Recipient).WithMany(x => x.Receipts)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<User>().HasMany<Package>(a => a.Packages).WithOne(x => x.Recipient)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}