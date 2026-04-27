public class Candidate
{
    public int Id { get; set; }
    public string OwnerId { get; set; }   // 🔥 AJOUT SaaS
    public Owner Owner { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public DateTime? BirthDate { get; set; }
    public int OccupantCount { get; set; } = 1;
    public int NetMonthlySalary { get; set; }
    public bool IndeterminateContract { get; set; } = true;
    public bool HasGuarantor { get; set; } = true;
    public int GuarantorNetMonthlySalary { get; set; }
    public int? TenantId { get; set; }
    public Tenant? Tenant { get; set; }
    public int? PropertyId { get; set; }
    public Property Property { get; set; }
    public string? Status { get; set; } = "Interested";// New / InReview / Accepted / Rejected
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public List<Document> Documents { get; set; } = new();
    public bool IsActive { get; set; } = true;
}