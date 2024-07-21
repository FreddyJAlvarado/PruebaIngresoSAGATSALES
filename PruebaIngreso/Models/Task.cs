﻿using System.ComponentModel.DataAnnotations;

namespace PruebaIngreso.Models
{
    public class Task
    {
        [Key]
        public int TaskId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }
    }
}
