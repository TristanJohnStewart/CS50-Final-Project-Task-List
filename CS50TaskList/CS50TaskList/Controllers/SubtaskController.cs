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
            var entity = _context.SubTasks.FirstOrDefault(x => x.Id == id);

            if (entity == null)
            {
                return View("Error");
            }

            var model = new SubTaskModel
            {
                Id = id,
                Title = entity.Title,
                Position = entity.Position,
                IsCompleted = entity.IsCompleted,
                ParentId = entity.TaskId
            };

            return View(model);
        }

        // POST: SubtaskController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Edit(SubTaskModel model)
        {   
            if (model == null || !ModelState.IsValid)
            {
                return View("Error");
            }

            try
            {
                var entity = _context.SubTasks.FirstOrDefault(x => x.Id == model.Id);
                entity.Id = model.Id;
                entity.Title = model.Title;
                entity.Position = model.Position;
                entity.IsCompleted = model.IsCompleted;
                entity.TaskId = model.ParentId;
                entity.Task = _context.Tasks.FirstOrDefault(x => x.Id == model.ParentId);

                _context.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return View("Error");
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: SubtaskController/Delete/5
        public ActionResult Delete(int id)
        {
            var entity = _context.SubTasks.FirstOrDefault(x => x.Id == id);

            if (entity == null)
            {
                return View("Error");
            }

            var model = new SubTaskModel
            {
                Id = id,
                Title = entity.Title,
                Position = entity.Position,
                IsCompleted = entity.IsCompleted,
                ParentId = entity.TaskId
            };

            return View(model);
        }

        // POST: SubtaskController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Delete(SubTaskModel model)
        {
            var entity = _context.SubTasks.FirstOrDefault(x => x.Id == model.Id);
            _context.SubTasks.Remove(entity);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
