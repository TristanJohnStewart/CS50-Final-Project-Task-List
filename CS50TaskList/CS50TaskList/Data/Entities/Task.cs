using CS50TaskList.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;

namespace CS50TaskList.Data.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
        public DateTimeOffset? Deadline { get; set; } 
        public RecurranceType Recurrance {  get; set; }
        public PriorityType Priority { get; set; }
        public int Position { get; set; }
        public bool IsCompleted { get; set; }
        public ICollection<SubTask> SubTasks { get; set; } = new List<SubTask>();
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
