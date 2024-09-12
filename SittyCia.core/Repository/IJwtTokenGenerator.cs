using SittyCia.Core.Models;

namespace SittyCia.Core.Repository
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    }
}
