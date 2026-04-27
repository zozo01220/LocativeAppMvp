public class LeaseContract
{
    public int Id { get; set; }

    public int PropertyId { get; set; }
    public Property Property { get; set; }

    public int TenantId { get; set; }
    public Tenant Tenant { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public decimal RentAmount { get; set; }
}