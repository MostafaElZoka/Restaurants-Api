
namespace Restaurant.Domain.Exceptions;

public class NotFoundExceptionHandler:Exception
{
    public NotFoundExceptionHandler(string resourceName, string ResourceIdentifier):base($"{resourceName} with id : {ResourceIdentifier} doesn't exist")
    {
           
    }
}
