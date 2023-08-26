namespace Domain.Entities;

public class Booking : BaseAuditableEntity
{
    public int NecessarySeats { get; set; } = 1;
    public User User { get; set; }
    public Guid UserId { get; set; }
    
    public Ride Ride { get; set; }
    public Guid RideId { get; set; }
    
    public BookingStatus Status { get; set; }
}