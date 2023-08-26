namespace Domain.Entities;

public class Driver : User
{
    public double Rating { get; set; }
    
    public ICollection<Car> Cars { get; set; } = null!;
}