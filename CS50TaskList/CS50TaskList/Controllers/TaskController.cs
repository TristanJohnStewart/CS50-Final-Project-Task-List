using CS50TaskList.Models;
using CS50TaskList.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
            return View(model);
        }

        public async Task<ActionResult> SetToComplete(int id)
        {
            await _taskService.CompleteTaskAsync(id);
            return Redirect(Request.Headers["Referer"].ToString());
        }

        // GET: Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(TaskModel model)
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
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var model = await _taskService.PrepareForDeleteAsync(id);
                return View(model);
            }
            catch (Exception ex) { return View("Error", ex); }
        }

        [HttpPost]
        public async Task<ActionResult> Delete(TaskModel model)
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
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var model = await _taskService.PrepareForEditAsync(id);
                return View(model);
            }
            catch (Exception ex) { return View("Error", ex); }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(TaskModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            try
            {
                await _taskService.EditTaskAsync(model);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public async Task<ActionResult> ViewCompletedTasks()
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
