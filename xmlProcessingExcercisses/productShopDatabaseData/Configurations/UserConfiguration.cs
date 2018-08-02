namespace productShopDatabase.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class UserConfiguration:IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.HasKey(u => u.Id);


            builder.Property(u => u.LastName)
                .IsRequired();
                

            builder.Property(u => u.Age)
                .IsRequired(false);

            builder.HasMany(u => u.BoughtProducts)
                .WithOne(p => p.Byer)
                .HasForeignKey(x=>x.ByerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.SoldProducts)
                .WithOne(p => p.Seller)
                .HasForeignKey(p=>p.SellerId)
                .OnDelete(DeleteBehavior.Restrict);           

        }
    }
}
