using MediatR;

namespace TaskFlow.Application.TaskItem.Commands.DeleteTaskItem
{
    public class DeleteTaskItemCommand : IRequest
    {
        public int Id { get; }

        public DeleteTaskItemCommand(int id)
        {
            Id = id;
        }
    }
}