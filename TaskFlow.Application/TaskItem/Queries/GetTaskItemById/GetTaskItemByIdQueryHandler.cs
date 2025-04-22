using AutoMapper;
using MediatR;
using TaskFlow.Application.DTOs;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.TaskItem.Queries.GetTaskItemById
{
    public class GetTaskItemByIdQueryHandler : IRequestHandler<GetTaskItemByIdQuery, TaskItemDto>
    {
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly IMapper _mapper;

        public GetTaskItemByIdQueryHandler(ITaskItemRepository taskItemRepository, IMapper mapper)
        {
            _taskItemRepository = taskItemRepository;
            _mapper = mapper;
        }

        public async Task<TaskItemDto> Handle(GetTaskItemByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _taskItemRepository.GetByIdAsync(request.Id);
            var dto = _mapper.Map<TaskItemDto>(item);

            return dto;
        }
    }
}