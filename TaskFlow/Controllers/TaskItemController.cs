using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using TaskFlow.Application.TaskItem.Commands.CreateTaskItem;
using TaskFlow.Application.TaskItem.Commands.DeleteTaskItem;
using TaskFlow.Application.TaskItem.Commands.EditTaskItem;
using TaskFlow.Application.TaskItem.Filters;
using TaskFlow.Application.TaskItem.Queries.GetAllTaskItems;
using TaskFlow.Application.TaskItem.Queries.GetTaskItemById;
using TaskFlow.Application.User;
using TaskFlow.Application.User.Queries.GetAllUsers;
using TaskFlow.Domain.Enums;
using TaskFlow.Domain.Identity;

namespace TaskFlow.Web.Controllers
{
    public class TaskItemController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TaskItemController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index([FromQuery] TaskItemFilterDto filter)
        {
            var query = new GetAllTaskItemsQuery(filter);
            var taskItems = await _mediator.Send(query);

            ViewBag.Users = await _mediator.Send(new GetAllUsersQuery());

            return View(taskItems);
        }

        [Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> Create()
        {
            await PrepareTaskFormData();
            return View();
        }

        [Authorize(Roles = Roles.Manager)]
        [Route("TaskItem/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _mediator.Send(new GetTaskItemByIdQuery(id));
            var command = _mapper.Map<UpdateTaskItemCommand>(item);

            await PrepareTaskFormData();
            return View(command);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> Create(CreateTaskItemCommand command)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }

            return await Create();
        }

        [HttpPost]
        [Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> Update(UpdateTaskItemCommand command)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }

            return await Edit(command.Id);
        }

        [Authorize(Roles = Roles.Admin)]
        [Route("TaskItem/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteTaskItemCommand(id));
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> LoadFilterPartial([FromQuery] FilterType filter)
        {
            switch (filter)
            {
                case FilterType.AssignedTo:
                    return PartialView("_FilterByAssignedTo", await _mediator.Send(new GetAllUsersQuery()));

                case FilterType.CreatedBy:
                    return PartialView("_FilterByCreatedBy", await _mediator.Send(new GetAllUsersQuery()));

                case FilterType.Status:
                    var statuses = Enum.GetValues<Domain.Enums.TaskStatus>();
                    return PartialView("_FilterByStatuses", statuses);

                case FilterType.Priority:
                    var priorities = Enum.GetValues<TaskPriority>();
                    return PartialView("_FilterByPriorities", priorities);

                default:
                    return Content("");
            }
        }

        private async Task PrepareTaskFormData()
        {
            var users = (await _mediator.Send(new GetAllUsersQuery())).ToList();
            users.Insert(0, new UserDto()
            {
                Id = "",
                Email = "None",
            });

            ViewBag.Users = new SelectList(users, "Id", "Email");
            ViewBag.Priorities = new SelectList(Enum.GetValues<TaskPriority>());
            ViewBag.Statuses = new SelectList(Enum.GetValues<Domain.Enums.TaskStatus>());
        }
    }
}