using MediatR;
using Microsoft.EntityFrameworkCore;
using NetCoreOnionArchTemplate.Application.Abstractions.Hubs;
using NetCoreOnionArchTemplate.Application.Abstractions.UnitOfWorks;
using NetCoreOnionArchTemplate.Application.Features.Rules;
using NetCoreOnionArchTemplate.Domain.Entities;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.Products.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        private readonly IProductHubService _productHubService;
        private readonly ProductRules _productRules;
        private readonly IUnitOfWork _unitOfWork;
        public CreateProductCommandHandler(IProductHubService productHubService, ProductRules productRules, IUnitOfWork unitOfWork)
        {
            _productHubService = productHubService;
            _productRules = productRules;
            _unitOfWork = unitOfWork;
        }
        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            IList<Product> products = _unitOfWork.GetReadRepository<Product>().Table.AsNoTracking().Select(x => new Product { Name = x.Name }).ToList();

            await _productRules.ProductNameMustNotBeSame(products, request.Name!);

            Product product = new()
            {
                Name = request.Name!,
                Price = request.Price,
                Stock = request.Stock
            };

            await _unitOfWork.GetWriteRepository<Product>().AddAsync(product);

            var response = new CreateProductCommandResponse();
            response.IsSuccess = await _unitOfWork.SaveAsync() > 0 ? true : false;

            //Hub service ile client'a anlık olarak bildirim göndermek için kullanılır. 
            if (response.IsSuccess)
                await _productHubService.ProductAddedMessageAsync($"{request.Name} isminde ürün eklenmiştir");
            await _productHubService.ProductAddedMessageAsync($"{request.Name} ürünü eklenemedi");

            return response;
        }
    }
}
