using Microsoft.AspNetCore.Identity;
using LocativeApp.Data;
using System.Security.Claims;

namespace LocativeApp.Services
{
    public class AdminService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<List<ApplicationUser>> GetAllByOwner(string ownerId)
        {
            return _userManager.Users
                .Where(u => u.OwnerId == ownerId)
                .ToList();
        }
        public async Task<List<ApplicationUser>> GetAll()
        {
            return await Task.FromResult(
                _userManager.Users
                    .Where(u => u.RoleType == "Admin")
                    .ToList()
            );
        }

        public async Task<ApplicationUser> CreateAdmin(
            string email,
            string password,
            string ownerId,
            string firstname,
            string name)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                OwnerId = ownerId,
                RoleType = "Admin",
                FirstName=firstname,
                LastName = name
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Admin");

                await _userManager.AddClaimAsync(user, new Claim("OwnerId", ownerId));
            }

            return user;
        }

        public async Task DeleteAdmin(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return;

            // optionnel : retirer rôles et claims
            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles);

            var claims = await _userManager.GetClaimsAsync(user);
            await _userManager.RemoveClaimsAsync(user, claims);

            await _userManager.DeleteAsync(user);
        }
    }
}