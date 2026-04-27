public class Property
{
    public int Id { get; set; }
    public bool IsActive { get; set; } = true;
    public string OwnerId { get; set; } 
    public Owner Owner { get; set; }
    public string? Title { get; set; }
    public string ResidentialComplex { get; set; }
    public string? Address { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }
    public string? Description { get; set; }
    public decimal? RentAmount { get; set; }
    public decimal? DepositAmount { get; set; }
    public decimal? ChargesAmount { get; set; }
    public string? MainPhotoPath { get; set; }
    // 👇 locataire actuel (optionnel)
    public ICollection<LeaseContract> LeaseContracts { get; set; } = new List<LeaseContract>();
    public List<Candidate> CandidateList { get; set; } = new();
    public bool AcceptingCandidates { get; set; } = false;
    public bool Sold { get; set; } = false;
    public DateTime? SoldDate { get; set; }
    public DateTime? ActivInactivDate { get; set; }
    public DateTime? AcceptingCandidatesDate { get; set; }
    public DateTime? AcquisitionDate { get; set; }
    public int? PropertyTypeId { get; set; }
    public PropertyType PropertyType { get; set; }
    public bool IsFurnished { get; set; }
    public decimal? Surface { get; set; }
    public ICollection<PropertyFeature> PropertyFeatures { get; set; } = new List<PropertyFeature>();
}