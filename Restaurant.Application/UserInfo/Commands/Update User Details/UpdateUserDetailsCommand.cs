using MediatR;

namespace Restaurant.Application.UserInfo.Commands;

public class UpdateUserDetailsCommand:IRequest
{
    public DateOnly? DateOfBirth {  get; set; }
    public string? Nationality { get; set; }
}
