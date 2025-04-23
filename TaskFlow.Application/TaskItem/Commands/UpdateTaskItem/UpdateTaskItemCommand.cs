using MediatR;

namespace TaskFlow.Application.TaskItem.Commands.EditTaskItem
{
    public class UpdateTaskItemCommand : TaskItemDto, IRequest
    {
    }
}