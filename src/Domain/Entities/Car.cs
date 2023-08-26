namespace Domain.Entities;

public class Car : BaseAuditableEntity
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Number { get; set; }
    public Color Color { get; set; }
    
    public Driver Owner { get; set; }
    public Guid OwnerId { get; set; }
}