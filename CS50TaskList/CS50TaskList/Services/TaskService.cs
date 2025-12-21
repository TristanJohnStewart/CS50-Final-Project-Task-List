using CS50TaskList.Data.Entities;
using CS50TaskList.Models;
using CS50TaskList.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using TaskEntity = CS50TaskList.Data.Entities.Task;

namespace CS50TaskList.Services
{
    /// <summary>
    ///     Service class to be used by the controllers for handling calls relating to Tasks to the Repository class.
    /// </summary>
    public class TaskService : ITaskService
    {
        private readonly IRepository<TaskEntity> _repository;
        private readonly IRepository<SubTask> _subTaskRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILogger<TaskService> _logger;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SubTaskService" /> class.
        /// </summary>
        /// <param name="repository">Repository for Tasks.</param>
        /// <param name="contextAccessor">The current HttpContext.</param>
        /// <param name="userManager">User Manager instance.</param>
        /// <param name="subTaskRepository">Repository for Tasks.</param>
        /// <param name="logger">Logger instance.</param>
        public TaskService(IRepository<TaskEntity> repository, IHttpContextAccessor contextAccessor, UserManager<IdentityUser> userManager, IRepository<SubTask> subTaskRepository, ILogger<TaskService> logger)
        {
            _repository = repository;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _subTaskRepository = subTaskRepository;
            _logger = logger;
        }

        /// <summary>
        ///     Method for switching a <see cref="TaskEntity.IsCompleted" /> state.
        /// </summary>
        /// <param name="id">Id of the Subtask.</param>
        public async Task CompleteTaskAsync(int id)
        {
            _logger.LogInformation("Beginning TaskService CompleteTaskAsync Method for Task #{id}.", id);
            try
            {
                _logger.LogInformation("Retrieving Task Entity #{id} from DB.", id);
                var entity = await _repository.GetByIdAsync(id);

                _logger.LogInformation("Updating Task Entity #{id} IsComplete property to {!entity.IsCompleted} from {entity.IsCompleted}", id, !entity.IsCompleted, entity.IsCompleted);
                entity.IsCompleted = !entity.IsCompleted;

                _logger.LogInformation("Updating Task Entity #{id} in DB.", id);
                _repository.Update(entity);

                _logger.LogInformation("Saving changes to DB.");
                await _repository.SaveChangesAsync();

                _logger.LogInformation("Successfully ending TaskService CompleteTaskAsync Method for Task #{id}.", id);
            }
            catch (Exception ex) 
            {
                _logger.LogError("Error occured in TaskService CompleteTaskAsync Method. Error: {ex}", ex);
                throw; 
            }
        }

        /// <summary>
        ///     Method for creating a new <see cref="TaskEntity" />.
        /// </summary>
        /// <param name="model">Model representing the data.</param>
        public async Task CreateTaskAsync(TaskModel model)
        {
            _logger.LogInformation("Beginning TaskService CompleteTaskAsync Method for new Task");
            try
            {
                _logger.LogInformation("Intialising new Task Entity.");
                TaskEntity entity = new()
                {
                    Title = model.Title,
                    Notes = model.Notes,
                    Date = model.Date,
                    Recurrance = model.Recurrance,
                    Priority = model.Priority,
                    Position = model.Position,
                    UserId = _userManager.GetUserId(_contextAccessor.HttpContext.User)
                };

                _logger.LogInformation("Checking if Task Time needs to be intialised.");
                if (model.Date is not null)
                {
                    entity.Time = model.Time;
                }

                _logger.LogInformation("Creating new Task entry in the DB.");
                await _repository.Create(entity);

                _logger.LogInformation("Saving changes to DB.");
                await _repository.SaveChangesAsync();

                _logger.LogInformation("Successfully ending TaskService CreateTaskAsync Method.");
            }
            catch (Exception ex) 
            {
                _logger.LogError("Error occured in TaskService CreateTaskAsync Method. Error: {ex}", ex);
                throw; 
            }
        }

