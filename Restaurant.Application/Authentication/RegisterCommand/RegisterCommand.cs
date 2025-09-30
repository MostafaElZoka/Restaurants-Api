
using MediatR;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Application.Authentication.RegisterCommand;

public class RegisterCommand:IRequest<string>
{
    [Required]
    public string FullName { get; set; } = default!;
    [EmailAddress]
    public string Email { get; set; } = default!;
    [PasswordPropertyText]
    public string Password { get; set; } = default!;    
}
