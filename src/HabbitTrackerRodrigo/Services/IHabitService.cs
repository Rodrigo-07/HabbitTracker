using HabbitTrackerRodrigo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HabbitTrackerRodrigo.Services
{
    public interface IHabitService
    {
        Task<List<Habit>> GetHabitsAsync(int userId);
        Task<Habit> GetHabitAsync(int userId, int habbitId);
        Task<Habit> CreateHabitAsync(int userId, Habit habit);
    }
}
