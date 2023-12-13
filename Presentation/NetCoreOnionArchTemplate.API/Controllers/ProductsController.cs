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

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
        {
            GetAllProductQueryResponse response = await _mediator.Send(new GetAllProductQueryRequest(pagination));
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _mediator.Send(new GetProductByIdQueryRequest(id)));
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> Post([FromRoute] CreateProductCommandRequest request)
        {
            return Ok((await _mediator.Send(request)).IsSuccess);
        }
        [HttpPut]
        public async Task<IActionResult> Put(UpdateProductCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok((await _mediator.Send(new DeleteProductCommandRequest(id))).IsSuccess);
        }
    }
}
