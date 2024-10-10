using Core.Domain.Entities;

namespace Core.Domain.Repository
{
    public interface ITaskRepository
    {
        Task AddTask(AppTask task);
        Task<bool> DeleteTask(int id);
        Task<List<AppTask>> GetAllTasks();
    }
}
