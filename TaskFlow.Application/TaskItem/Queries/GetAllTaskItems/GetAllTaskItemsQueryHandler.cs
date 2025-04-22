using AutoMapper;
using MediatR;
using TaskFlow.Application.DTOs;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.TaskItem.Queries.GetAllTaskItems
{
    public class GetAllTaskItemsQueryHandler : IRequestHandler<GetAllTaskItemsQuery, IEnumerable<TaskItemDto>>
    {
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly IMapper _mapper;

        public GetAllTaskItemsQueryHandler(ITaskItemRepository taskItemRepository, IMapper mapper)
        {
            _taskItemRepository = taskItemRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskItemDto>> Handle(GetAllTaskItemsQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _taskItemRepository.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<TaskItemDto>>(tasks);

            return dtos;
        }
    }
}