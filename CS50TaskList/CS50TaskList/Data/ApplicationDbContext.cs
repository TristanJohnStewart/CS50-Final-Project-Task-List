using CS50TaskList.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CS50TaskList.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        /// <summary>
        ///     Sets the table structure for Tasks by using Entities.Task's Properties to establish the columns, their names, and types in the db.
        /// </summary>
        public DbSet<Task> Tasks { get; set; }

        /// <summary>
        ///     Sets the table structure for SubTasks by using Entities.SubTask's Properties to establish the columns, their names, and types in the db.
        /// </summary>
        public DbSet<SubTask> SubTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /// <summary>
            ///     Sets the rules that apply to the Tasks table in the db.
            /// </summary>
            builder.Entity<Task>()
                .HasOne(u => u.User)
                .WithMany(t => t.Tasks)
                .HasForeignKey(u => u.UserId);

            /// <summary>
            ///     Sets the rules that apply to the SubTasks table in the db.
            /// </summary>
            builder.Entity<SubTask>()
                .HasOne(t => t.Task)
                .WithMany(s => s.SubTasks)
                .HasForeignKey(t => t.TaskId);
        }
    }
}
