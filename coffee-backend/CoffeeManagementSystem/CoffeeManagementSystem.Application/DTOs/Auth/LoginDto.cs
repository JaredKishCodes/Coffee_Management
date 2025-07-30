

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CoffeeManagementSystem.Application.DTOs.Auth
{
    public class LoginDto
    {
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = string.Empty;

    }
}
