using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetCoreOnionArchTemplate.Application.Features.Commands.Auth.LoginUser;
using NetCoreOnionArchTemplate.Application.Features.Commands.Auth.PasswordReset;
using NetCoreOnionArchTemplate.Application.Features.Commands.Auth.RefreshTokenLogin;
using NetCoreOnionArchTemplate.Application.Features.Commands.Auth.VerifyResetToken;

namespace NetCoreOnionArchTemplate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> RefreshTokenLogin([FromQuery] RefreshTokenLoginCommandRequest request)
        {
            RefreshTokenLoginCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

		[HttpPost("[action]")]
		public async Task<IActionResult> PasswordReset([FromBody] PasswordResetCommandRequest request)
		{
			PasswordResetCommandResponse response = await _mediator.Send(request);
			return Ok(response);
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> VerifyResetToken([FromBody] VerifyResetTokenCommandRequest request)
		{
			VerifyResetTokenCommandResponse response = await _mediator.Send(request);
			return Ok(response);
		}
	}
}
