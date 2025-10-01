using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CS50TaskList.Data.Entities;

namespace CS50TaskList.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<SubTask> SubTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Task>()
                .HasOne(u => u.User)
                .WithMany(t => t.Tasks)
                .HasForeignKey(u => u.UserId);

            builder.Entity<SubTask>()
                .HasOne(t => t.Task)
                .WithMany(s => s.SubTasks)
                .HasForeignKey(t => t.TaskId);
        }
    }
}
