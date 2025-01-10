namespace Sales.Services.Dtos.BusinessDtos.AuthDtos;

public class AuthResponse
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}