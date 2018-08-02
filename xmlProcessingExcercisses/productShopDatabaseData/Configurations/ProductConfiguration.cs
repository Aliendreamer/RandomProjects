namespace productShopDatabase.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class ProductConfiguration:IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Name)
                .IsRequired();
              

            builder.Property(p => p.Price)
                .IsRequired();

            builder.Property(p => p.ByerId)
                .IsRequired(false);

            builder.Property(p => p.SellerId)
                .IsRequired();
        }
    }
}
