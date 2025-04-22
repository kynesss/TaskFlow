using MediatR;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.TaskItem.Commands.EditTaskItem
{
    public class UpdateTaskItemCommand : TaskItemDto, IRequest
    {
    }
}