using CS50TaskList.Models;
using CS50TaskList.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CS50TaskList.Controllers
{
    [Authorize]
    public class SubtaskController : Controller
    {
        private readonly ISubTaskService _subTaskService;
        public SubtaskController(ISubTaskService subTaskService)
        {
            _subTaskService = subTaskService;
        }

        // GET: SubtaskController/Create
        public ActionResult Create(int id)
        {
            var model = new SubTaskModel { ParentId = id };
            return View(model);
        }

        // POST: SubtaskController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SubTaskModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            await _subTaskService.CreateSubTaskAsync(model);
            return RedirectToAction("Edit", "Task", model.ParentId);
        }

        // GET: SubtaskController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _subTaskService.PrepareForEditAsync(id);
            return View(model);
        }

        // POST: SubtaskController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SubTaskModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            await _subTaskService.EditSubTaskAsync(model);
            return RedirectToAction("Index", "Home");
        }

        // GET: SubtaskController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _subTaskService.PrepareForDeleteAsync(id);

            return View(model);
        }

        // POST: SubtaskController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(SubTaskModel model)
        {
            await _subTaskService.DeleteSubTaskAsync(model);

            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> SetToComplete(int id)
        {
            await _subTaskService.CompleteTaskAsync(id);
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
