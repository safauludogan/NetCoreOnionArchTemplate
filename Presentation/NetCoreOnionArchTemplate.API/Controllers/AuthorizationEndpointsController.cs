using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreOnionArchTemplate.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorizationEndpointsController : ControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> AssignRole()
		{
			return Ok();
		}
	}
}
