namespace CarDealer.Data.Configurations
{

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class PartConfiguration : IEntityTypeConfiguration<Part>
    {
        public void Configure(EntityTypeBuilder<Part> builder)
        {
            builder.HasKey(x => x.PartId);

            builder.HasOne(x => x.Supplier)
                .WithMany(x => x.Parts)
                .HasForeignKey(x => x.SupplierId);

        }
    }
}
