using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; } 
    public string LastName { get; set; } 

    public ICollection<Ride> Rides { get; set; } = null!;
    public ICollection<Booking> Bookings { get; set; } = null!;
    public ICollection<Notification> Notifications { get; set; } = null!;
    public ICollection<Review> Reviews { get; set; } = null!;
    public ICollection<Chat> Chats { get; set; } = null!;
}