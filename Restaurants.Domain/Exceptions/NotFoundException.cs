namespace Restaurants.Domain.Exceptions;

public class NotFoundException(string resourceType, string resourceIentifier) : Exception($"{resourceType} with Id: {resourceIentifier} doesn't exist")
{
}
