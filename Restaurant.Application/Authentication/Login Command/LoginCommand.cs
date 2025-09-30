
using MediatR;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Application.Authentication.Login_Command;

public class LoginCommand:IRequest<string>
{
    [EmailAddress]
    [Required]
    public string Email { get; set; } = default!;
    [Required]
    [PasswordPropertyText]
    public string Password { get; set; } = default!;
}
