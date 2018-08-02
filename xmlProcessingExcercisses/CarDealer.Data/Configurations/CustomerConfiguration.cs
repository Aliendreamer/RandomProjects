namespace CarDealer.Data.Configurations
{   
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class CustomerConfiguration:IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.CustomerId);

            builder.Property(x => x.BirthDate)
                .IsRequired(false);

            builder.Property(x => x.IsYoungDriver)
                .HasDefaultValue(false);
        }
    }
}
