using Sales.Entities.DomainModels;
using Sales.Exceptions;
using Sales.Repositories.Interfaces;
using Sales.Services.Dtos.BusinessDtos.AuthDtos;
using Sales.Services.Interfaces;
using Sales.Support;
using Sales.Support.Jwt;

namespace Sales.Services.Business;

public class AuthService: IAuthService
{
    private readonly ILogger<AuthService> _logger;
    private readonly IUnitOfWork _unitOfWork;
    
    public AuthService(IUnitOfWork unitOfWork, ILogger<AuthService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public async Task<AuthResponse> Authorize(AuthRequest request)
    {
        try
        {
            Buyer? buyer = await _unitOfWork.BuyerRepository.GetById(request.BuyerId);
            if (buyer == null)
                throw new NotFoundException(typeof(Buyer), request.BuyerId);
            (string, string) tokens = JwtHandler.CreateToken(buyer.Id);
            return new AuthResponse
            {
                Token = tokens.Item1,
                RefreshToken = tokens.Item2
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error authorizing buyer with id: {request.BuyerId}: {ex.Message}");
            throw;
        }
    }

    public AuthResponse RefreshToken(string token)
    {
        try
        {
            (string, string) tokens = JwtHandler.RefreshToken(token);
            return new AuthResponse
            {
                Token = tokens.Item1,
                RefreshToken = tokens.Item2
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error refreshing token: {ex.Message}");
            throw;
        }
    }
}