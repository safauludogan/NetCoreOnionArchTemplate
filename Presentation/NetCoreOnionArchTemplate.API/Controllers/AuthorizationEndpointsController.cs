using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreOnionArchTemplate.Application.Features.Commands.AuthorizationEndpoint.AssignRoleEndpoint;

namespace NetCoreOnionArchTemplate.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	[Authorize(AuthenticationSchemes = "Admin")]
	public class AuthorizationEndpointsController : ControllerBase
	{
		private readonly IMediator _mediator;

		public AuthorizationEndpointsController(IMediator mediator)
		{
			_mediator = mediator;
		}


		/*
		{
		  "roles": [
			"Create Product Yetkisi"
		  ],
		  "code": "POST.Writing.CreateaProducts",
		  "menu": "Products"
		}

		 */
		[HttpPost]
		public async Task<IActionResult> AssignRole([FromBody] AssignRoleEndpointCommandRequest request)
		{
			request.Type = typeof(Program);
			AssignRoleEndpointCommandResponse response = await _mediator.Send(request);
			return Ok(response);
		}
	}
}
