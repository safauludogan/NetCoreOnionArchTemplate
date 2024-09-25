using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreOnionArchTemplate.Application.Consts;
using NetCoreOnionArchTemplate.Application.CustomAttributes;
using NetCoreOnionArchTemplate.Application.Enums;
using NetCoreOnionArchTemplate.Application.Features.Commands.Products.CreateProduct;
using NetCoreOnionArchTemplate.Application.Features.Commands.Products.DeleteProduct;
using NetCoreOnionArchTemplate.Application.Features.Commands.Products.UpdateProduct;
using NetCoreOnionArchTemplate.Application.Features.Queries.Products.GetAllProduct;
using NetCoreOnionArchTemplate.Application.Features.Queries.Products.GetProductById;
using NetCoreOnionArchTemplate.Application.RequestParameters;

namespace NetCoreOnionArchTemplate.API.Controllers
{
    [Route("api/[controller]")]
	[Authorize(AuthenticationSchemes = "Admin")]

	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ProductsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("[action]")]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Reading, Definition = "Get Products")]
		public async Task<IActionResult> GetAllProduct([FromQuery] Pagination pagination)
		{
			GetAllProductQueryResponse response = await _mediator.Send(new GetAllProductQueryRequest(pagination));
			return Ok(response);
		}

		[HttpGet("[action]/{id}")]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Reading, Definition = "Get Product ById")]
		public async Task<IActionResult> GetProductById(Guid id)
		{
			return Ok(await _mediator.Send(new GetProductByIdQueryRequest(id)));
		}

		[HttpPost("[action]")]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Writing, Definition = "Create a Products")]
		public async Task<IActionResult> CreateProduct([FromQuery] CreateProductCommandRequest request)
		{
			return Ok((await _mediator.Send(request)).IsSuccess);
		}
		[HttpPut("[action]")]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Updating, Definition = "Update Product")]
		public async Task<IActionResult> UpdateProduct(UpdateProductCommandRequest request)
		{
			return Ok(await _mediator.Send(request));
		}

		[HttpDelete("[action]/{id}")]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Deleting
			, Definition = "Delete Product")]
		public async Task<IActionResult> DeleteProduct(Guid id)
		{
			return Ok((await _mediator.Send(new DeleteProductCommandRequest(id))).IsSuccess);
		}
	}
}
