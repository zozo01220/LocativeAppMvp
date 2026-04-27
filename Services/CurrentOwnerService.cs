using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using LocativeApp.Data;

namespace LocativeApp.Services
{
    public class CurrentOwnerService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public CurrentOwnerService(
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        // =========================
        // 🔥 GET CURRENT USER ID
        // =========================
        public string? GetUserId()
        {
            return _httpContextAccessor.HttpContext?
                .User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        // =========================
        // 🔥 GET OWNER ID
        // =========================
        public async Task<string?> GetOwnerIdAsync()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null)
                return null;

            if (user.IsInRole("SuperAdmin"))
                return null; // ou "GLOBAL"

            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return null;

            var appUser = await _userManager.FindByIdAsync(userId);

            return appUser?.OwnerId;
        }
        //public async Task<string?> GetOwnerIdAsync()
        //{
        //    var userId = GetUserId();

        //    if (userId == null)
        //        return null;

        //    var user = await _userManager.FindByIdAsync(userId);

        //    return user?.OwnerId; // null = SuperAdmin
        //}

        // =========================
        // 🔥 CHECK SUPERADMIN
        // =========================
        public bool IsSuperAdmin()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            return user?.IsInRole("SuperAdmin") ?? false;
        }
    }
}