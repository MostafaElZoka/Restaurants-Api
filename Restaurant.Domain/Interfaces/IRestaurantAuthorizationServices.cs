using Restaurant.Domain.Constants;
using Restaurant.Domain.Entities;

namespace Restaurant.Domain.Interfaces

{
    public interface IRestaurantAuthorizationServices
    {
        bool Authorize(Restaurantt restaurant, ResourceOperations resourceOperations);
    }
}