using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.ApplicationUser;
using TaskFlow.Application.DTOs;
using TaskFlow.Application.Interfaces;
using TaskFlow.Domain.Entities;
using TaskFlow.Infrastructure.Persistence;

namespace TaskFlow.Infrastructure.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public TaskItemService(ApplicationDbContext dbContext, IMapper mapper, IUserContext userContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<int> CreateAsync(TaskItemDto dto)
        {
            var item = _mapper.Map<TaskItem>(dto);

            item.CreatedAt = DateTime.UtcNow;
            item.CreatedBy = _userContext.GetCurrentUser()!.Id;

            _dbContext.TaskItems.Add(item);
            await _dbContext.SaveChangesAsync();

            return item.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _dbContext.TaskItems.FirstAsync(x => x.Id == id);
            _dbContext.TaskItems.Remove(item);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TaskItemDto>> GetAllAsync()
        {
            var items = await _dbContext.TaskItems.ToListAsync();
            var dtos = _mapper.Map<IEnumerable<TaskItemDto>>(items);

            return dtos;
        }

        public async Task<TaskItemDto> GetByIdAsync(int id)
        {
            var item = await _dbContext.TaskItems.FirstOrDefaultAsync(x => x.Id == id) ?? throw new NullReferenceException($"TaskItem with ID {id} not found.");
            var dto = _mapper.Map<TaskItemDto>(item);
            return dto;
        }

        public async Task UpdateAsync(TaskItemDto dto)
        {
            var entity = await _dbContext.TaskItems.FirstAsync(x => x.Id == dto.Id);

            entity.Description = dto.Description;
            entity.AssignedTo = dto.AssignedTo;
            entity.Title = dto.Title;
            entity.Status = dto.Status;
            entity.Priority = dto.Priority;

            await _dbContext.SaveChangesAsync();
        }
    }
}