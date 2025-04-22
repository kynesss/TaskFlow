using MediatR;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.TaskItem.Queries.GetAllTaskItems
{
    public class GetAllTaskItemsQuery : IRequest<IEnumerable<TaskItemDto>>
    {
    }
}