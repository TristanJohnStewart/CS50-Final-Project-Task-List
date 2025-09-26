namespace CS50TaskList.Data.Entities
{
    public class SubTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Position { get; set; }
        public bool IsCompleted { get; set; }

        public int TaskId { get; set; }
        public Task Task { get; set; }

    }
}
