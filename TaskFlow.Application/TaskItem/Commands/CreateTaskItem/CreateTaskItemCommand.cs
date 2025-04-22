using MediatR;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.TaskItem.Commands.CreateTaskItem
{
    public class CreateTaskItemCommand : TaskItemDto, IRequest
    {
    }
}