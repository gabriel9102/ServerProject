//using Core.Application.UseCases;
//using Microsoft.AspNetCore.Routing;
//using Microsoft.AspNetCore.Mvc;
//using Core.Domain.Entities;

//namespace Infra.Controller
//{
//    [ApiController] // Definindo que este é um controlador de API
//    [Route("api/[controller]")] // Definindo a rota base para todos os endpoints

//    //ControllerBase é a classe padrão para controladores de API no ASP.NET Core.
//    //Fornece métodos úteis como Ok(), BadRequest().
//    public class TaskController : ControllerBase
//    {
//        private readonly AddTask _addTask;
//        private readonly DeleteTask _deleteTask;
//        private readonly GetTask _getTask;

//        // Injeção de dependências dos casos de uso
//        public TaskController(AddTask addTask, DeleteTask deleteTask, GetTask getTask)
//        {
//            Console.WriteLine("TaskController foi inicializado");
//            _addTask = addTask;
//            _deleteTask = deleteTask;
//            _getTask = getTask;
//        }

//        // Rota (POST /api/task/addtask)
//        [HttpPost("add-task")]
//        public async Task<IActionResult> AddTask([FromBody] AppTask task)
//        {
//            await _addTask.Execute(task);
//            return Ok(); // Retorna um código 200 OK
//        }

//        // Rota (POST /api/task/delete-task)
//        [HttpPost("delete-task")]
//        public async Task<IActionResult> DeleteTask([FromBody] int id)
//        {
//            var output = await _deleteTask.Execute(id);
//            return Ok(output);
//        }

//        // Rota (GET /api/task/get-task)
//        [HttpGet("get-task")]
//        public async Task<IActionResult> GetTask()
//        {
//            Console.WriteLine("Rota /get-task foi atingida");
//            var tasks = await _getTask.Execute();
//            return Ok(tasks); // Retorna a lista de tasks como JSON
//        }
//    }

//}
