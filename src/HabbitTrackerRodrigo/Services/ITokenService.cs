using HabbitTrackerRodrigo.Models;

namespace HabbitTrackerRodrigo.Services
{
    public interface ITokenService
    {
        string GenerateToken(ApplicationUser user);
    }
}
