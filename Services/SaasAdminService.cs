using Microsoft.AspNetCore.Identity;
using LocativeApp.Data;

namespace LocativeApp.Services
{
    public class SaasAdminService
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public SaasAdminService(
            ApplicationDbContext db,
            UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        // =========================
        // 🔥 CREATE OWNER (CLIENT SAAS)
        // =========================
        public async Task<Owner> CreateOwner(string name)
        {
            var owner = new Owner
            {
                Id = Guid.NewGuid().ToString(),
                Name = name
            };

            _db.Owners.Add(owner);
            await _db.SaveChangesAsync();

            return owner;
        }

        // =========================
        // 🔥 CREATE ADMIN LINKED TO OWNER
        // =========================
        public async Task<ApplicationUser> CreateAdmin(
            string email,
            string password,
            string ownerId)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                OwnerId = ownerId,
                RoleType = "Admin"
            };

            await _userManager.CreateAsync(user, password);
            await _userManager.AddToRoleAsync(user, "Admin");

            return user;
        }
    }
}