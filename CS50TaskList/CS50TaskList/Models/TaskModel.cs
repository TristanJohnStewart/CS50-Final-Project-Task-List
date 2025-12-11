using CS50TaskList.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CS50TaskList.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title required")]
        public string Title { get; set; }
        public string Notes { get; set; }
        public DateOnly? Date { get; set; }
        public TimeOnly? Time { get; set; }

        [Display(Name = "Repeat")]
        public RecurranceType Recurrance { get; set; }
        public PriorityType Priority { get; set; }
        public int Position { get; set; }
        public bool IsCompleted { get; set; }
        public string UserId { get; set; }
        public IList<SubTaskModel> SubTasks { get; set; }
    }
}
