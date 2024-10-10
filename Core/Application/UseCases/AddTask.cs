using Core.Domain.Repository;
using Core.Domain.Entities;

namespace Core.Application.UseCases
{
    public class AddTask
    {
        private readonly ITaskRepository _taskRepository;

        public AddTask(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task Execute(AppTask task) { 
            await _taskRepository.AddTask(task);
        }
    }
}
