using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAll();

        Task<UserDto> GetById(string id);
    }
}