using System.ComponentModel.DataAnnotations;

namespace Sales.Services.Dtos.BusinessDtos.AuthDtos;

public class AuthRequest
{
    [Required(ErrorMessage = "BuyerId is required")]
    public long BuyerId { get; set; }
}