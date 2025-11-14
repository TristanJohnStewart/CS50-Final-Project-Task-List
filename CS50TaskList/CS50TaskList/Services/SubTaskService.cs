using CS50TaskList.Data.Entities;
using CS50TaskList.Models;
using CS50TaskList.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace CS50TaskList.Services
{
    public class SubTaskService : ISubTaskService
    {
        private readonly IRepository<SubTask> _repository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        public SubTaskService(IRepository<SubTask> repository, IHttpContextAccessor contextAccessor, UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
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

        public async System.Threading.Tasks.Task CreateSubTaskAsync(SubTaskModel model)
        {
            try
            {
                var entity = new SubTask 
                { 
                    Title = model.Title,
                    Position = model.Position,
                    TaskId = model.ParentId
                };            

                await _repository.Create(entity);
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex) { throw; }
        }

        public async System.Threading.Tasks.Task DeleteSubTaskAsync(SubTaskModel model)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(model.Id);
                _repository.Remove(entity);
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex) { throw; }
        }

        public async System.Threading.Tasks.Task EditSubTaskAsync(SubTaskModel model)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(model.Id);
                entity.Title = model.Title;

                _repository.Update(entity);
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex) { throw; }
        }

        public async Task<SubTaskModel> PrepareForDeleteAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);

                var model = new SubTaskModel
                {
                    Id = id,
                    Title = entity.Title
                };

                return model;
            }
            catch (Exception ex) { throw; }
        }

        public async Task<SubTaskModel> PrepareForEditAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);

                var model = new SubTaskModel
                {
                    Id = id,
                    Title = entity.Title
                };

                return model;
            }
            catch (Exception ex) { throw; }
        }
    }
}
