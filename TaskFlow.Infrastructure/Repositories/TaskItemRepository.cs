using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.ApplicationUser;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;
using TaskFlow.Infrastructure.Persistence;

namespace TaskFlow.Infrastructure.Repositories
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserContext _userContext;

        public TaskItemRepository(ApplicationDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }

        public async Task CreateAsync(TaskItem item)
        {
            item.CreatedAt = DateTime.UtcNow;
            item.CreatedBy = _userContext.GetCurrentUser()!.Id;

            _dbContext.TaskItems.Add(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _dbContext.TaskItems.FirstAsync(x => x.Id == id);
            _dbContext.TaskItems.Remove(item);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _dbContext.TaskItems.ToListAsync();
        }

        public async Task<TaskItem> GetByIdAsync(int id)
        {
            return await _dbContext.TaskItems.FirstAsync(x => x.Id == id);
        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}