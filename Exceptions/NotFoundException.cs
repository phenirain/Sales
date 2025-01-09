namespace Sales.Exceptions;

public class NotFoundException(string dbModel, long id): Exception($"{dbModel} with id: {id} not found")
{
}