﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sales.Entities.DomainModels;

namespace Sales.Entities.DbModels;

public class SaleDbModel: BaseDbModel
{
    
    [Required]
    public DateOnly Date { get; set; }
    
    [Required]
    public TimeOnly Time { get; set; }
    
    [Required]
    public long SalesPointId { get; set; }
    
    public long? BuyerId { get; set; }
    
    public List<SaleDataDbModel> SaleData { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }
    
    // Navigation properties
    public virtual SalesPointDbModel SalesPointDbModel { get; set; }
    public virtual BuyerDbModel BuyerDbModel { get; set; }
}