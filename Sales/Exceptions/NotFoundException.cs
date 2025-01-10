namespace Sales.Exceptions;

public class NotFoundException: Exception
{
    public NotFoundException(Type DbModel, long id) : base($"{DbModel} with id: {id} not found") {}
    
    public NotFoundException(string message): base(message){}
}