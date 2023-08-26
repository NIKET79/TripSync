using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class DriverConfiguration : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
        builder.HasMany(d => d.Cars)
            .WithOne(c => c.Owner)
            .HasForeignKey(c => c.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        /*builder.HasMany(d => d.Rides)
            .WithOne(r => r.Driver)
            .HasForeignKey(r => r.DriverId)
            .OnDelete(DeleteBehavior.Cascade);*/

        builder.ToTable("Drivers");
    }
}