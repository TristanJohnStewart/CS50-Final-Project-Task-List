namespace CS50TaskList.Data.Entities
{
    /// <summary>
    ///     An entity class to hold and represent a subtask's data from the db.
    /// </summary>
    public class SubTask
    {
        /// <summary>
        ///     An Id for the <see cref="SubTask"/> as a positive interger .
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     A Title for the <see cref="SubTask"/> as a string.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     The <see cref="SubTask"/>'s position on the parent <see cref="Task.SubTasks"/> list as a positive interger. 
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        ///     The <see cref="SubTask"/>'s current completion state.
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        ///     A Foreign Key from the db for the parent <see cref="Entities.Task"/> equal to <see cref="Task.Id"/>.
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        ///     The data of the parent <see cref="Entities.Task"/> of the <see cref="SubTask"/>.
        /// </summary>
        public Task Task { get; set; }
    }
}
