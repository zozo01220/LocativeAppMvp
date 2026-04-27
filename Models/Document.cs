public class Document
{
    public int Id { get; set; }
    public string OwnerId { get; set; }   // 🔥 AJOUT SaaS
    public Owner Owner { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }

    public int? CandidateId { get; set; }
    public Candidate? Candidate { get; set; }

    public int? TenantId { get; set; }
    public Tenant? Tenant { get; set; }

    public DateTime UploadedAt { get; set; } = DateTime.Now;

}