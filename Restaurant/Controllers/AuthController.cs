using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Restaurant.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(IMediator mediator):ControllerBase
    {
        [HttpPost("register")]
        public
    }
}
