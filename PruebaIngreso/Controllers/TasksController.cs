using Microsoft.AspNetCore.Mvc;
using PruebaIngreso.Data;
using PruebaIngreso.Models;

namespace PruebaIngreso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        // Métodos CRUD

        //1. Crear una tarea
        [HttpPost]
        [Route("CreateTask")]
        public IActionResult CreateTask([FromBody] Models.Task task)
        {
            if (task == null)
            {
                return BadRequest("Task is null.");
            }

            _context.Tasks.Add(task);
            _context.SaveChanges();

            return Ok(task);
        }

        //2. Obtener todas las tareas
        [HttpGet]
        [Route("GetTasks")]
        public IActionResult GetTasks()
        {
            var tasks = _context.Tasks.ToList();
            return Ok(tasks);
        }

        //3. Obtener una tarea por id
        [HttpGet]
        [Route("GetTaskById/{id}")]
        public IActionResult GetTaskById(int id)
        {
            var task = _context.Tasks.Find(id);
            return Ok(task);
        }

        //4. Actualizar una tarea
        [HttpPut]
        [Route("UpdateTask")]
        public IActionResult UpdateTask([FromBody] Models.Task task)
        {
            if (task == null)
            {
                return BadRequest("Task is null.");
            }

            var existingTask = _context.Tasks.Find(task.TaskId);
            if (existingTask == null)
            {
                return NotFound("Task not found.");
            }

            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.IsCompleted = task.IsCompleted;
            existingTask.UserId = task.UserId;

            _context.Tasks.Update(existingTask);
            _context.SaveChanges();

            return Ok(existingTask);
        }

        //5. Eliminar una tarea
        [HttpDelete]
        [Route("DeleteTask/{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null)
            {
                return NotFound("Task not found.");
            }

            _context.Tasks.Remove(task);
            _context.SaveChanges();

            return Ok("Task deleted.");
        }
    }
}
