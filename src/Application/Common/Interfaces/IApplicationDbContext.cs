using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Booking> Bookings { get; }
    DbSet<Car> Cars { get; }
    DbSet<Chat> Chats { get; }
    DbSet<Driver> Drivers { get; }
    DbSet<Ride> Rides { get; }
    DbSet<User> Users { get; }
    DbSet<Review> Reviews { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}