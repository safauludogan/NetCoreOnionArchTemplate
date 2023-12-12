using Microsoft.AspNetCore.Mvc;
using NetCoreOnionArchTemplate.Application.Repositories;
using NetCoreOnionArchTemplate.Application.RequestParameters;
using NetCoreOnionArchTemplate.Application.ViewModels.Products;
using NetCoreOnionArchTemplate.Domain.Entities;
using System.Net;

namespace NetCoreOnionArchTemplate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;

        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
        {
            var totalCount = _productReadRepository.GetAll().Count();
            var products = _productReadRepository.GetAll(tracking: false)
                .Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Price,
                    p.CreatedDate,
                    p.UpdatedDate
                }).ToList();

            return Ok(new { products, totalCount });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _productReadRepository.GetByIdAsync(id, tracking: false));
        }

        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product model)
        {
            Product product = new()
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock
            };
            await _productWriteRepository.AddAsync(product);
            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
            Product product = await _productReadRepository.GetByIdAsync(model.Id);
            product.Stock = model.Stock;
            product.Price = model.Price;
            product.Name = model.Name;
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }
    }
}
