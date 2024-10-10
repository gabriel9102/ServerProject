using Core.Domain.Repository;
using Core.Domain.Entities;

namespace Core.Application.UseCases
{
    public class GetTask
    {
        private readonly ITaskRepository _taskRepository;

        public GetTask(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<List<AppTask>> Execute()
        {
            return await _taskRepository.GetAllTasks();
        }
    }
}
