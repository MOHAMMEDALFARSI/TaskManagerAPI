using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskManagerAPI.Model;
using TaskManagerAPI.Repositories;

namespace TaskManagerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {

        private readonly TaskRepository taskRepository;
        private readonly ILogger<TasksController> _logger;

        public TasksController(TaskRepository repository, ILogger<TasksController> logger)
        {
            taskRepository = repository;
            this._logger = logger;
        }
        [HttpGet]
        public IActionResult GetTasks()
        {
            var tasks = taskRepository.GetAllTasks();
            return Ok(tasks);
        }
        [HttpGet("{id}")]
        public IActionResult GetTaskById(int id)
        {
            var task = taskRepository.GetTaskById(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }
        [HttpPost]
        public IActionResult InsertTask([FromBody] TaskModel task)
        {
            taskRepository.InsertTask(task);
            _logger.LogInformation($"InsertTask API operation: Task with ID {task.Id} inserted.");
            return Ok(task);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] TaskModel updatedTask)
        {
            updatedTask.Id = id;
            taskRepository.UpdateTask(updatedTask);
            
            _logger.LogInformation($"UpdateTask API operation: Task with ID {id} updated.");
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            taskRepository.SoftDeleteTask(id);
            _logger.LogInformation($"DeleteTask API operation: Task with ID {id} soft-deleted.");
            return NoContent();
        }

    }
}