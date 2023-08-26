namespace Domain.Entities;

public class Chat : BaseAuditableEntity
{
    public string Title { get; set; }
    public ICollection<User> Users { get; set; }
}