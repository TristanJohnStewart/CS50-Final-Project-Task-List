using CS50TaskList.Enums;
using System;
using System.Collections.Generic;

namespace CS50TaskList.Data.Entities
{
    /// <summary>
    ///     An entity class to hold and represent a task's data from the db.
    /// </summary>
    public class Task
    {
        /// <summary>
        ///     An Id for the <see cref="Task"/> as a positive interger.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     A Title for the <see cref="Task"/> as a string.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     A description or notes of the <see cref="Task"/> as a string.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        ///     A date deadline for the <see cref="Task"/>.
        /// </summary>
        public DateOnly? Date { get; set; }

        /// <summary>
        ///     A time deadline for the <see cref="Task"/>.
        /// </summary>
        public TimeOnly? Time { get; set; }

        /// <summary>
        ///     An enum to designate the timespan between recurrances of the <see cref="Task"/>.
        /// </summary>
        public RecurranceType Recurrance { get; set; }

        /// <summary>
        ///     An enum to designate the priority of the <see cref="Task"/>.
        /// </summary>
        public PriorityType Priority { get; set; }

        /// <summary>
        ///     The <see cref="Task"/>'s position on the <see cref="ApplicationUser.Tasks"/> list. 
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        ///     The <see cref="Task"/>'s current completion state.
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        ///     A <see cref="List{T}"/> of <see cref="SubTask"/>'s tied to the <see cref="Task"/>.
        /// </summary>
        public ICollection<SubTask> SubTasks { get; set; } = new List<SubTask>();

        /// <summary>
        ///     A Foreign Key from the db for the user's Id / primary key.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        ///     The data of the <see cref="Task"/>'s <see cref="ApplicationUser"/>.
        /// </summary>
        public ApplicationUser User { get; set; }
    }
}
