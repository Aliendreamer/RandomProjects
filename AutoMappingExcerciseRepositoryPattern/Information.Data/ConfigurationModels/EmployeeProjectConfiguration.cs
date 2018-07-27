namespace Information.Data.ConfigurationModels
{
    using InformationModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class EmployeeProjectConfiguration:IEntityTypeConfiguration<EmployeeProject>
    {
        public void Configure(EntityTypeBuilder<EmployeeProject> builder)
        {
            builder.HasKey(e =>new{ e.EmployeeId,e.ProjectId});

            builder.HasOne(ep => ep.Employee)
                .WithMany(e => e.Projects)
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ep => ep.Project)
                .WithMany(e => e.Employees)
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
