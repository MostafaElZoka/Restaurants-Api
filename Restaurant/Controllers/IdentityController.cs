using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.UserInfo.Commands;
using Restaurant.Application.UserInfo.Commands.Remove_User_From_Role;
using Restaurant.Application.UserInfo.Commands.Update_User_Role;
using Restaurant.Domain.Constants;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController(IMediator mediator) : ControllerBase
    {
        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpPost("userRole")]
        [Authorize(Roles =UserRoles.Admin)]
        public async Task<IActionResult> ChangeUserRole(UpdateUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("userRole")]
        [Authorize(Roles =UserRoles.Admin)]
        public async Task<IActionResult> RemoveFromRole(RemoveUserFromRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }
    }
}
