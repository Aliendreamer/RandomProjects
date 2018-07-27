namespace Information.Data
{
    using ConfigurationModels;
    using InformationModels;
    using Microsoft.EntityFrameworkCore;

    public class InformationDbContext : DbContext
    {
        public InformationDbContext(DbContextOptions options) 
            : base(options)
        {
           
        }

        public InformationDbContext()
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<EmployeeProject> EmployeeProjects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;
            optionsBuilder.UseLazyLoadingProxies(true);
            optionsBuilder.UseSqlServer(Configuration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new EmployeeConfiguration());
            builder.ApplyConfiguration(new ProjectConfiguration());
            builder.ApplyConfiguration(new EmployeeProjectConfiguration());
        }
    }
}
