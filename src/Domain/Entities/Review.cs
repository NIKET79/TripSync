namespace Domain.Entities;

public class Review : BaseAuditableEntity
{
    public Ride Ride { get; set; }
    public Guid RideId { get; set; }
    
    public User User { get; set; }
    public Guid UserId { get; set; }
    
    public double Rating { get; set; }
    public string? Message { get; set; }
}