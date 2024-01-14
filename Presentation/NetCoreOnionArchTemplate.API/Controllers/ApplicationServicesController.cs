using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreOnionArchTemplate.Application.Abstractions.Services.Configurations;
using NetCoreOnionArchTemplate.Application.Consts;
using NetCoreOnionArchTemplate.Application.CustomAttributes;
using NetCoreOnionArchTemplate.Application.Enums;

namespace NetCoreOnionArchTemplate.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(AuthenticationSchemes = "Admin")]
	public class ApplicationServicesController : ControllerBase
	{

		private readonly IApplicationService _applicationService;

		public ApplicationServicesController(IApplicationService applicationService)
		{
			_applicationService = applicationService;
		}

		[HttpGet]
		[AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Authorize Definition Endpoints",
			Menu = AuthorizeDefinitionConstants.ApplicationServices)]
		public IActionResult GetAuthorizeDefinitionEndpoints()
		{
			var datas = _applicationService.GetAuthorizeDefinitionEndpoints(typeof(Program));
			return Ok(datas);
		}
	}
}
