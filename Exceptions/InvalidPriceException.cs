namespace Sales.Exceptions;

public class InvalidPriceException(): Exception("Invalid Price\nPrice must be more than 0")
{
    
}