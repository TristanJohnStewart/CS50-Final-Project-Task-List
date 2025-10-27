using CS50TaskList.Data;
using CS50TaskList.Data.Entities;
using CS50TaskList.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CS50TaskList.Controllers
{
    public class SubtaskController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SubtaskController(ApplicationDbContext context) { _context = context; }

        // GET: SubtaskController/Create
        public ActionResult Create(int id)
        {
            var model = new SubTaskModel { ParentId = id };

            return View(model);
        }

        // POST: SubtaskController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Create(SubTaskModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return View("Error");
            }

            SubTask entity = new SubTask
            {
                Title = model.Title,
                TaskId = model.ParentId,
                Task = _context.Tasks.FirstOrDefault(x => x.Id == model.ParentId)
            };

            _context.Add(entity);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        // GET: SubtaskController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SubtaskController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction();
            }
            catch
            {
                return View();
            }
        }

        // GET: SubtaskController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SubtaskController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction();
            }
            catch
            {
                return View();
            }
        }
    }
}
