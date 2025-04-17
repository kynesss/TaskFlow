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
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = default!;
        public string? AssignedTo { get; set; }
    }
}