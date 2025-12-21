using CS50TaskList.Data.Entities;
using CS50TaskList.Models;
using CS50TaskList.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using TaskEntity = CS50TaskList.Data.Entities.Task;

namespace CS50TaskList.Services
{
    /// <summary>
    ///     Service class to be used by the controllers for handling calls relating to Subtasks to the Repository class.
    /// </summary>
    public class SubTaskService : ISubTaskService
    {
        private readonly IRepository<SubTask> _repository;
        private readonly ILogger<SubTaskService> _logger;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SubTaskService" /> class.
        /// </summary>
        /// <param name="repository">Repository for Subtasks.</param>
        /// <param name="logger">Logger instance.</param>
        public SubTaskService(IRepository<SubTask> repository, ILogger<SubTaskService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        ///     Method for switching a <see cref="SubTask.IsCompleted" /> state.
        /// </summary>
        /// <param name="id">Id of the Subtask.</param>
        public async Task CompleteTaskAsync(int id)
        {
            _logger.LogInformation("Beginning SubTaskService CompleteTaskAsync Method for Subtask #{id}.", id);
            try
            {
                _logger.LogInformation("Retrieving Subtask Entity #{id} from DB.", id);
                var entity = await _repository.GetByIdAsync(id);

                _logger.LogInformation("Updating Subtask Entity #{id} IsComplete property to {!entity.IsCompleted} from {entity.IsCompleted}", id, !entity.IsCompleted, entity.IsCompleted);
                entity.IsCompleted = !entity.IsCompleted;

                _logger.LogInformation("Updating Subtask Entity #{id} in DB.", id);
                _repository.Update(entity);

                _logger.LogInformation("Saving changes to DB.");
                await _repository.SaveChangesAsync();

                _logger.LogInformation("Successfully ending SubTaskService CompleteTaskAsync Method for Subtask #{id}.", id);
            }
            catch (Exception ex) 
            {
                _logger.LogError("Error occured in SubTaskService CompleteTaskAsync Method. Error: {ex}", ex);
                throw; 
            }
        }

        /// <summary>
        ///     Method for creating a new <see cref="SubTask" /> for a <see cref="TaskEntity" />.
        /// </summary>
        /// <param name="model">Model representing the data.</param>
        public async Task CreateSubTaskAsync(SubTaskModel model)
        {
            _logger.LogInformation("Beginning SubTaskService CreateSubTaskAsync Method to create Subtask for Task #{model.ParentId}.", model.ParentId);
            try
            {
                _logger.LogInformation("Intialising new Subtask Entity.");
                var entity = new SubTask 
                { 
                    Title = model.Title,
                    Position = model.Position,
                    TaskId = model.ParentId
                };

                _logger.LogInformation("Creating new Subtask entry in the DB.");
                await _repository.Create(entity);

                _logger.LogInformation("Saving changes to DB.");
                await _repository.SaveChangesAsync();

                _logger.LogInformation("Successfully ending SubTaskService CreateSubTaskAsync Method.");
            }
            catch (Exception ex) 
            {
                _logger.LogError("Error occured in SubTaskService CreateSubTaskAsync Method. Error: {ex}", ex);
                throw; 
            }
        }

        /// <summary>
        ///     Method for deleting a <see cref="SubTask" />.
        /// </summary>
        /// <param name="model">Model representing the data.</param>
        public async Task DeleteSubTaskAsync(SubTaskModel model)
        {
            _logger.LogInformation("Beginning SubTaskService DeleteSubTaskAsync Method to delete Subtask #{model.Id} from DB.", model.Id);
            try
            {
                _logger.LogInformation("Retrieving Subtask Entity #{model.Id} from DB.", model.Id);
                var entity = await _repository.GetByIdAsync(model.Id);

                _logger.LogInformation("Removing Subtask #{model.Id} from DB.", model.Id);
                _repository.Remove(entity);

                _logger.LogInformation("Saving changes to DB.");
                await _repository.SaveChangesAsync();

                _logger.LogInformation("Successfully ending SubTaskService DeleteSubTaskAsync Method.");
            }
            catch (Exception ex) 
            {
                _logger.LogError("Error occured in SubTaskService DeleteSubTaskAsync Method. Error: {ex}", ex);
                throw; 
            }
        }

        /// <summary>
        ///     Method for Editing a <see cref="SubTask" />.
        /// </summary>
        /// <param name="model">Model representing the data.</param>
        public async Task EditSubTaskAsync(SubTaskModel model)
        {
            _logger.LogInformation("Beginning SubTaskService EditSubTaskAsync Method to edit Subtask #{model.Id} in DB.", model.Id);
            try
            {
                _logger.LogInformation("Retrieving Subtask Entity #{model.Id} from DB.", model.Id);
                var entity = await _repository.GetByIdAsync(model.Id);

                _logger.LogInformation("Asigning new Subtask Entity Title property.");
                entity.Title = model.Title;

                _logger.LogInformation("Updating Subtask #{model.Id} in DB.", model.Id);
                _repository.Update(entity);

                _logger.LogInformation("Saving changes to DB.");
                await _repository.SaveChangesAsync();

                _logger.LogInformation("Successfully ending SubTaskService EditSubTaskAsync Method.");
            }
            catch (Exception ex) 
            {
                _logger.LogError("Error occured in SubTaskService EditSubTaskAsync Method. Error: {ex}", ex);
                throw; 
            }
        }

        /// <summary>
        ///     Method for preparing and returning a <see cref="SubTaskModel" />.
        /// </summary>
        /// <param name="id">Id of Subtask.</param>
        /// <returns>A <see cref="SubTaskModel" />.</returns>
        public async Task<SubTaskModel> PrepareForDeleteAsync(int id)
        {
            _logger.LogInformation("Beginning SubTaskService PrepareForDeleteAsync Method to prepare Subtask #{id} for the Delete View.", id);
            try
            {
                _logger.LogInformation("Retrieving Subtask Entity #{id} from DB.", id);
                var entity = await _repository.GetByIdAsync(id);

                _logger.LogInformation("Intialising new SubtaskModel for Subtask #{id}.", id);
                var model = new SubTaskModel
                {
                    Id = id,
                    Title = entity.Title,
                    ParentId = entity.TaskId
                };

                _logger.LogInformation("Sucessfully returning model of Subtask #{id}.", id);
                return model;
            }
            catch (Exception ex) 
            {
                _logger.LogError("Error occured in SubTaskService PrepareForDeleteAsync Method. Error: {ex}", ex);
                throw; 
            }
        }

        /// <summary>
        ///     Method for preparing and returning a <see cref="SubTaskModel" />.
        /// </summary>
        /// <param name="id">Id of Subtask.</param>
        /// /// <returns>A <see cref="SubTaskModel" />.</returns>
        public async Task<SubTaskModel> PrepareForEditAsync(int id)
        {
            _logger.LogInformation("Beginning SubTaskService PrepareForEditAsync Method to prepare Subtask #{id} for the Delete View.", id);
            try
            {
                _logger.LogInformation("Retrieving Subtask Entity #{id} from DB.", id);
                var entity = await _repository.GetByIdAsync(id);

                _logger.LogInformation("Intialising new SubtaskModel for Subtask #{id}.", id);
                var model = new SubTaskModel
                {
                    Id = id,
                    Title = entity.Title,
                    ParentId = entity.TaskId
                };

                _logger.LogInformation("Sucessfully returning model of Subtask #{id}.", id);
                return model;
            }
            catch (Exception ex) 
            {
                _logger.LogError("Error occured in SubTaskService PrepareForEditAsync Method. Error: {ex}", ex);
                throw; 
            }
        }
    }
}
