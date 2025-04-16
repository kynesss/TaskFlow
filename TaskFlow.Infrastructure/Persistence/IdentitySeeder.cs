using Microsoft.AspNetCore.Identity;
using TaskFlow.Domain.Identity;

namespace TaskFlow.Infrastructure.Persistence
{
    public class IdentitySeeder
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentitySeeder(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            await CreateRoleIfNotExistsAsync(Roles.Admin);
            await CreateRoleIfNotExistsAsync(Roles.Manager);
            await CreateRoleIfNotExistsAsync(Roles.User);
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