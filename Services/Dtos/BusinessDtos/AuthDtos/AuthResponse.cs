namespace Sales.Services.Dtos.BusinessDtos.AuthDtos;

public class AuthResponse
{
    public string JWTToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime TokenExpiredAt { get; set; }
}