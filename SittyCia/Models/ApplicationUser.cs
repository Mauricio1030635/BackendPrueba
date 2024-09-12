using Microsoft.AspNetCore.Identity;

namespace SittyCia.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public ICollection<TaskEntity> Tasks { get; set; }
    }
}
