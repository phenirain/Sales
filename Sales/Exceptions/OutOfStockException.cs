namespace Sales.Exceptions;

public class OutOfStockException(long id, int requested, int maxValue): Exception($"Product with id: {id} max value in stock: {maxValue}\nRequested: {requested}")
{
    
}