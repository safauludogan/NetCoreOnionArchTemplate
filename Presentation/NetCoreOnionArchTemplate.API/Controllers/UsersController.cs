using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreOnionArchTemplate.API.Filters;
using NetCoreOnionArchTemplate.Application.Consts;
using NetCoreOnionArchTemplate.Application.CustomAttributes;
using NetCoreOnionArchTemplate.Application.Enums;
using NetCoreOnionArchTemplate.Application.Features.Commands.Auth.AssignRoleToUser;
using NetCoreOnionArchTemplate.Application.Features.Commands.Auth.CreateUser;
using NetCoreOnionArchTemplate.Application.Features.Commands.Auth.UpdatePassword;
using NetCoreOnionArchTemplate.Application.Features.Queries.AppUser.GetAllUsers;
using NetCoreOnionArchTemplate.Application.Features.Queries.AppUser.GetRolesToUser;
using NetCoreOnionArchTemplate.Application.Features.Queries.Auth.GetAllUsers;

namespace NetCoreOnionArchTemplate.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Admin")]
    [Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IMediator _mediator;

		public UsersController(IMediator mediator)
		{
			_mediator = mediator;
		}

        [ApiKeyAuthFilter]
		[AllowAnonymous]
        [HttpPost("[action]")]
		public async Task<IActionResult> CreateUser(CreateUserCommandRequest request)
		{
			CreateUserCommandResponse response = await _mediator.Send(request);
			return Ok(response);
		}

        [ApiKeyAuthFilter]
        [AllowAnonymous]
        [HttpPost("[action]")]
		public async Task<IActionResult> UpdatePassword(UpdatePasswordCommandRequest request)
		{
			UpdatePasswordCommandResponse response = await _mediator.Send(request);
			return Ok(response);
		}

		[HttpGet("[action]")]
		[AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get All Users", Menu = AuthorizeDefinitionConstants.Users)]
		public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersQueryRequest request)
		{
			GetAllUsersQueryResponse response = await _mediator.Send(request);
			return Ok(response);
		}

		[HttpGet("[action]/{userId}")]
		[AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Roles To User", Menu = AuthorizeDefinitionConstants.Users)]
		public async Task<IActionResult> GetRolesToUser([FromRoute] GetRolesToUserQueryRequest request)
		{
			GetRolesToUserQueryResponse response = await _mediator.Send(request);
			return Ok(response);
		}

		[HttpPost("[action]")]
		[AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Assign Role To User", Menu = AuthorizeDefinitionConstants.Users)]
		public async Task<IActionResult> AssignRoleToUser([FromBody] AssignRoleToUserCommandRequest request)
		{
			AssignRoleToUserCommandResponse response = await _mediator.Send(request);
			return Ok(response);
		}
	}
}
