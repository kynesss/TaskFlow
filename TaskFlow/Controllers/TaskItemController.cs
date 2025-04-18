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

        public TaskItemController(ITaskItemService taskItemService)
        {
            _taskItemService = taskItemService;
        }

        public async Task<IActionResult> Index()
        {
            var taskItems = await _taskItemService.GetAllAsync();
            return View(taskItems);
        }

        public IActionResult Create()
        {
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

            return Create();
        }
    }
}