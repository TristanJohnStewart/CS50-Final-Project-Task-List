using CS50TaskList.Models;
using CS50TaskList.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CS50TaskList.Controllers
{
    /// <summary>
    ///     Controller class for the Subtask related actions.
    /// </summary>
    [Authorize]
    public class SubtaskController : Controller
    {
        private readonly ISubTaskService _subTaskService;
        private readonly ILogger<SubtaskController> _logger;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SubtaskController" /> class.
        /// </summary>
        /// <param name="subTaskService">Service for Subtasks.</param>
        /// <param name="logger">Logger instance.</param>
        public SubtaskController(ISubTaskService subTaskService, ILogger<SubtaskController> logger)
        {
            _subTaskService = subTaskService;
            _logger = logger;
        }

        /// <summary>
        ///     GET Action to return the view for creating a new <see cref="SubTaskModel" /> for a <see cref="TaskModel" />.
        /// </summary>
        /// <param name="id">Id of the parent Task.</param>
        /// <returns>A <see cref="SubTaskModel"/> passed through to a <see cref="Controller.View()"/>.</returns>
        // GET: SubtaskController/Create
        public ActionResult Create(int id)
        {
            _logger.LogInformation("Intialising a new SubTaskModel and assigning ParentId as {id} .", id);
            var model = new SubTaskModel { ParentId = id };

            _logger.LogInformation("Returning model to view.");
            return View(model);
        }

        /// <summary>
        ///     POST Action to check if the model is valid for manipulation and passing a 
        ///     <see cref="SubTaskModel" /> through to <see cref="SubTaskService.CreateSubTaskAsync" />
        ///     before redirecting to the edit page for the parent task.
        /// </summary>
        /// <param name="model">Model of the data.</param>
        /// <returns>A <see cref="SubTaskModel.ParentId"/> passed through to a <see cref="TaskController.Edit(int)"/>.</returns>
        // POST: SubtaskController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SubTaskModel model)
        {
            _logger.LogInformation("Checking is ModelState is valid.");
            if (!ModelState.IsValid)
            {
                _logger.LogInformation("ModelState is not valid, redirecting to Error view.");
                return View("Error");
            }

            _logger.LogInformation("Creating and saving Subtask in Repository.");
            await _subTaskService.CreateSubTaskAsync(model);

            _logger.LogInformation("Action Complete, redirecting to edit view for parent Task #{model.ParentIdId}.", model.ParentId);
            return RedirectToAction("Edit", "Task", new {id = model.ParentId});
        }

        /// <summary>
        ///     Get Action to return the view for editing a <see cref="SubTaskModel" /> for a <see cref="TaskModel" />.
        /// </summary>
        /// <param name="id">Id of the Subtask.</param>
        /// <returns>A <see cref="SubTaskModel"/> passed through to a <see cref="Controller.View()"/>.</returns>
        // GET: SubtaskController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            _logger.LogInformation("Intialising a new SubTaskModel and assigning it with the model returned from PrepareForEditAsync.");
            var model = await _subTaskService.PrepareForEditAsync(id);

            _logger.LogInformation("Returning model to view.");
            return View(model);
        }

        /// <summary>
        ///     POST Action to check if the model is valid for manipulation and passing a 
        ///     <see cref="SubTaskModel" /> through to <see cref="SubTaskService.EditSubTaskAsync" />
        ///     before redirecting to the landing page.
        /// </summary>
        /// <param name="model">Model of the data.</param>
        /// <returns>A <see cref="Controller.View()"/>.</returns>
        // POST: SubtaskController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SubTaskModel model)
        {
            _logger.LogInformation("Checking is ModelState is valid.");
            if (!ModelState.IsValid)
            {
                _logger.LogInformation("ModelState is not valid, redirecting to Error view.");
                return View("Error");
            }

            _logger.LogInformation("Editing and saving the Subtask in Repository.");
            await _subTaskService.EditSubTaskAsync(model);

            _logger.LogInformation("Action Complete, redirecting to landing page.");
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        ///     Get Action to return the view for deleting a <see cref="SubTaskModel" /> for a <see cref="TaskModel" />.
        /// </summary>
        /// <param name="id">Id of the Subtask.</param>
        /// <returns>A <see cref="SubTaskModel"/> passed through to a <see cref="Controller.View()"/>.</returns>
        // GET: SubtaskController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            _logger.LogInformation("Intialising a new SubTaskModel and assigning it with the model returned from PrepareForDeleteAsync.");
            var model = await _subTaskService.PrepareForDeleteAsync(id);

            _logger.LogInformation("Returning model to view.");
            return View(model);
        }

        /// <summary>
        ///     POST Action to passing a <see cref="SubTaskModel" /> through to 
        ///     <see cref="SubTaskService.DeleteSubTaskAsync" /> before redirecting to the landing page.
        /// </summary>
        /// <param name="model">Model of the data.</param>
        /// <returns>A <see cref="Controller.View()"/>.</returns>
        // POST: SubtaskController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(SubTaskModel model)
        {
            _logger.LogInformation("Calling DeleteSubTaskAsync and passing model through it.");
            await _subTaskService.DeleteSubTaskAsync(model);

            _logger.LogInformation("Action Complete, redirecting to landing page.");
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        ///     Action for switching a <see cref="SubTaskModel.IsCompleted" /> state.
        /// </summary>
        /// <param name="id">Id of the Subtask.</param>
        /// <returns>The previous <see cref="Controller.View()"/>.</returns>
        public async Task<ActionResult> SetToComplete(int id)
        {
            _logger.LogInformation("Calling CompleteTaskAsync and passing id through it.");
            await _subTaskService.CompleteTaskAsync(id);

            _logger.LogInformation("Action Complete, redirecting to previous page.");
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
