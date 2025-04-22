using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskFlow.Application.TaskItem.Commands.CreateTaskItem;
using TaskFlow.Application.TaskItem.Commands.DeleteTaskItem;
using TaskFlow.Application.TaskItem.Queries.GetAllTaskItems;
using TaskFlow.Application.User;
using TaskFlow.Application.User.Queries.GetAllUsers;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Web.Controllers
{
    public class TaskItemController : Controller
    {
        private readonly IMediator _mediator;

        public TaskItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var taskItems = await _mediator.Send(new GetAllTaskItemsQuery());
            return View(taskItems);
        }

        public async Task<IActionResult> Create()
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

            return View();
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

        [Route("TaskItem/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteTaskItemCommand(id));
            return RedirectToAction(nameof(Index));
        }
    }
}