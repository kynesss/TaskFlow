using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskFlow.Application.TaskItem.Commands.CreateTaskItem;
using TaskFlow.Application.TaskItem.Commands.DeleteTaskItem;
using TaskFlow.Application.TaskItem.Commands.EditTaskItem;
using TaskFlow.Application.TaskItem.Queries.GetAllTaskItems;
using TaskFlow.Application.TaskItem.Queries.GetTaskItemById;
using TaskFlow.Application.User;
using TaskFlow.Application.User.Queries.GetAllUsers;
using TaskFlow.Domain.Enums;

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

        public async Task<IActionResult> Index()
        {
            var taskItems = await _mediator.Send(new GetAllTaskItemsQuery());
            return View(taskItems);
        }

        public async Task<IActionResult> Create()
        {
            await PrepareTaskFormData();
            return View();
        }

        [Route("TaskItem/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _mediator.Send(new GetTaskItemByIdQuery(id));
            var command = _mapper.Map<UpdateTaskItemCommand>(item);

            await PrepareTaskFormData();
            return View(command);
        }

        [HttpPost]
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
        public async Task<IActionResult> Update(UpdateTaskItemCommand command)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }

            return await Edit(command.Id);
        }

        [Route("TaskItem/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteTaskItemCommand(id));
            return RedirectToAction(nameof(Index));
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