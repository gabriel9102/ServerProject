using Core.Domain.Repository;
using Core.Domain.Entities;

namespace Infra.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly List<AppTask> _tasks = new List<AppTask>();

        public async Task AddTask(AppTask task)
        {
            task.Id = _tasks.Count + 1;
            _tasks.Add(task);
            await Task.CompletedTask;
        }

        public async Task<bool> DeleteTask(int id) 
        {
            var task = _tasks.Find(x => x.Id == id);
            if (task != null) 
            {
                _tasks.Remove(task);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        public async Task<List<AppTask>> GetAllTasks()
        {
            return await Task.FromResult(_tasks);
        }
    }
}
