using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Constants;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurants.Commands.Upload_Logo;

internal class UploadLogoCommandHandler(ILogger<UploadLogoCommandHandler> logger, IRestaurantRepository restaurantRepository,
    IRestaurantAuthorizationServices restaurantAuthorizationServices, IBlobStrorageService blobStrorageService) : IRequestHandler<UploadLogoCommand>
{
    public async Task Handle(UploadLogoCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Uploading logo for Restaurant with id {restaurantId}", request.RestaurantId);

        var restaurant = await restaurantRepository.GetById(request.RestaurantId);

        if (restaurant == null)
        {
            //return false;
            throw new NotFoundExceptionHandler(nameof(Restaurantt), request.RestaurantId.ToString());
        }

        if (!restaurantAuthorizationServices.Authorize(restaurant, ResourceOperations.Update)) //if the user is not the owner of the restaurant
            throw new ForbidException();

        var logoURL = await blobStrorageService.UploadToBlobAsync(request.File, request.FileName);
        restaurant.LogoUrl = logoURL;
        await restaurantRepository.SaveChanges();
    }
}
