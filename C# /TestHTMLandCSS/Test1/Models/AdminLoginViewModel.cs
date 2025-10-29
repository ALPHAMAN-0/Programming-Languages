using System.ComponentModel.DataAnnotations;

namespace Test1.Models
{
    public class AdminLoginViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; init; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; init; } = string.Empty;

        public AdminLoginViewModel()
        {
        }

        public AdminLoginViewModel(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}