namespace CarDealer.Data.Configurations
{   
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class CarConfiguration:IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(c => c.CarId);

            builder.HasMany(x => x.Sales)
                .WithOne(x => x.Car)
                .HasForeignKey(x => x.CarId);
        }
    }
}
