using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.Interfaces
{
    public interface ITaskItemService
    {
        Task<IEnumerable<TaskItemDto>> GetAllAsync();

        Task<TaskItemDto> GetByIdAsync(int id);

        Task<int> CreateAsync(TaskItemDto dto);

        Task UpdateAsync(TaskItemDto dto);

        Task DeleteAsync(int id);
    }
}