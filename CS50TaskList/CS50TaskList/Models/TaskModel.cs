using CS50TaskList.Enums;

namespace CS50TaskList.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
        public DateTimeOffset Deadline { get; set; }
        public RecurranceType Recurrance { get; set; }
        public PriorityType Priority { get; set; }
        public int Position { get; set; }
        public bool IsCompleted { get; set; }
        public string UserId { get; set; }
        public IList<SubTaskModel> SubTasks { get; set; }
    }
}
