using System.ComponentModel.DataAnnotations;

namespace Test1.Models
{
    public class Admin
    {
        public int Id { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; init; } = string.Empty;
        
        [Required]
        [MinLength(6)]
        public string Password { get; init; } = string.Empty;

        public Admin()
        {
        }

        public Admin(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}