using CS50TaskList.Models;
using CS50TaskList.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CS50TaskList.Controllers
{
    /// <summary>
    ///     Controller class for the Task related actions.
    /// </summary>
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<TaskController> _logger;
        /// <summary>
        ///     Initializes a new instance of the <see cref="TaskController" /> class.
        /// </summary>
        /// <param name="taskService">Service for Tasks.</param>
        /// <param name="logger">Logger instance.</param>
        public TaskController(ITaskService taskService, ILogger<TaskController> logger)
        {
            _taskService = taskService;
            _logger = logger;
        }

        /// <summary>
        ///     GET Action for the landing page / view of the user's list of tasks.
        /// </summary>
        /// <returns>A <see cref="TaskModel"/> passed through to a <see cref="Controller.View()"/>.</returns>
        public async Task<IActionResult> IndexAsync()
        {
            _logger.LogInformation("Beginning IndexAsync.");

            _logger.LogInformation("Intialising a new List<TaskModel> with all of the user's tasks.");
            var model = await _taskService.PrepareForIndexAsync();

            _logger.LogInformation("Returning model to view.");
            return View(model);
        }

        /// <summary>
        ///     Action for switching a <see cref="TaskModel.IsCompleted" /> state.
        /// </summary>
        /// <param name="id">Id of the Task.</param>
        /// <returns>The previous <see cref="Controller.View()"/>.</returns>
        public async Task<ActionResult> SetToComplete(int id)
        {
            _logger.LogInformation("Beginning SetToComplete.");

            _logger.LogInformation("Calling CompleteTaskAsync and passing id through it.");
            await _taskService.CompleteTaskAsync(id);

            _logger.LogInformation("Action Complete, redirecting to previous page.");
            return Redirect(Request.Headers["Referer"].ToString());
        }

        /// <summary>
        ///     GET Action to return the view for creating a new <see cref="TaskModel" />.
        /// </summary>
        /// <returns>A <see cref="Controller.View()"/>.</returns>
        // GET: Create
        public ActionResult Create()
        {
            _logger.LogInformation("Beginning Create (GET).");

            _logger.LogInformation("Loading view.");
            return View();
        }

        /// <summary>
        ///     POST Action to check if the model is valid for manipulation and passing a 
        ///     <see cref="TaskModel" /> through to <see cref="TaskService.CreateTaskAsync" />
        ///     before redirecting to the landing page.
        /// </summary>
        /// <param name="model">Model of the data.</param>
        /// <returns>A <see cref="Controller.View()"/>.</returns>
        [HttpPost]
        public async Task<ActionResult> Create(TaskModel model)
        {
            _logger.LogInformation("Beginning Create (POST).");

            _logger.LogInformation("Checking is ModelState is valid.");
            if (!ModelState.IsValid)
            {
                _logger.LogInformation("ModelState is not valid, redirecting to Error view.");
                return View("Error");
            }

            try
            {
                _logger.LogInformation("Creating and saving Task in Repository.");
                await _taskService.CreateTaskAsync(model);
            }
            catch (Exception ex) 
            {
                _logger.LogError("Error occured in TaskController.Create Action. Error: {ex}", ex);
                return View("Error", ex); 
            }

            _logger.LogInformation("Action Complete, redirecting to landing page.");
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        ///     GET Action to return the view for deleting a <see cref="TaskModel" />.
        /// </summary>
        /// <param name="id">Id of the Task.</param>
        /// <returns>A <see cref="TaskModel"/> passed through to a <see cref="Controller.View()"/>.</returns>
        // GET: Delete
        public async Task<ActionResult> Delete(int id)
        {
            _logger.LogInformation("Beginning Delete (GET).");

            try
            {
                _logger.LogInformation("Intialising a new TaskModel.");
                var model = await _taskService.PrepareForDeleteAsync(id);

                _logger.LogInformation("Returning model to view.");
                return View(model);
            }
            catch (Exception ex) 
            {
                _logger.LogError("Error occured in TaskController.Create Action. Error: {ex}", ex); 
                return View("Error", ex); 
            }
        }

        /// <summary>
        ///     POST Action to delete the passed in <see cref="TaskModel" /> from the db.
        /// </summary>
        /// <param name="model">Model of the data.</param>
        /// <returns>A <see cref="Controller.View()"/>.</returns>
        [HttpPost]
        public async Task<ActionResult> Delete(TaskModel model)
        {
            _logger.LogInformation("Beginning Delete (POST).");

            _logger.LogInformation("Checking if ModelState is valid.");
            if (!ModelState.IsValid)
            {
                _logger.LogInformation("ModelState is not valid, redirecting to Error view.");
                return View("Error");
            }

            try
            {
                _logger.LogInformation("Deleting Task #{model.Id} from Repository.", model.Id);
                await _taskService.DeleteTaskAsync(model);

                _logger.LogInformation("Action complete, redirecting to landing page.");
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex) 
            {
                _logger.LogError("Error occured in TaskController.Create Action. Error: {ex}", ex);
                return View("Error", ex); 
            }
        }

        /// <summary>
        ///     GET Action to return the view for editing a <see cref="TaskModel" />.
        /// </summary>
        /// <param name="id">Id of the Task.</param>
        /// <returns>A <see cref="TaskModel"/> passed through to a <see cref="Controller.View()"/>.</returns>
        // GET: Edit
        public async Task<ActionResult> Edit(int id)
        {
            _logger.LogInformation("Beginning Edit (GET).");

            try
            {
                _logger.LogInformation("Intialising a new TaskModel.");
                var model = await _taskService.PrepareForEditAsync(id);

                _logger.LogInformation("Returning model to view.");
                return View(model);
            }
            catch (Exception ex) 
            {
                _logger.LogError("Error occured in TaskController.Create Action. Error: {ex}", ex);
                return View("Error", ex); 
            }
        }

        /// <summary>
        ///     POST Action to edit the passed in <see cref="TaskModel" /> in the db.
        /// </summary>
        /// <param name="model">Model of the data.</param>
        /// <returns>A <see cref="Controller.View()"/>.</returns>
        [HttpPost]
        public async Task<ActionResult> Edit(TaskModel model)
        {
            _logger.LogInformation("Beginning Edit (POST).");

            _logger.LogInformation("Checking if ModelState is valid.");
            if (!ModelState.IsValid)
            {
                _logger.LogInformation("ModelState is not valid, redirecting to Error view.");
                return View("Error");
            }

            try
            {
                _logger.LogInformation("Editing Task #{model.Id} in the Repository.", model.Id);
                await _taskService.EditTaskAsync(model);

                _logger.LogInformation("Action complete, redirecting to landing page.");
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                _logger.LogError("Error occured in TaskController.Create Action.");
                return View("Error");
            }
        }

        /// <summary>
        ///     GET Action to return the view for viewing <see cref="TaskModel.IsCompleted" /> that equal True.
        /// </summary>
        /// <returns>A <see cref="TaskModel"/> passed through to a <see cref="Controller.View()"/>.</returns>
        public async Task<ActionResult> ViewCompletedTasks()
        {
            _logger.LogInformation("Beginning ViewCompletedTasks (GET).");
            try 
            {
                _logger.LogInformation("Intialising a new List<TaskModel>.");
                var model = await _taskService.PrepareForViewCompleted();

                _logger.LogInformation("Returning model to view.");
                return View(model);
            }
            catch (Exception)
            {
                _logger.LogError("Error occured in TaskController.Create Action.");
                return View("Error");
            }            
        }
    }
}
