using Microsoft.AspNetCore.Identity;

namespace LocativeApp.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        // Common properties
        public string? RoleType { get; set; } // Admin ou Tenant
        // Tenant properties
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        // 🔥 AJOUT SaaS
        public string? OwnerId { get; set; } // NULL = SuperAdmin
        public Owner? Owner { get; set; }
    }

}
