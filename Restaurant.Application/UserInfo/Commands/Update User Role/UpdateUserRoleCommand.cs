
using MediatR;

namespace Restaurant.Application.UserInfo.Commands.Update_User_Role;

public class UpdateUserRoleCommand : IRequest
{
    public string Email { get; set; } = default!; //the email of the user we want to assign the role to
    public string RoleName { get; set; } = default!;
}
