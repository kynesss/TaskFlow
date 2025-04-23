using MediatR;

namespace TaskFlow.Application.TaskItem.Queries.GetTaskItemById
{
    public class GetTaskItemByIdQuery : IRequest<TaskItemDto>
    {
        public int Id { get; }

        public GetTaskItemByIdQuery(int id)
        {
            Id = id;
        }
    }
}