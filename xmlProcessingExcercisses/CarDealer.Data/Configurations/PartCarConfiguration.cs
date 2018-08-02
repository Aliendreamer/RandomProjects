namespace CarDealer.Data.Configurations
{   
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    class PartCarConfiguration:IEntityTypeConfiguration<PartCar>
    {
        public void Configure(EntityTypeBuilder<PartCar> builder)
        {
            builder.HasKey(x => new {x.CarId, x.PartId});

            builder.HasOne(x => x.Part)
                .WithMany(x => x.Cars)
                .HasForeignKey(x => x.PartId);

            builder.HasOne(x => x.Car)
                .WithMany(x => x.Parts)
                .HasForeignKey(x => x.CarId);
        }
    }
}
