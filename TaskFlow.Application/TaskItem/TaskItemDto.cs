using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.DTOs
{
    public class TaskItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public TaskPriority Priority { get; set; }
        public Domain.Enums.TaskStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = string.Empty;
        public string? CreatedByEmail { get; set; }
        public string? AssignedTo { get; set; }
        public string? AssignedToEmail { get; set; }
    }
}