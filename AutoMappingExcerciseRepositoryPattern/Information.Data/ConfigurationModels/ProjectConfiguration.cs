namespace Information.Data.ConfigurationModels
{
    using InformationModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ProjectConfiguration:IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(p => p.ProjectId);

            builder.Property(p => p.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(p => p.Goal)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(p => p.StartDate)
                .HasColumnType("DATETIME2");

            builder.Property(p => p.EndDate)
                .IsRequired(false)
                .HasColumnType("DATETIME2");

            builder.HasOne(p => p.Manager)
                .WithMany(e => e.ManagerProjects)
                .HasForeignKey(e => e.ManagerId)
                .IsRequired();
                

        }
    }
}
