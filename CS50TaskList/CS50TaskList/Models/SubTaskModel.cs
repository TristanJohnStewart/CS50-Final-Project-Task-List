namespace CS50TaskList.Models
{
    /// <summary>
    ///     An model class to hold and represent a subtask's data to be passed from and to a <see cref="Data.Entities.SubTask"/> onject.
    /// </summary>
    public class SubTaskModel
    {
        /// <summary>
        ///     An Id for the <see cref="SubTaskModel"/> as a positive interger .
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     A Title for the <see cref="SubTaskModel"/> as a string.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     The <see cref="SubTaskModel"/>'s position on the parent <see cref="TaskModel"/> list as a positive interger. 
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        ///     The <see cref="SubTaskModel"/>'s current completion state.
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        ///     A Foreign Key from the db for the parent <see cref="TaskModel"/> equal to <see cref="TaskModel.Id"/>.
        /// </summary>
        public int ParentId { get; set; }
    }
}
