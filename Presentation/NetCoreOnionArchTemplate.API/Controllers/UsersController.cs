using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetCoreOnionArchTemplate.Application.Abstractions.Services;
using NetCoreOnionArchTemplate.Application.Features.Commands.AppUser.CreateUser;
using NetCoreOnionArchTemplate.Application.Features.Commands.AppUser.UpdatePassword;

namespace NetCoreOnionArchTemplate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMailService _mailService;

		public UsersController(IMediator mediator, IMailService mailService)
		{
			_mediator = mediator;
			_mailService = mailService;
		}

		[HttpPost("[action]")]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest request)
        {
            CreateUserCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

		[HttpPost("[action]")]
		public async Task<IActionResult> UpdatePassword(UpdatePasswordCommandRequest request)
		{
			UpdatePasswordCommandResponse response = await _mediator.Send(request);
			return Ok(response);
		}
	}
}
