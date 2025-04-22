using MediatR;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.TaskItem.Commands.EditTaskItem
{
    public class UpdateTaskItemCommandHandler : IRequestHandler<UpdateTaskItemCommand>
    {
        private readonly ITaskItemRepository _taskItemRepository;

        public UpdateTaskItemCommandHandler(ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        public async Task<Unit> Handle(UpdateTaskItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _taskItemRepository.GetByIdAsync(request.Id);

            item.Description = request.Description;
            item.Priority = request.Priority;
            item.Status = request.Status;
            item.AssignedTo = request.AssignedTo;
            item.Title = request.Title;

            await _taskItemRepository.Commit();

            return Unit.Value;
        }
    }
}