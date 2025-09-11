namespace CS50TaskList.Models
{
    public class SubTaskModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Position { get; set; }
        public bool IsCompleted { get; set; }
        public int ParentId { get; set; }
    }
}
