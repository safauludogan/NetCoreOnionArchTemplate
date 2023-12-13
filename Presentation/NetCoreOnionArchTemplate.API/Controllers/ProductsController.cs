using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreOnionArchTemplate.Application.Features.Commands.Product.CreateProduct;
using NetCoreOnionArchTemplate.Application.Features.Commands.Product.DeleteProduct;
using NetCoreOnionArchTemplate.Application.Features.Commands.Product.UpdateProduct;
using NetCoreOnionArchTemplate.Application.Features.Queries.GetAllProduct;
using NetCoreOnionArchTemplate.Application.Features.Queries.GetProductById;
using NetCoreOnionArchTemplate.Application.RequestParameters;

namespace NetCoreOnionArchTemplate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllProduct([FromQuery] Pagination pagination)
        {
            GetAllProductQueryResponse response = await _mediator.Send(new GetAllProductQueryRequest(pagination));
            return Ok(response);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            return Ok(await _mediator.Send(new GetProductByIdQueryRequest(id)));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateProduct([FromQuery] CreateProductCommandRequest request)
        {
            return Ok((await _mediator.Send(request)).IsSuccess);
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return Ok((await _mediator.Send(new DeleteProductCommandRequest(id))).IsSuccess);
        }
    }
}
