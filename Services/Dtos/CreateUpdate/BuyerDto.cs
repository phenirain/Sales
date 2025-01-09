using System.ComponentModel.DataAnnotations;

namespace Sales.Services.Dtos.CreateUpdate;

public class BuyerDto
{
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }
}

