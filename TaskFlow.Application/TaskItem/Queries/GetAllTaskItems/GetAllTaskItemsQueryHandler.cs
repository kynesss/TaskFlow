using AutoMapper;
using MediatR;
using System.Runtime.CompilerServices;
using TaskFlow.Application.Extensions;
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

            var filter = request.Filter;

            if (filter is not null)
            {
                if (!string.IsNullOrEmpty(filter.AssignedTo))
                    tasks = tasks.Where(x => x.AssignedTo == filter.AssignedTo);

                if (!string.IsNullOrEmpty(filter.CreatedBy))
                    tasks = tasks.Where(x => x.CreatedBy == filter.CreatedBy);

                if (filter.Status.HasValue)
                    tasks = tasks.Where(x => x.Status == filter.Status.Value);

                if (filter.Priority.HasValue)
                    tasks = tasks.Where(x => x.Priority == filter.Priority.Value);
            }

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