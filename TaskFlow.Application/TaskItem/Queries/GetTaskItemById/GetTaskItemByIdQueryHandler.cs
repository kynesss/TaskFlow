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
        private readonly IUserRepository _userRepository;

        public GetTaskItemByIdQueryHandler(ITaskItemRepository taskItemRepository, IMapper mapper, IUserRepository userRepository)
        {
            _taskItemRepository = taskItemRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<TaskItemDto> Handle(GetTaskItemByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _taskItemRepository.GetByIdAsync(request.Id);
            var dto = _mapper.Map<TaskItemDto>(item);

            var author = await _userRepository.GetById(dto.CreatedBy);
            dto.CreatedByEmail = author.Email;

            if (!string.IsNullOrEmpty(dto.AssignedTo))
            {
                var assigned = await _userRepository.GetById(dto.AssignedTo);
                dto.AssignedToEmail = assigned.Email;
            }

            return dto;
        }
    }
}