using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TaskFlow.Application.DTOs;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.TaskItem.Queries.GetAllTaskItems
{
    public class GetAllTaskItemsQueryHandler : IRequestHandler<GetAllTaskItemsQuery, IEnumerable<TaskItemDto>>
    {
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GetAllTaskItemsQueryHandler(ITaskItemRepository taskItemRepository, IMapper mapper, IUserRepository userRepository)
        {
            _taskItemRepository = taskItemRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<TaskItemDto>> Handle(GetAllTaskItemsQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _taskItemRepository.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<TaskItemDto>>(tasks);

            foreach (var dto in dtos)
            {
                var author = await _userRepository.GetById(dto.CreatedBy);
                dto.CreatedByEmail = author.Email;

                if (string.IsNullOrEmpty(dto.AssignedTo))
                    continue;

                var assigned = await _userRepository.GetById(dto.AssignedTo);
                dto.AssignedToEmail = assigned.Email;
            }

            return dtos;
        }
    }
}