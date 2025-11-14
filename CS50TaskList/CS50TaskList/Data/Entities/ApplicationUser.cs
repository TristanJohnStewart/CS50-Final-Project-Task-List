using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CS50TaskList.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
