public class Assignment
{
    public int Id { get; set; }
    public string OwnerId { get; set; }   // 🔥 AJOUT SaaS
    public Owner Owner { get; set; }
    public int PropertyId { get; set; }
    public string TenantId { get; set; }
}