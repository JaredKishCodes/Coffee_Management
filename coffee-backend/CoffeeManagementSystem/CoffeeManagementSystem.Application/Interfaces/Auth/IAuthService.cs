

using CoffeeManagementSystem.Application.DTOs.Auth;

namespace CoffeeManagementSystem.Application.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<LoginResponse> LoginAsync(LoginDto loginDto);
    }
}
