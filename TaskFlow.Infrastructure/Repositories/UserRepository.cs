using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserRepository(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<IdentityUser> GetById(string id)
        {
            return await _userManager.FindByIdAsync(id)!;
        }
    }
}