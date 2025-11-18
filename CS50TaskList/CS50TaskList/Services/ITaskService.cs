using CS50TaskList.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CS50TaskList.Services
{
    public interface ITaskService
    {
        Task CompleteTaskAsync(int id);
        Task CreateTaskAsync(TaskModel model);
        Task DeleteTaskAsync(TaskModel model);
        Task EditTaskAsync(TaskModel model);
        Task<TaskModel> PrepareForDeleteAsync(int id);
        Task<TaskModel> PrepareForEditAsync(int id);
        Task<List<TaskModel>> PrepareForIndexAsync();
        Task<List<TaskModel>> PrepareForViewCompleted();
    }
}