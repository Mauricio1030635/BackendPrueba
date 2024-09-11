using Microsoft.AspNetCore.Identity;

namespace SittyCia.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
