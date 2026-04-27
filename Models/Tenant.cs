public class Tenant
{
    public int Id { get; set; }
    public string OwnerId { get; set; }   // 🔥 AJOUT SaaS
    public Owner Owner { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public string? IdentityUserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; } // null = encore actif
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public List<Document> Documents { get; set; } = new();
    public ICollection<LeaseContract> LeaseContracts { get; set; } = new List<LeaseContract>();
}