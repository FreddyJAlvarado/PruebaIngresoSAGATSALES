using System.ComponentModel.DataAnnotations;

namespace PruebaIngreso.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public int TaskId { get; set; }
        public int? ParentCommentId { get; set; }
        public string CommentText { get; set; }
        public bool IsUpdated { get; set; }
        
    }
}
