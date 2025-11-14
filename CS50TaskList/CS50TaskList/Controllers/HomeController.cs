using CS50TaskList.Data.Entities;
using CS50TaskList.Models;
using CS50TaskList.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CS50TaskList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IRepository<Data.Entities.Task> _taskRepository;
        private readonly IRepository<SubTask> _subTaskRepository;
        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, IRepository<Data.Entities.Task> taskRepository, IRepository<SubTask> subTaskRepository)
        {
            _logger = logger;
            _userManager = userManager;
            _taskRepository = taskRepository;
            _subTaskRepository = subTaskRepository;
        }

        public async Task<ActionResult> Index()
        {
            return (User.Identity.IsAuthenticated) ? RedirectToAction("Index", "Task") : View();
            //_logger.Log(LogLevel.Information, "Returning {model} to Index view", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
