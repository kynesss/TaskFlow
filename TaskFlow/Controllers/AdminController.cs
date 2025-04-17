using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Domain.Identity;

namespace TaskFlow.Web.Controllers
{
    public class AdminController : Controller
    {
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Index()
        {
            return View();
        }
    }
}