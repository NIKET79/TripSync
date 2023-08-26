using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class RideConfiguration : IEntityTypeConfiguration<Ride>
{
    public void Configure(EntityTypeBuilder<Ride> builder)
    {
        builder.HasKey(r => r.Id);

        builder.OwnsOne(r => r.StartLocation);
        builder.OwnsOne(r => r.EndLocation);

        builder.ToTable("Rides");
    }
}