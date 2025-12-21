using CS50TaskList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CS50TaskList.Controllers
{
    /// <summary>
    ///     Controller class for the landing and error pages.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        /// <param name="logger">Logger instance.</param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        ///     Action to direct a user to the correct action/view based on their login status.
        /// </summary>
        /// <returns>A <see cref="Controller.View"/>.</returns>
        public IActionResult Index()
        {
            _logger.LogInformation("Checking if the user is logged in or not.");
            if (User.Identity.IsAuthenticated)
            {
                _logger.LogInformation("User is logged in, redirecting to Task.Index.");
                return RedirectToAction("Index", "Task");
            }
            else
            {
                _logger.LogInformation("User is not logged, continuing to view.");
                return View();
            }
            
        }

        /// <summary>
        ///     Action that displays error information to view.
        /// </summary>
        /// <returns>A <see cref="ErrorViewModel"/> passed through to a <see cref="Controller.View()"/>.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogInformation("Intialising a new ErrorViewModel and assigning the error information to it.");
            var model = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };

            _logger.LogInformation("Returning ErrorViewModel to view.");
            return View(model);
        }
    }
}