        /// <summary>
        ///     Method for deleting a <see cref="TaskEntity" />.
        /// </summary>
        /// <param name="model">Model representing the data.</param>
        public async Task DeleteTaskAsync(TaskModel model)
        {
            _logger.LogInformation("Beginning TaskService DeleteTaskAsync Method to delete Task #{model.Id} from DB.", model.Id);
            try
            {
                _logger.LogInformation("Retrieving Task Entity #{model.Id} from DB.", model.Id);
                var entity = await _repository.GetByIdAsync(model.Id);

                _logger.LogInformation("Retrieving All Subtasks Entities tied to Task #{model.Id} from DB.", model.Id);
                var subEntities = await _subTaskRepository.GetAllAsync(x => x.TaskId == model.Id);

                foreach (var subEntity in subEntities)
                {
                    _logger.LogInformation("Removing SubTask #{model.Id} from DB.", subEntity.Id);
                    _subTaskRepository.Remove(subEntity);
                }

                _logger.LogInformation("Removing Task #{model.Id} from DB.", model.Id);
                _repository.Remove(entity);

                _logger.LogInformation("Saving changes to DB.");
                await _repository.SaveChangesAsync();

                _logger.LogInformation("Successfully ending TaskService DeleteTaskAsync Method.");
            }
            catch (Exception ex) 
            {
                _logger.LogError("Error occured in TaskService DeleteTaskAsync Method. Error: {ex}", ex);
                throw; 
            }
        }

        /// <summary>
        ///     Method for Editing a <see cref="TaskEntity" />.
        /// </summary>
        /// <param name="model">Model representing the data.</param>
        public async Task EditTaskAsync(TaskModel model)
        {
            _logger.LogInformation("Beginning TaskService.EditSubTaskAsync() Method to edit Task #{model.Id} in DB.", model.Id);
            try
            {
                _logger.LogInformation("Retrieving Task Entity #{model.Id} from DB.", model.Id);
                var entity = await _repository.GetByIdAsync(model.Id);

                _logger.LogInformation("Asigning new Task Entity Title property.");
                entity.Title = model.Title;

                _logger.LogInformation("Asigning new Task Entity Notes property.");
                entity.Notes = model.Notes;

                _logger.LogInformation("Asigning new Task Entity Date property.");
                entity.Date = model.Date;

                if (model.Date is not null)
                {
                    _logger.LogInformation("Time.Date is not null, asigning new Task Entity Time property.");
                    entity.Time = model.Time;
                }
                else
                {
                    _logger.LogInformation("Time.Date is null, asigning new Task Entity Time property to null.");
                    entity.Time = null;
                }

                _logger.LogInformation("Asigning new Task Entity Recurrance property.");
                entity.Recurrance = model.Recurrance;

                _logger.LogInformation("Asigning new Task Entity Priority property.");
                entity.Priority = model.Priority;

                _logger.LogInformation("Asigning new Task Entity Position property.");
                entity.Position = model.Position;

                _logger.LogInformation("Updating Task #{model.Id} in DB.", model.Id);
                _repository.Update(entity);

                _logger.LogInformation("Saving changes to DB.");
                await _repository.SaveChangesAsync();

                _logger.LogInformation("Successfully ending TaskService.EditTaskAsync() Method.");
            }
            catch (Exception ex) 
            {
                _logger.LogError("Error occured in TaskService.EditSubTaskAsync() Method. Error: {ex}", ex);
                throw; 
            }
        }

        /// <summary>
        ///     Method for preparing and returning a <see cref="TaskModel" />.
        /// </summary>
        /// <param name="id">Id of Task.</param>
        /// <returns>A <see cref="TaskModel" />.</returns>
        public async Task<TaskModel> PrepareForDeleteAsync(int id)
        {
            _logger.LogInformation("Beginning TaskService.PrepareForDeleteAsync() Method to prepare Task #{id} for the Delete View.", id);
            try
            {
                _logger.LogInformation("Retrieving Task Entity #{id} from DB.", id);
                var entity = await _repository.GetByIdAsync(id);

                _logger.LogInformation("Intialising new TaskModel for Task #{id}.", id);
                TaskModel model = new()
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Notes = entity.Notes,
                    Date = entity.Date,
                    Time = entity.Time,
                    Recurrance = entity.Recurrance,
                    Priority = entity.Priority,
                    Position = entity.Position,
                    SubTasks = new List<SubTaskModel>()
                };

                _logger.LogInformation("Retrieving All Subtask Entities tied to Task #{id} from DB.", id);
                var subtasks = await _subTaskRepository.GetAllAsync(x => x.TaskId == model.Id);

                foreach (var subtask in subtasks)
                {
                    _logger.LogInformation("Intialising new SubTaskModel for Subtask #{subtask.Id}.", subtask.Id);
                    var subModel = new SubTaskModel
                    {
                        Id = subtask.Id,
                        Title = subtask.Title,
                        Position = subtask.Position,
                        IsCompleted = subtask.IsCompleted,
                        ParentId = subtask.TaskId
                    };

                    _logger.LogInformation("Adding new SubTaskModel for Subtask #{subtask.Id} to TaskModel.", subtask.Id);
                    model.SubTasks.Add(subModel);
                }

                _logger.LogInformation("Sucessfully returning model of Task #{id}.", id);
                return model;
            }
            catch (Exception ex) 
            {
                _logger.LogError("Error occured in TaskService.PrepareForDeleteAsync() Method. Error: {ex}", ex);
                throw; 
            }
        }

