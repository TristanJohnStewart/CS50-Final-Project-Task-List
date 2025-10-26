using AspNetCoreGeneratedDocument;
using CS50TaskList.Data;
using CS50TaskList.Data.Entities;
using CS50TaskList.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> CreateAsync(TaskModel model)
        {
            if (model == null)
            {
                return View("Error");
            }

            if (ModelState.IsValid)
            {
                CS50TaskList.Data.Entities.Task entity = new CS50TaskList.Data.Entities.Task();
                entity.Id = model.Id;
                entity.Title = model.Title;
                entity.Notes = model.Notes;
                entity.Deadline = model.Deadline;
                entity.Recurrance = model.Recurrance;
                entity.Priority = model.Priority;
                entity.Position = model.Position;
                entity.IsCompleted = model.IsCompleted;
                entity.UserId = model.UserId;

                _context.Add(entity);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult CreateSubTask(TaskModel model)
        {
            if (model == null)
            {
                model.SubTasks = new List<SubTaskModel>() { };
            }
            model.SubTasks.Add(new SubTaskModel());
            return RedirectToAction("Create", model);
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
        public ActionResult Edit(int id)
        {
            var task = _context.Tasks.FirstOrDefault(x => x.Id == id);

            if (task == null)
            {
                return View("Error");
            }

            TaskModel model = new TaskModel();
            model.Id = task.Id;
            model.Title = task.Title;
            model.Notes = task.Notes;
            model.Deadline = task.Deadline;
            model.Recurrance = task.Recurrance;
            model.Priority = task.Priority;
            model.Position = task.Position;
            model.IsCompleted = task.IsCompleted;
            model.UserId = task.UserId;

            return View(model);
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Edit(TaskModel model)
        {
            if (model == null)
            {
                return View("Error");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var entity = _context.Tasks.FirstOrDefault(x => x.Id == model.Id);
                    entity.Title = model.Title;
                    entity.Notes = model.Notes;
                    entity.Deadline = model.Deadline;
                    entity.Recurrance = model.Recurrance;
                    entity.Priority = model.Priority;
                    entity.Position = model.Position;
                    entity.IsCompleted = model.IsCompleted;
                    entity.UserId = model.UserId;

                    _context.Update(entity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return View("Error");
                }
            }
            
            return RedirectToAction("Index", "Home");
        }
    }
}
