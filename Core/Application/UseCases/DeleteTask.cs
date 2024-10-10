using Core.Domain.Repository;

namespace Core.Application.UseCases
{
    public class DeleteTask
    {
        private readonly ITaskRepository _taskRepository;

        public DeleteTask(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<bool> Execute(int id)
        {
           return await _taskRepository.DeleteTask(id);
        }
    }
}
