using LocativeApp.Data;

//public class Owner
//{
//    public string Id { get; set; } = Guid.NewGuid().ToString();
//    public string Name { get; set; }
//    public string? Description { get; set; }
//}
public class OwnerStatsDto
{
    public string OwnerId { get; set; }
    public int UsersCount { get; set; }
    public int PropertiesCount { get; set; }
    public int TenantsCount { get; set; }
}

public class Owner
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; }
    public string Email { get; set; }

    public bool IsActive { get; set; } = true;

    public string Plan { get; set; } = "Free";

    public int MaxUsers { get; set; } = 5;
    public int MaxProperties { get; set; } = 20;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<ApplicationUser> Users { get; set; }
    public ICollection<Property> Properties { get; set; }
}