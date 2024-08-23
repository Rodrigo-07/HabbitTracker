using HabbitTrackerRodrigo.Data;
using HabbitTrackerRodrigo.Models;
using HabbitTrackerRodrigo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Security.Claims;



namespace HabbitTrackerRodrigo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _jwtTokenGenerator;

        public AccountController(ApplicationDbContext context, ITokenService jwtTokenGenerator)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.ApplicationUsers
                .FirstOrDefaultAsync(u => u.UserName == model.UserName && u.PasswordHash == model.Password);

            if (user == null)
            {
                return Unauthorized("Crendeciais Erradas");
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Checar se já existe um usuário com o mesmo nome
            var existingUser = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.UserName == model.UserName);

            if (existingUser != null)
            {

               return BadRequest("Usuário já existente");
            }

            // Adiciona o usuário ao banco de dados
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                FullName = model.FullName,
                Email = model.Email,
                PasswordHash = model.Password,
                Birthday = model.Birthday,
                Role = "User"
            };


            try {
                _context.ApplicationUsers.Add(user);

                await _context.SaveChangesAsync();

                return Ok("Usuário criado");

            }
            catch (Exception e) {
                return BadRequest($"Erro ao adicionar usuário. Erro: {e}");
            }
           
        }

        [HttpGet("WhoAmI")]
        public async Task<IActionResult> WhoAmI()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var userId = await _context.ApplicationUsers.Where(u => u.UserName == userIdClaim.Value)
                .Select(u => u.Id)
                .FirstOrDefaultAsync();

            var user = await _context.ApplicationUsers.FindAsync(userId);
            return Ok(user);
        }
    }
}
