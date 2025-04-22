using AutoMapper;
using MediatR;
using TaskFlow.Application.ApplicationUser;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.TaskItem.Commands.CreateTaskItem
{
    public class CreateTaskItemCommandHandler : IRequestHandler<CreateTaskItemCommand>
    {
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public CreateTaskItemCommandHandler(ITaskItemRepository taskItemRepository, IMapper mapper, IUserContext userContext)
        {
            _taskItemRepository = taskItemRepository;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<Unit> Handle(CreateTaskItemCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<Domain.Entities.TaskItem>(request);
            item.CreatedAt = DateTime.UtcNow;
            item.CreatedBy = _userContext.GetCurrentUser()!.Id;

            await _taskItemRepository.CreateAsync(item);

            return Unit.Value;
        }
    }
}