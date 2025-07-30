
using CoffeeManagementSystem.Application.DTOs.Auth;


namespace CoffeeManagementSystem.Application.Interfaces.Auth
{
    public interface IJwtTokenService
    {
        Task<string> CreateTokenAsync(UserDto user, List<string> roles); // Fixed parameter name

    }
}