        /// <summary>
        ///     Method for preparing and returning a <see cref="TaskModel" />.
        /// </summary>
        /// <param name="id">Id of Task.</param>
        /// <returns>A <see cref="TaskModel" />.</returns>
        public async Task<TaskModel> PrepareForEditAsync(int id)
        {
            _logger.LogInformation("Beginning TaskService.PrepareForEditAsync() Method to prepare Task #{id} for the Edit View.", id);
            try
            {
                _logger.LogInformation("Retrieving Task Entity #{id} from DB.", id);
                var entity = await _repository.GetByIdAsync(id);

                _logger.LogInformation("Intialising new TaskModel for Task #{id}.", id);
                TaskModel model = new TaskModel
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Notes = entity.Notes,
                    Date = entity.Date, 
                    Time = entity.Time,
                    Recurrance = entity.Recurrance,
                    Priority = entity.Priority,
                    Position = entity.Position,
                    SubTasks = new List<SubTaskModel>()
                };

                _logger.LogInformation("Retrieving All Subtask Entities tied to Task #{id} from DB.", id);
                var subtasks = await _subTaskRepository.GetAllAsync(x => x.TaskId == model.Id);

                foreach (var subtask in subtasks)
                {
                    _logger.LogInformation("Intialising new SubTaskModel for Subtask #{subtask.Id}.", subtask.Id);
                    var subModel = new SubTaskModel
                    {
                        Id = subtask.Id,
                        Title = subtask.Title,
                        Position = subtask.Position,
                        IsCompleted = subtask.IsCompleted
                    };

                    _logger.LogInformation("Adding new SubTaskModel for Subtask #{subtask.Id} to TaskModel.", subtask.Id);
                    model.SubTasks.Add(subModel);
                }

                _logger.LogInformation("Sucessfully returning model of Task #{id}.", id);
                return model;
            }
            catch (Exception ex) 
            {
                _logger.LogError("Error occured in TaskService.PrepareForEditAsync() Method. Error: {ex}", ex);
                throw; 
            }
        }

        /// <summary>
        ///     Method for preparing and returning a <see cref="List{}" /> of <see cref="TaskModel" />.
        /// </summary>
        /// <returns>A <see cref="List{}" /> of <see cref="TaskModel"/>.</returns>
        public async Task<List<TaskModel>> PrepareForIndexAsync()
        {
            _logger.LogInformation("Beginning TaskService.PrepareForIndexAsync() Method to prepare a List<TaskModel> for the View.");
            try
            {
                _logger.LogInformation("Retrieving UserId from HttpContext.");
                var userId = _userManager.GetUserId(_contextAccessor.HttpContext.User);

                _logger.LogInformation("Retrieving all Task Entities tied to UserId.");
                var tasks = await _repository.GetAllAsync(x => !x.IsCompleted);

                _logger.LogInformation("Intialising new List<TaskModel> for Tasks.");
                var model = new List<TaskModel>();

                foreach (var task in tasks)
                {
                    _logger.LogInformation("Intialising new TaskModel for Task #{task.Id}.", task.Id);
                    var item = new TaskModel
                    {
                        Id = task.Id,
                        Title = task.Title,
                        Notes = task.Notes,
                        Date = task.Date,
                        Time = task.Time,
                        Recurrance = task.Recurrance,
                        Priority = task.Priority,
                        Position = task.Position,
                        IsCompleted = task.IsCompleted
                    };

                    _logger.LogInformation("Retrieving All Subtask Entities tied to Task #{task.Id} from DB.", task.Id);
                    var subtasks = await _subTaskRepository.GetAllAsync(x => x.TaskId == item.Id);

                    if (subtasks != null)
                    {
                        _logger.LogInformation("Intialising new List<SubTaskModel> for Subtasks.");
                        var subModel = new List<SubTaskModel>();
                        foreach (var subtask in subtasks)
                        {
                            _logger.LogInformation("Intialising new SubTaskModel for Subtask #{subtask.Id}.", subtask.Id);
                            var subItem = new SubTaskModel
                            {
                                Id = subtask.Id,
                                Title = subtask.Title,
                                Position = subtask.Position,
                                IsCompleted = subtask.IsCompleted
                            };
                            _logger.LogInformation("Adding new SubTaskModel to List<SubTaskModel>.");
                            subModel.Add(subItem);
                        }

                        _logger.LogInformation("Assigning List<SubTaskModel> to #{task.Id} TaskModel.SubTasks.", task.Id);
                        item.SubTasks = subModel;
                    }

                    _logger.LogInformation("Adding new TaskModel to List<TaskModel>.");
                    model.Add(item);
                }

                _logger.LogInformation("Intialising new List<TaskModel> for the List<TaskModel> sorted by Date, Time, and then Priority.");
                var returnModel = model.Where(x => x.Date != null)
                    .OrderBy(x => x.Date)
                    .ThenBy(x => x.Time)
                    .ThenByDescending(x => x.Priority)
                    .ToList();

                _logger.LogInformation("Adding Tasks without a Date to Model and sorting by Priority.");
                returnModel.AddRange(model.Where(x => x.Date == null)
                    .OrderByDescending(x => x.Priority)
                    .ToList());

                _logger.LogInformation("Sucessfully returning model to view.");
                return returnModel;
            }
            catch (Exception ex) 
            {
                _logger.LogError("Error occured in TaskService.PrepareForIndexAsync() Method. Error: {ex}", ex);
                throw; 
            }
        }

        /// <summary>
        ///     Method for preparing and returning a <see cref="List{}" /> of <see cref="TaskModel" />.
        /// </summary>
        /// <returns>A <see cref="List{}" /> of <see cref="TaskModel"/>.</returns>
        public async Task<List<TaskModel>> PrepareForViewCompleted()
        {
            _logger.LogInformation("Beginning TaskService.PrepareForViewCompleted() Method to prepare a List<TaskModel> for the View.");
            try
            {
                _logger.LogInformation("Retrieving UserId from HttpContext.");
                var userId = _userManager.GetUserId(_contextAccessor.HttpContext.User);

                _logger.LogInformation("Retrieving all Task Entities tied to UserId.");
                var tasks = await _repository.GetAllAsync(x => x.IsCompleted == true);

                _logger.LogInformation("Intialising new List<TaskModel> for Tasks.");
                var model = new List<TaskModel>();

                foreach (var task in tasks)
                {
                    _logger.LogInformation("Intialising new TaskModel for Task #{task.Id}.", task.Id);
                    var item = new TaskModel
                    {
                        Id = task.Id,
                        Title = task.Title,
                        Notes = task.Notes,
                        Date = task.Date,
                        Time = task.Time,
                        Recurrance = task.Recurrance,
                        Priority = task.Priority,
                        Position = task.Position,
                        IsCompleted = task.IsCompleted
                    };

                    _logger.LogInformation("Retrieving All Subtask Entities tied to Task #{task.Id} from DB.", task.Id);
                    var subtasks = await _subTaskRepository.GetAllAsync(x => x.TaskId == item.Id);

                    if (subtasks != null)
                    {
                        _logger.LogInformation("Intialising new List<SubTaskModel> for Subtasks.");
                        var subModel = new List<SubTaskModel>();
                        foreach (var subtask in subtasks)
                        {
                            _logger.LogInformation("Intialising new SubTaskModel for Subtask #{subtask.Id}.", subtask.Id);
                            var subItem = new SubTaskModel
                            {
                                Id = subtask.Id,
                                Title = subtask.Title,
                                Position = subtask.Position,
                                IsCompleted = subtask.IsCompleted
                            };

                            _logger.LogInformation("Adding new SubTaskModel to List<SubTaskModel>.");
                            subModel.Add(subItem);
                        }

                        _logger.LogInformation("Assigning List<SubTaskModel> to #{task.Id} TaskModel.SubTasks.", task.Id);
                        item.SubTasks = subModel;
                    }

                    _logger.LogInformation("Adding new TaskModel to List<TaskModel>.");
                    model.Add(item);
                }

                _logger.LogInformation("Intialising new List<TaskModel> for the List<TaskModel> sorted by Date, Time, and then Priority.");
                var returnModel = model.Where(x => x.Date != null)
                    .OrderBy(x => x.Date)
                    .ThenBy(x => x.Time)
                    .ThenBy(x => x.Priority)
                    .ToList();

                _logger.LogInformation("Adding Tasks without a Date to Model and sorting by Priority.");
                returnModel.AddRange(model.Where(x => x.Date == null)
                    .OrderBy(x => x.Priority)
                    .ToList());

                _logger.LogInformation("Sucessfully returning model to view.");
                return returnModel;
            }
            catch (Exception ex) 
            {
                _logger.LogError("Error occured in TaskService.PrepareForViewCompleted() Method. Error: {ex}", ex);
                throw; 
            }
        }
    }
}
