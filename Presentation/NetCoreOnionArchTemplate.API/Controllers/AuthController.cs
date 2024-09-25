using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreOnionArchTemplate.API.Filters;
using NetCoreOnionArchTemplate.Application.Features.Commands.Auth.LoginUser;
using NetCoreOnionArchTemplate.Application.Features.Commands.Auth.PasswordReset;
using NetCoreOnionArchTemplate.Application.Features.Commands.Auth.RefreshTokenLogin;
using NetCoreOnionArchTemplate.Application.Features.Commands.Auth.Revoke;
using NetCoreOnionArchTemplate.Application.Features.Commands.Auth.RevokeAll;
using NetCoreOnionArchTemplate.Application.Features.Commands.Auth.VerifyResetToken;

namespace NetCoreOnionArchTemplate.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Admin")]
    [ApiKeyAuthFilter]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> RefreshTokenLogin([FromQuery] RefreshTokenLoginCommandRequest request)
        {
            RefreshTokenLoginCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
		public async Task<IActionResult> PasswordReset([FromBody] PasswordResetCommandRequest request)
		{
			PasswordResetCommandResponse response = await _mediator.Send(request);
			return Ok(response);
		}

        [AllowAnonymous]
        [HttpPost]
		public async Task<IActionResult> VerifyResetToken([FromBody] VerifyResetTokenCommandRequest request)
		{
			VerifyResetTokenCommandResponse response = await _mediator.Send(request);
			return Ok(response);
		}
        
        [HttpPost]
		public async Task<IActionResult> Revoke([FromBody] RevokeCommandRequest request)
		{
			await _mediator.Send(request);
			return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> RevokeAll()
        {
            await _mediator.Send(new RevokeAllCommandRequest());
            return Ok();
        }
    }
}
