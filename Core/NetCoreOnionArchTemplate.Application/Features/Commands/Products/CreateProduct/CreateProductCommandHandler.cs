using Bogus;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetCoreOnionArchTemplate.Application.Abstractions.Hubs;
using NetCoreOnionArchTemplate.Application.Abstractions.UnitOfWorks;
using NetCoreOnionArchTemplate.Application.Features.Commands.Products.Rules;
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

            #region Fake data creator
            Faker faker = new("tr");
            List<Product> products1 = new();
            for (int i = 0; i < 1000000; i++)
            {
                products1.Add(new()
                {
                    Name = faker.Commerce.Product(),
                    Price = faker.Random.Number(1, 10000),
                    Stock = faker.Random.Number(1, 50)
                });
            }
            await _unitOfWork.GetWriteRepository<Product>().AddRangeAsync(products1);
            #endregion

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
