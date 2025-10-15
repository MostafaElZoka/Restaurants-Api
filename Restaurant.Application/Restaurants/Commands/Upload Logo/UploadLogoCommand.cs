using MediatR;

namespace Restaurant.Application.Restaurants.Commands.Upload_Logo;

public class UploadLogoCommand : IRequest
{
    public int RestaurantId { get; set; }
    public string FileName { get; set; } = default!;
    public Stream File { get; set; } = default!;
}
