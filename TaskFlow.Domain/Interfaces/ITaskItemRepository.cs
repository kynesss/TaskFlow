using TaskFlow.Domain.Entities;

namespace TaskFlow.Domain.Interfaces
{
    public interface ITaskItemRepository
    {
        Task<IEnumerable<TaskItem>> GetAllAsync();

        Task<TaskItem> GetByIdAsync(int id);

        Task CreateAsync(TaskItem item);

        Task DeleteAsync(int id);

        Task Commit();
    }
}