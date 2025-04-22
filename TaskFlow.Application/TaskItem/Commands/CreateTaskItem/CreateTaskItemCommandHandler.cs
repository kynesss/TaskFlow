using AutoMapper;
using MediatR;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.TaskItem.Commands.CreateTaskItem
{
    public class CreateTaskItemCommandHandler : IRequestHandler<CreateTaskItemCommand>
    {
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly IMapper _mapper;

        public CreateTaskItemCommandHandler(ITaskItemRepository taskItemRepository, IMapper mapper)
        {
            _taskItemRepository = taskItemRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateTaskItemCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<Domain.Entities.TaskItem>(request);
            await _taskItemRepository.CreateAsync(item);

            return Unit.Value;
        }
    }
}