public class RentPayment
{
    public int Id { get; set; }
    public string OwnerId { get; set; }   // 🔥 AJOUT SaaS
    public Owner Owner { get; set; }
    public int TenantId { get; set; }
    public Tenant Tenant { get; set; }

    public int PropertyId { get; set; }
    public Property Property { get; set; }

    public int Year { get; set; }
    public int Month { get; set; }

    public decimal AmountPaid { get; set; }

    public DateTime? PaidDate { get; set; }

    public bool IsPaid { get; set; }
}