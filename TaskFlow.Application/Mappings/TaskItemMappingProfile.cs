using AutoMapper;
using TaskFlow.Application.DTOs;
using TaskFlow.Application.TaskItem.Commands.CreateTaskItem;
using TaskFlow.Application.TaskItem.Commands.EditTaskItem;

namespace TaskFlow.Application.Mappings
{
    public class TaskItemMappingProfile : Profile
    {
        public TaskItemMappingProfile()
        {
            CreateMap<TaskItemDto, Domain.Entities.TaskItem>()
                .ReverseMap();

            CreateMap<CreateTaskItemCommand, Domain.Entities.TaskItem>();
            CreateMap<TaskItemDto, UpdateTaskItemCommand>();
        }
    }
}