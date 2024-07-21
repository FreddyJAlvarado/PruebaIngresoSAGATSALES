using Microsoft.AspNetCore.Mvc;
using PruebaIngreso.Data;

namespace PruebaIngreso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CommentsController(AppDbContext context)
        {
            _context = context;
        }

        // Métodos CRUD

        //1. Crear un comentario en una tarea. Un comentario puede ser contestado por otro comentario.
        [HttpPost]
        [Route("CreateComment")]
        public IActionResult CreateComment([FromBody] Models.Comment comment)
        {
            if (comment == null)
            {
                return BadRequest("Comment is null.");
            }

            _context.Comments.Add(comment);
            _context.SaveChanges();

            return Ok(comment);
        }

        //2. Obtener todos los comentarios de una tarea
        [HttpGet]
        [Route("GetCommentsByTaskId/{taskId}")]
        public IActionResult GetCommentsByTaskId(int taskId)
        {
            var comments = _context.Comments.Where(c => c.TaskId == taskId).ToList();
            return Ok(comments);
        }

        //3. Actualizar un comentario
        [HttpPut]
        [Route("UpdateComment")]
        public IActionResult UpdateComment([FromBody] Models.Comment comment)
        {
            if (comment == null)
            {
                return BadRequest("Comment is null.");
            }

            var existingComment = _context.Comments.Find(comment.CommentId);
            if (existingComment == null)
            {
                return NotFound("Comment not found.");
            }

            existingComment.CommentText = comment.CommentText;
            existingComment.IsUpdated = true;

            _context.Comments.Update(existingComment);
            _context.SaveChanges();

            return Ok(existingComment);
        }

        //4. Eliminar un comentario
        [HttpDelete]
        [Route("DeleteComment/{id}")]
        public IActionResult DeleteComment(int id)
        {
            var comment = _context.Comments.Find(id);
            if (comment == null)
            {
                return NotFound("Comment not found.");
            }

            _context.Comments.Remove(comment);
            _context.SaveChanges();

            return Ok(comment);
        }
    }

}
