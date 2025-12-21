using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CS50TaskList.Data.Entities
{
    /// <summary>
    ///     An entity class to represent and hold the data of a user and their tasks, inheriting <see cref="IdentityUser{TKey}"/> which uses a string as a primary key.
    ///     It has the <see cref="Tasks"/> property which is a <see cref="ICollection{T}"/> of <see cref="Task"/>'s.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        ///     An <see cref="ICollection{T}"/> of <see cref="Task"/>'s to be populated by the <see cref="Task"/>'s tied to the <see cref="ApplicationUser"/>'s primary key.
        /// </summary>
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
