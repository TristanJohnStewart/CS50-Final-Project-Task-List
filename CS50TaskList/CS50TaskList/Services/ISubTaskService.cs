using CS50TaskList.Data.Entities;
using CS50TaskList.Models;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using TaskEntity = CS50TaskList.Data.Entities.Task;

namespace CS50TaskList.Services
{
    /// <summary>
    ///     Service class to be used by the controllers for handling calls relating to Subtasks to the Repository class.
    /// </summary>
    public interface ISubTaskService
    {
        /// <summary>
        ///     Method for switching a <see cref="SubTask.IsCompleted" /> state.
        /// </summary>
        /// <param name="id">Id of the Subtask.</param>
        Task CompleteTaskAsync(int id);

        /// <summary>
        ///     Method for creating a new <see cref="SubTask" /> for a <see cref="TaskEntity" />.
        /// </summary>
        /// <param name="model">Model representing the data.</param>
        Task CreateSubTaskAsync(SubTaskModel model);

        /// <summary>
        ///     Method for deleting a <see cref="SubTask" />.
        /// </summary>
        /// <param name="model">Model representing the data.</param>
        Task DeleteSubTaskAsync(SubTaskModel model);

        /// <summary>
        ///     Method for Editing a <see cref="SubTask" />.
        /// </summary>
        /// <param name="model">Model representing the data.</param>
        Task EditSubTaskAsync(SubTaskModel model);

        /// <summary>
        ///     Method for preparing and returning a <see cref="SubTaskModel" />.
        /// </summary>
        /// <param name="id">Id of Subtask.</param>
        /// <returns>A <see cref="SubTaskModel" />.</returns>
        Task<SubTaskModel> PrepareForDeleteAsync(int id);

        /// <summary>
        ///     Method for preparing and returning a <see cref="SubTaskModel" />.
        /// </summary>
        /// <param name="id">Id of Subtask.</param>
        /// /// <returns>A <see cref="SubTaskModel" />.</returns>
        Task<SubTaskModel> PrepareForEditAsync(int id);
    }
}