using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Authentication.Login_Command;
using Restaurant.Application.Authentication.RegisterCommand;

namespace Restaurant.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Authentication([FromBody] RegisterCommand command)
        {
            try
            {
                var token = await mediator.Send(command);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest( new{ error = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            try
            {
                var token = await mediator.Send(command);
                return Ok(new {Token = token});
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
