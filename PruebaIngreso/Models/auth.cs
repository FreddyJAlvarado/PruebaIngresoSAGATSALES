using System.ComponentModel.DataAnnotations;

namespace PruebaIngreso.Models
{
    public class auth
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
