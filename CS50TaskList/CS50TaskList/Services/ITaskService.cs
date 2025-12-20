using CS50TaskList.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CS50TaskList.Services
{
    /// <summary>
    ///     Service class to be used by the controllers for handling calls relating to Tasks to the Repository class.
    /// </summary>
    public interface ITaskService
    {
        /// <summary>
        ///     Method for switching a <see cref="Task.IsCompleted" /> state.
        /// </summary>
        /// <param name="id">Id of the Subtask.</param>
        Task CompleteTaskAsync(int id);

        /// <summary>
        ///     Method for creating a new <see cref="Task" />.
        /// </summary>
        /// <param name="model">Model representing the data.</param>
        Task CreateTaskAsync(TaskModel model);

        /// <summary>
        ///     Method for deleting a <see cref="Task" />.
        /// </summary>
        /// <param name="model">Model representing the data.</param>
        Task DeleteTaskAsync(TaskModel model);

        /// <summary>
        ///     Method for Editing a <see cref="Task" />.
        /// </summary>
        /// <param name="model">Model representing the data.</param>
        Task EditTaskAsync(TaskModel model);

        /// <summary>
        ///     Method for preparing and returning a <see cref="TaskModel" />.
        /// </summary>
        /// <param name="id">Id of Task.</param>
        /// <returns>A <see cref="TaskModel" />.</returns>
        Task<TaskModel> PrepareForDeleteAsync(int id);

        /// <summary>
        ///     Method for preparing and returning a <see cref="TaskModel" />.
        /// </summary>
        /// <param name="id">Id of Task.</param>
        /// <returns>A <see cref="TaskModel" />.</returns>
        Task<TaskModel> PrepareForEditAsync(int id);

        /// <summary>
        ///     Method for preparing and returning a <see cref="List{}" /> of <see cref="TaskModel" />.
        /// </summary>
        /// <returns>A <see cref="List{}" /> of <see cref="TaskModel"/>.</returns>
        Task<List<TaskModel>> PrepareForIndexAsync();

        // <summary>
        ///     Method for preparing and returning a <see cref="List{}" /> of <see cref="TaskModel" />.
        /// </summary>
        /// <returns>A <see cref="List{}" /> of <see cref="TaskModel"/>.</returns>
        Task<List<TaskModel>> PrepareForViewCompleted();
    }
}