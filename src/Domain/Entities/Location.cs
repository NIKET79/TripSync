using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Owned] public class Location : BaseAuditableEntity
{
    public string City { get; set; }
    public string Address { get; set; }
    
    // public string Longitude { get; set; }
    // public string Latitude { get; set; }
}