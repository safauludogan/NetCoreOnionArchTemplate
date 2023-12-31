﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreOnionArchTemplate.Application.Abstractions.Services.Configurations;

namespace NetCoreOnionArchTemplate.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ApplicationServicesController : ControllerBase
	{

		private readonly IApplicationService _applicationService;

		public ApplicationServicesController(IApplicationService applicationService)
		{
			_applicationService = applicationService;
		}

		[HttpGet]
		public IActionResult GetAuthorizeDefinitionEndpoints()
		{
			var datas = _applicationService.GetAuthorizeDefinitionEndpoints(typeof(Program));
			return Ok(datas);
		}
	}
}
