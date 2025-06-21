
using MediatR;

namespace Restaurant.Application.Authentication.RegisterCommand;

public class RegisterCommand:IRequest<string>
{
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;    
}
