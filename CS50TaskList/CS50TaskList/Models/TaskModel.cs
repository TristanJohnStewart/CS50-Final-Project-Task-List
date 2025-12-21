using CS50TaskList.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CS50TaskList.Models
{
    /// <summary>
    ///     An model class to hold and represent a task's data to be passed from and to a <see cref="Data.Entities.Task"/> onject.
    /// </summary>
    public class TaskModel
    {
        /// <summary>
        ///     An Id for the <see cref="TaskModel"/> as a positive interger.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     A Title for the <see cref="TaskModel"/> as a string.
        /// </summary>
        [Required(ErrorMessage = "Title required")]
        public string Title { get; set; }

        /// <summary>
        ///     A description or notes of the <see cref="TaskModel"/> as a string.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        ///     A date deadline for the <see cref="TaskModel"/>.
        /// </summary>
        public DateOnly? Date { get; set; }

        /// <summary>
        ///     A time deadline for the <see cref="TaskModel"/>.
        /// </summary>
        public TimeOnly? Time { get; set; }

        /// <summary>
        ///     An enum to designate the timespan between recurrances of the <see cref="TaskModel"/>.
        /// </summary>
        [Display(Name = "Repeat")]
        public RecurranceType Recurrance { get; set; }

        /// <summary>
        ///     An enum to designate the priority of the <see cref="TaskModel"/>.
        /// </summary>
        public PriorityType Priority { get; set; }

        /// <summary>
        ///     The <see cref="TaskModel"/>'s position on the user's task list. 
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        ///     The <see cref="TaskModel"/>'s current completion state.
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        ///     A Foreign Key from the db for the user's Id / primary key.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        ///     A <see cref="List{T}"/> of <see cref="SubTaskModel"/>'s tied to the <see cref="TaskModel"/>.
        /// </summary>
        public IList<SubTaskModel> SubTasks { get; set; }
    }
}
