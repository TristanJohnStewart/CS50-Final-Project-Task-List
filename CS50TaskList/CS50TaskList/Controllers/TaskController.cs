using CS50TaskList.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CS50TaskList.Controllers
{
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TaskController(ApplicationDbContext context) { _context = context; }

        // GET: Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: Delete
        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        { 
            var task = _context.Tasks.FirstOrDefault(x => x.Id == id);
            _context.Tasks.Remove(task);
            return View(); 
        }

        // GET: Edit
        public ActionResult Edit()
        {
            return View();
        }
    }
}
