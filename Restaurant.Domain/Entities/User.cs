

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Domain.Entities;

public class User:IdentityUser
{
    [Required]
    public string FullName { get; set; } = default!;
    public DateOnly? DateOfBirth { get; set; }
    public string? Nationality { get; set; }
}
