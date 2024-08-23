using System.ComponentModel.DataAnnotations;

namespace HabbitTrackerRodrigo.Models
{
    public class Habit
    {
        [Key]
        public int HabitId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int DaysGoal { get; set; }
        public int DaysToComplete { get; set; }
        public int CurrentStreak { get; set; }
        public int MaxStreak { get; set; }
    }
}
