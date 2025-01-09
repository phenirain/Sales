using Sales.Services.Dtos.BusinessDtos.AuthDtos;

namespace Sales.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> Authorize(AuthRequest request);
    Task<AuthResponse> RefreshToken(string token);
}