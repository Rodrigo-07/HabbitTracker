using HabbitTrackerRodrigo.Data;
using HabbitTrackerRodrigo.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HabbitTrackerRodrigo.Services
{
    public class HabitService : IHabitService
    {
        private readonly ApplicationDbContext _context;

        public HabitService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Habit>> GetHabitsAsync(int userId)
        {
            return await _context.Habits.Where(h => h.UserId == userId).ToListAsync();
        }

        public async Task<Habit> CreateHabitAsync(int userId, Habit habit)
        {
            habit.UserId = userId;
            _context.Habits.Add(habit);
            await _context.SaveChangesAsync();
            return habit;
        }

        public Task<Habit> GetHabitAsync(int userId, int habbitId)
        {
            // pegar apenas um habito
            var habit = _context.Habits.Where(h => h.UserId == userId && h.HabitId == habbitId).FirstOrDefaultAsync(); // FirstOrDefault retorna o primeiro elemento ou null

            return habit;
        }
    }
}
