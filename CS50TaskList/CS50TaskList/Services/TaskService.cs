using CS50TaskList.Data.Entities;
using CS50TaskList.Models;
using CS50TaskList.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace CS50TaskList.Services
{
    public class TaskService : ITaskService
    {
        private readonly IRepository<Task> _repository;
        private readonly IRepository<SubTask> _subTaskRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        public TaskService(IRepository<Task> repository, IHttpContextAccessor contextAccessor, UserManager<IdentityUser> userManager, IRepository<SubTask> subTaskRepository)
        {
            _repository = repository;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _subTaskRepository = subTaskRepository;
        }

        public async System.Threading.Tasks.Task CompleteTaskAsync(int id, bool isCompleted)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                entity.IsCompleted = isCompleted;
                _repository.Update(entity);
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex) { throw; }
        }

        public async System.Threading.Tasks.Task CreateTaskAsync(TaskModel model)
        {
            try
            {
                Task entity = new()
                {
                    Title = model.Title,
                    Notes = model.Notes,
                    Deadline = model.Deadline,
                    Recurrance = model.Recurrance,
                    Priority = model.Priority,
                    Position = model.Position,
                    UserId = _userManager.GetUserId(_contextAccessor.HttpContext.User)
                };

                await _repository.Create(entity);
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex) { throw; }
        }

        public async System.Threading.Tasks.Task DeleteTaskAsync(TaskModel model)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(model.Id);
                _repository.Remove(entity);
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex) { throw; }
        }

        public async System.Threading.Tasks.Task EditTaskAsync(TaskModel model)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(model.Id);
                entity.Title = model.Title;
                entity.Notes = model.Notes;
                entity.Deadline = model.Deadline;
                entity.Recurrance = model.Recurrance;
                entity.Priority = model.Priority;
                entity.Position = model.Position;

                _repository.Update(entity);
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex) { throw; }
        }

        public async System.Threading.Tasks.Task<TaskModel> PrepareForDeleteAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);

                TaskModel model = new()
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Notes = entity.Notes,
                    Deadline = entity.Deadline,
                    Recurrance = entity.Recurrance,
                    Priority = entity.Priority,
                    Position = entity.Position
                };

                return model;
            }
            catch (Exception ex) { throw; }
        }

        public async System.Threading.Tasks.Task<TaskModel> PrepareForEditAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);

                TaskModel model = new TaskModel
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Notes = entity.Notes,
                    Deadline = entity.Deadline,
                    Recurrance = entity.Recurrance,
                    Priority = entity.Priority,
                    Position = entity.Position
                };

                return model;
            }
            catch (Exception ex) { throw; }
        }

        public async System.Threading.Tasks.Task<List<TaskModel>> PrepareForIndexAsync()
        {
            try
            {
                var userId = _userManager.GetUserId(_contextAccessor.HttpContext.User);
                var tasks = await _repository.GetAllAsync();
                var model = new List<TaskModel>();

                foreach (var task in tasks)
                {
                    var item = new TaskModel
                    {
                        Id = task.Id,
                        Title = task.Title,
                        Notes = task.Notes,
                        Deadline = task.Deadline,
                        Recurrance = task.Recurrance,
                        Priority = task.Priority,
                        Position = task.Position,
                        IsCompleted = task.IsCompleted
                    };

                    var subtasks = await _subTaskRepository.GetAllAsync(x => x.TaskId == item.Id);

                    if (subtasks != null)
                    {
                        var subModel = new List<SubTaskModel>();
                        foreach (var subtask in subtasks)
                        {
                            var subItem = new SubTaskModel
                            {
                                Id = subtask.Id,
                                Title = subtask.Title,
                                Position = subtask.Position,
                                IsCompleted = subtask.IsCompleted
                            };

                            subModel.Add(subItem);
                        }

                        item.SubTasks = subModel;
                    }

                    model.Add(item);
                }

                return model;
            }
            catch (Exception ex) { throw; }
        }
    }
}
