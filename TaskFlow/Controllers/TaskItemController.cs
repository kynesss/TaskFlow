using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskFlow.Application.DTOs;
using TaskFlow.Application.Interfaces;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Web.Controllers
{
    public class TaskItemController : Controller
    {
        private readonly ITaskItemService _taskItemService;
        private readonly IUserService _userService;

        public TaskItemController(ITaskItemService taskItemService, IUserService userService)
        {
            _taskItemService = taskItemService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var taskItems = await _taskItemService.GetAllAsync();
            return View(taskItems);
        }

        public async Task<IActionResult> Create()
        {
            var users = await _userService.GetAll();

            ViewBag.Users = new SelectList(users, "Id", "Email");
            ViewBag.Priorities = new SelectList(Enum.GetValues<TaskPriority>());
            ViewBag.Statuses = new SelectList(Enum.GetValues<Domain.Enums.TaskStatus>());

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskItemDto dto)
        {
            if (ModelState.IsValid)
            {
                var itemId = await _taskItemService.CreateAsync(dto);
                return RedirectToAction(nameof(Index));
            }

            return await Create();
        }
    }
}