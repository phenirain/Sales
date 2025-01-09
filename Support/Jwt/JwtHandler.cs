using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Sales.Exceptions;

namespace Sales.Support.Jwt;

public class JwtHandler
{
    public static (string, string) CreateToken(long buyerId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JwtSecret")!);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new("BuyerId", buyerId.ToString())
            }),
            Audience = Environment.GetEnvironmentVariable("JwtAudience")!,
            Issuer = Environment.GetEnvironmentVariable("JwtIssuer"),
            Expires = DateTime.UtcNow.AddDays(int.Parse(Environment.GetEnvironmentVariable("JwtExpTime")!)),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var refreshTokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new("BuyerId", buyerId.ToString())
            }),
            Audience = Environment.GetEnvironmentVariable("JwtAudience")!,
            Issuer = Environment.GetEnvironmentVariable("JwtIssuer"),
            Expires = DateTime.UtcNow.AddDays(int.Parse(Environment.GetEnvironmentVariable("JwtRefreshExpTime")!)),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var refreshToken = tokenHandler.CreateToken(refreshTokenDescriptor);
        return (tokenHandler.WriteToken(token), tokenHandler.WriteToken(refreshToken));
    }

    public static ClaimsPrincipal? ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JwtSecret")!);
        try
        {
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true
            }, out SecurityToken validatedToken);
            if (validatedToken is JwtSecurityToken jwtToken &&
                jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                return principal;
            return null;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public static (string, string) RefreshToken(string refreshToken)
    {
        var principal = ValidateToken(refreshToken);
        if (principal == null)
            throw new InvalidTokenException("Invalid refresh token");
        var idString = principal.Claims.FirstOrDefault(c => c.Type == "BuyerId")?.Value;
        if (string.IsNullOrEmpty(idString))
            throw new InvalidTokenException("Invalid refresh token");
        return CreateToken(long.Parse(idString));
    }
}