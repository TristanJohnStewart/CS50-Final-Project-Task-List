using CS50TaskList.Data.Entities;
using CS50TaskList.Models;
using CS50TaskList.Repositories;
using CS50TaskList.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CS50TaskList.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var model = await _taskService.PrepareForIndexAsync();
            // _logger.Log(LogLevel.Information, "Returning {model} to Index view", model);
            return View(model);
        }

        public async System.Threading.Tasks.Task<ActionResult> SetToComplete(int id)
        {
            await _taskService.CompleteTaskAsync(id);
            return Redirect(Request.Headers["Referer"].ToString());
                //RedirectToAction("Index", "Task");
        }

        // GET: Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Create(TaskModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            try
            {
                await _taskService.CreateTaskAsync(model);
            }
            catch (Exception ex) { return View("Error", ex); }

            return RedirectToAction("Index", "Home");
        }

        // GET: Delete
        public async System.Threading.Tasks.Task<ActionResult> Delete(int id)
        {
            try
            {
                var model = _taskService.PrepareForDeleteAsync(id);
                return View(model);
            }
            catch (Exception ex) { return View("Error", ex); }
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Delete(TaskModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            try
            {
                await _taskService.DeleteTaskAsync(model);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex) { return View("Error", ex); }
        }

        // GET: Edit
        public async System.Threading.Tasks.Task<ActionResult> Edit(int id)
        {
            try
            {
                var model = await _taskService.PrepareForEditAsync(id);
                // model.Date = model.Deadline.HasValue ? model.Deadline.Value.Date : new DateOnly(); ;
                return View(model);
            }
            catch (Exception ex) { return View("Error", ex); }
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
                // model.Deadline = DateTime.Parse(model.Date.ToString() + model.Time.ToString());
                await _taskService.EditTaskAsync(model);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public async System.Threading.Tasks.Task<ActionResult> ViewCompletedTasks()
        {
            try 
            {
                var model = await _taskService.PrepareForViewCompleted();
                return View(model);
            }
            catch (Exception)
            {
                return View("Error");
            }            
        }
    }
}
