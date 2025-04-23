using MediatR;
using TaskFlow.Application.TaskItem.Filters;

namespace TaskFlow.Application.TaskItem.Queries.GetAllTaskItems
{
    public class GetAllTaskItemsQuery : IRequest<IEnumerable<TaskItemDto>>
    {
        public TaskItemFilterDto? Filter { get; }

        public GetAllTaskItemsQuery(TaskItemFilterDto? filter = null)
        {
            Filter = filter;
        }
    }
}