using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HabbitTrackerRodrigo.Models
{
    public class ApplicationUser // IdentityUser serve para indicar ao Entity Framework que essa classe é uma classe de usuário
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime Birthday { get; set; }
        public string Role { get; set; }


    }
}
