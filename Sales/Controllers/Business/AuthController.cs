using Microsoft.AspNetCore.Mvc;
using Sales.Services.Dtos.BusinessDtos.AuthDtos;
using Sales.Services.Interfaces;

namespace Sales.Controllers.Business;

[ApiController]
[Route("auth")]
public class AuthController: ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost]
    public async Task<AuthResponse> Authrorize(AuthRequest authRequest)
    {
        return await _authService.Authorize(authRequest);
    }
    
    [HttpPost("/refresh")]
    public AuthResponse RefreshToken(string token)
    {
        return _authService.RefreshToken(token);
    }
}