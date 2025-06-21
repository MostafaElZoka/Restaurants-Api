
using MediatR;

namespace Restaurant.Application.Authentication.Login_Command;

public class LoginCommand:IRequest<string>
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}
