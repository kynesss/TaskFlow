using TaskFlow.Domain.Enums;

namespace TaskFlow.Domain.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public TaskPriority Priority { get; set; }
        public Enums.TaskStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = default!;
        public string? AssignedTo { get; set; }
    }
}