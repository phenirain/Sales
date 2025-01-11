using System.ComponentModel.DataAnnotations;

namespace Sales.Entities.DbModels;

public class BaseDbModel
{
    [Key]
    public long Id { get; set; }
}