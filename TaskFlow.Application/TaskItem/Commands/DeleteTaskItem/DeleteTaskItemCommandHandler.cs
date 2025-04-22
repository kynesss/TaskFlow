using AutoMapper;
using MediatR;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.TaskItem.Commands.DeleteTaskItem
{
    public class DeleteTaskItemCommandHandler : IRequestHandler<DeleteTaskItemCommand>
    {
        private readonly ITaskItemRepository _taskItemRepository;

        public DeleteTaskItemCommandHandler(ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        public async Task<Unit> Handle(DeleteTaskItemCommand request, CancellationToken cancellationToken)
        {
            await _taskItemRepository.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}