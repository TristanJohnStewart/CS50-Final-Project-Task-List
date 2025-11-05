using CS50TaskList.Data;
using CS50TaskList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CS50TaskList.Controllers
{
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TaskController(ApplicationDbContext context) { _context = context; }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> SetToComplete([FromForm]int id, [FromForm]bool isCompleted) 
        {
            var entity = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);

            entity.IsCompleted = isCompleted;

            return Ok();
        }
        // GET: Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> CreateAsync(TaskModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            Data.Entities.Task entity = new Data.Entities.Task
            {
                Id = model.Id,
                Title = model.Title,
                Notes = model.Notes,
                Deadline = model.Deadline,
                Recurrance = model.Recurrance,
                Priority = model.Priority,
                Position = model.Position,
                IsCompleted = model.IsCompleted,
                UserId = model.UserId
            };

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        // GET: Delete
        public ActionResult Delete(int id)
        {
            var task = _context.Tasks.FirstOrDefault(x => x.Id == id);

            if (task == null)
            {
                return View("Error");
            }

            TaskModel model = new TaskModel
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

            return View(model);
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Delete(TaskModel model)
        { 
            var task = _context.Tasks.FirstOrDefault(x => x.Id == model.Id);
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home"); 
        }

        // GET: Edit
        public ActionResult Edit(int id)
        {
            var task = _context.Tasks.FirstOrDefault(x => x.Id == id);

            if (task == null)
            {
                return View("Error");
            }

            TaskModel model = new TaskModel
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

            return View(model);
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Edit(TaskModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }


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
            catch (Exception)
            {
                return View("Error");
            }
            
            return RedirectToAction("Index", "Home");
        }
    }
}
