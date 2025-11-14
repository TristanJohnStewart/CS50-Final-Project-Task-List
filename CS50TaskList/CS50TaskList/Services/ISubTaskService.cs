using CS50TaskList.Models;
using System.Threading.Tasks;

namespace CS50TaskList.Services
{
    public interface ISubTaskService
    {
        Task CompleteTaskAsync(int id, bool isCompleted);
        Task CreateSubTaskAsync(SubTaskModel model);
        Task DeleteSubTaskAsync(SubTaskModel model);
        Task EditSubTaskAsync(SubTaskModel model);
        Task<SubTaskModel> PrepareForDeleteAsync(int id);
        Task<SubTaskModel> PrepareForEditAsync(int id);
    }
}