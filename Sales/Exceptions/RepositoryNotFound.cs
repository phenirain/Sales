namespace Sales.Exceptions;

public class RepositoryNotFound(string className): Exception($"Repository for classname: {className} not found")
{
    
}