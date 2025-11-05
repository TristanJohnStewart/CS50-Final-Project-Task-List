using CS50TaskList.Data;
using CS50TaskList.Models;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;
        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger) 
        { 
            _context = context; 
            _logger = logger; 
        }
        
        public async Task<ActionResult> Index() 
        {
            _logger.Log(LogLevel.Information, "Entering the Index action");

            var tasks = await _context.Tasks.ToListAsync();

            if (tasks == null)
            {
                return View();
            }

            var model = new List<TaskModel>();

            foreach (var task in tasks)
            {
                var item = new TaskModel
                {
                    Id = task.Id,
                    Title = task.Title,
                    Notes = task.Notes,
                    Deadline = task.Deadline,
                    Recurrance = task.Recurrance,
                    Priority = task.Priority,
                    Position = task.Position,
                    IsCompleted = task.IsCompleted,
                    UserId = task.UserId
                };

                var subtasks = await _context.SubTasks
                    .Where(x => x.TaskId == item.Id)
                    .ToListAsync();

                if (subtasks != null)
                {
                    var subModel = new List<SubTaskModel>();
                    foreach (var subtask in subtasks)
                    {
                        var subItem = new SubTaskModel {
                            Id = subtask.Id,
                            Title = subtask.Title,
                            Position = subtask.Position,
                            IsCompleted = subtask.IsCompleted,
                            ParentId = subtask.TaskId
                        };

                        subModel.Add(subItem);
                    }
                    item.SubTasks = subModel;
                }
                
                model.Add(item);
            }

            _logger.Log(LogLevel.Information, "Returning {model} to Index view", model);
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
