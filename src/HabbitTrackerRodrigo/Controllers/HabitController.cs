using HabbitTrackerRodrigo.Services;
using HabbitTrackerRodrigo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using HabbitTrackerRodrigo.Data;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.Bson;

namespace HabbitTrackerRodrigo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HabitController : Controller
    {
        private readonly IHabitService _habitService;
        private readonly ApplicationDbContext _context;
        public readonly IMongoCollection<UserLogModel> _usersCompletionLog;

        public HabitController(IHabitService habitService, ApplicationDbContext context, MongoDBService mongoDBService)
        {
            _habitService = habitService;
            _context = context;
            _usersCompletionLog = mongoDBService.Database.GetCollection<UserLogModel>("UserLog");
        }


        [HttpGet("habits")]
        [Authorize(Policy = "UserOnly")]
        public async Task<IActionResult> GetHabits()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var userId = await _context.ApplicationUsers.Where(u => u.UserName == userIdClaim.Value)
                .Select(u => u.Id)
                .FirstOrDefaultAsync();

            var habits = await _habitService.GetHabitsAsync(userId);
            return Ok(habits);
        }

        [HttpGet("getHabit")]
        [Authorize(Policy = "UserOnly")]
        public async Task<IActionResult> GetHabbit([FromQuery] int habbitId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var userId = await _context.ApplicationUsers.Where(u => u.UserName == userIdClaim.Value)
                .Select(u => u.Id)
                .FirstOrDefaultAsync();

            // Pegar as infomrações de apenas um habito
            var habit = await _habitService.GetHabitAsync(userId, habbitId);

            return Ok(habit);
        }


        [HttpPost("createHabit")]
        [Authorize(Policy = "UserOnly")]
        public async Task<IActionResult> CreateHabit([FromBody] Habit habit)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);


            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var userId = await _context.ApplicationUsers.Where(u => u.UserName == userIdClaim.Value)
                .Select(u => u.Id)
                .FirstOrDefaultAsync();

            var createdHabit = await _habitService.CreateHabitAsync(userId, habit);
            return Ok(createdHabit);
        }


        [HttpGet("AdminCheckUsers")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> AdminCheckUsers()
        {
            var users = await _context.ApplicationUsers.ToListAsync();
            return Ok(users);
        }

        [HttpGet("test-db")]
        public async Task<IActionResult> TestDb()
        {
            var anyUsers = await _context.ApplicationUsers.AnyAsync();
            return Ok(new { anyUsers });
        }

        [HttpPost("UpdateHabbitCompletions")]
        public async Task<IActionResult> UpdateHabbitCompletions(int HabitId, bool status = true)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var userId = await _context.ApplicationUsers.Where(u => u.UserName == userIdClaim.Value)
                .Select(u => u.Id)
                .FirstOrDefaultAsync();

            var userLog = new UserLogModel 
            { 
                UserId = userId, 
                HabitId = HabitId,
                Date = DateTime.Now.ToString("MM/dd/yyyy"),
                Completed = status
            };

            await _usersCompletionLog.InsertOneAsync(userLog);

            return Ok();

        }

        [HttpGet("GetAllHabbitCompletions")]
        public async Task<IActionResult> GetAllHabbitCompletions(int habbitId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var userId = await _context.ApplicationUsers.Where(u => u.UserName == userIdClaim.Value)
                .Select(u => u.Id)
                .FirstOrDefaultAsync();

            var filter = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Eq("userId", userId),
                Builders<BsonDocument>.Filter.Eq("habitId", habbitId)
                );

            var completions = await _usersCompletionLog.Find(new BsonDocument()).ToListAsync();

            return Ok(completions);
        }
    }
}
