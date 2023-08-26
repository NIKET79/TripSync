namespace Domain.Entities;

public class Ride : BaseAuditableEntity
{
    public Driver Driver { get; set; }
    public Guid DriverId { get; set; }
    
    public int AvailableSeats { get; set; }
    
    public DateTime Starts { get; set; }
    public Location StartLocation { get; set; }
    public Location EndLocation { get; set; }

    public ICollection<User> Users { get; set; } = null!;
}