
using MediatR;

namespace Restaurant.Application.UserInfo.Commands.Remove_User_From_Role;

public class RemoveUserFromRoleCommand : IRequest
{
    public string Email { get; set; } = default!;
    public string RoleName { get; set; } = default!;
}
