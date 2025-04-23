using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.TaskItem.Filters
{
    public class TaskItemFilterDto
    {
        public string? CreatedBy { get; set; }
        public string? AssignedTo { get; set; }
        public TaskPriority? Priority { get; set; }
        public Domain.Enums.TaskStatus? Status { get; set; }
        public SortDirection? SortByCreatedAt { get; set; }
    }
}