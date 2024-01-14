using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreOnionArchTemplate.Application.Consts;
using NetCoreOnionArchTemplate.Application.CustomAttributes;
using NetCoreOnionArchTemplate.Application.Enums;
using NetCoreOnionArchTemplate.Application.Features.Commands.Role.CreateRole;
using NetCoreOnionArchTemplate.Application.Features.Commands.Role.UpdateRole;
using NetCoreOnionArchTemplate.Application.Features.Queries.Role.GetRoleById;
using NetCoreOnionArchTemplate.Application.Features.Queries.Role.GetRoles;

namespace NetCoreOnionArchTemplate.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(AuthenticationSchemes = "Admin")]
	public class RolesController : ControllerBase
	{

		private readonly IMediator _mediator;

		public RolesController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		[AuthorizeDefinition(ActionType = ActionType.Reading,Definition = "Get All Roles",Menu = AuthorizeDefinitionConstants.Roles)]
		//public async Task<IActionResult> GetAllRoles([FromQuery] GetAllRolesQueryRequest request)
		public async Task<IActionResult> GetAllRoles()
		{
			GetAllRolesQueryResponse roles = await _mediator.Send(new GetAllRolesQueryRequest());
			return Ok(roles);
		}

		[HttpGet("{id}")]
		[AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Role By Id", Menu = AuthorizeDefinitionConstants.Roles)]
		public async Task<IActionResult> GetRoleById([FromRoute]  GetRoleByIdQueryRequest request)
		{
			GetRoleByIdQueryResponse role = await _mediator.Send(request);
			return Ok(role);
		}

		[HttpPost]
		[AuthorizeDefinition(ActionType = ActionType.Writing, Definition = "Create Role", Menu = AuthorizeDefinitionConstants.Roles)]
		public async Task<IActionResult> CreateRole([FromBody]  CreateRoleCommandRequest request)
		{
			CreateRoleCommandResponse response = await _mediator.Send(request);
			return Ok(response);
		}

		[HttpPut("{Id}")]
		[AuthorizeDefinition(ActionType = ActionType.Updating, Definition = "Update Role", Menu = AuthorizeDefinitionConstants.Roles)]
		public async Task<IActionResult> UpdateRole([FromBody, FromRoute]  UpdateRoleCommandRequest request)
		{
			UpdateRoleCommandResponse response = await _mediator.Send(request);
			return Ok(response);
		}

		[HttpDelete("{name}")]
		[AuthorizeDefinition(ActionType = ActionType.Deleting, Definition = "Delete Role", Menu = AuthorizeDefinitionConstants.Roles)]
		public async Task<IActionResult> DeleteRole([FromRoute] UpdateRoleCommandRequest request)
		{
			UpdateRoleCommandResponse response = await _mediator.Send(request);
			return Ok(response);
		}
	}
}

