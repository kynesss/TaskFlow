using MediatR;

namespace TaskFlow.Application.TaskItem.Commands.CreateTaskItem
{
    public class CreateTaskItemCommand : TaskItemDto, IRequest
    {
    }
}