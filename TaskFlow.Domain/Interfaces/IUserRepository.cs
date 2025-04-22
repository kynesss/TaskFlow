using Microsoft.AspNetCore.Identity;

namespace TaskFlow.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();

        Task<IdentityUser> GetById(string id);
    }
}