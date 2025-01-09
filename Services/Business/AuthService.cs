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
    private readonly IUnitOfWork _unitOfWork;
    
    public AuthService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<AuthResponse> Authorize(AuthRequest request)
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

    public AuthResponse RefreshToken(string token)
    {
        (string, string) tokens = JwtHandler.RefreshToken(token);
        return new AuthResponse
        {
            Token = tokens.Item1,
            RefreshToken = tokens.Item2
        };
    }
}