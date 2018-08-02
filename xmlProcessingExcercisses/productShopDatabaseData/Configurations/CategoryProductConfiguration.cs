namespace productShopDatabase.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class CategoryProductConfiguration: IEntityTypeConfiguration<CategoryProduct>
    {
        public void Configure(EntityTypeBuilder<CategoryProduct> builder)
        {

            builder.HasKey(x => new {x.CategoryId, x.ProductId});

            builder.HasOne(cp=>cp.Category)                
                .WithMany(cp => cp.Products)
                .HasForeignKey(cp=> cp.CategoryId);

            builder.HasOne(cp =>cp.Product)
                .WithMany(cp=>cp.Categories)
                .HasForeignKey(cp =>cp.ProductId);

        }
    }
}
