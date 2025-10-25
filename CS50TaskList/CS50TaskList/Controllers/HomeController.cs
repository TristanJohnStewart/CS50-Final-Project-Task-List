using CS50TaskList.Data;
using CS50TaskList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CS50TaskList.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        //public HomeController(ILogger<HomeController> logger) { _logger = logger; }

        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context) { _context = context; }
        
        public async System.Threading.Tasks.Task<ActionResult> Index() 
        {
            var tasks = await _context.Tasks.ToListAsync();

            if (tasks == null)
            {
                View();
            }

            var model = new List<TaskModel>();

            foreach (var task in tasks)
            {
                var item = new TaskModel();

                item.Id = task.Id;
                item.Title = task.Title;
                item.Notes = task.Notes;
                item.Deadline = task.Deadline;
                item.Recurrance = task.Recurrance;
                item.Priority = task.Priority;
                item.Position = task.Position;
                item.IsCompleted = task.IsCompleted;
                item.UserId = task.UserId;

                model.Add(item);
            }

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
