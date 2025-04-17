using Microsoft.AspNetCore.Identity;
using TaskFlow.Domain.Identity;

namespace TaskFlow.Infrastructure.Persistence
{
    public class IdentitySeeder
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public IdentitySeeder(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            await CreateRoleIfNotExistsAsync(Roles.Admin);
            await CreateRoleIfNotExistsAsync(Roles.Manager);
            await CreateRoleIfNotExistsAsync(Roles.User);

            var adminExists = await _userManager.FindByEmailAsync("admin@taskflow.com") != null;
            if (adminExists)
                return;

            var admin = new IdentityUser()
            {
                UserName = "Admin",
                Email = "admin@taskflow.com"
            };

            await _userManager.CreateAsync(admin, "Admin123!");
            await _userManager.AddToRoleAsync(admin, Roles.Admin);
        }

        private async Task CreateRoleIfNotExistsAsync(string roleName)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);

            if (roleExists)
                return;

            await _roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}