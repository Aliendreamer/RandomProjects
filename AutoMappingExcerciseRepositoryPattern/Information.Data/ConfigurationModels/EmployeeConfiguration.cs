namespace Information.Data.ConfigurationModels
{
    using InformationModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class EmployeeConfiguration:IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.EmployeeId);

            builder.Property(e => e.FirstName)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            builder.Property(e => e.LastName)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            builder.Property(e => e.Salary)
                .IsRequired()
                .HasColumnType("DECIMAL(18,2)");

            builder.Property(p => p.JobTitle)
                .IsRequired(false)
                .IsUnicode()
                .HasMaxLength(100);

            builder.HasOne(e => e.Manager)
                .WithMany(e => e.Employees)
                .HasForeignKey(e => e.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
