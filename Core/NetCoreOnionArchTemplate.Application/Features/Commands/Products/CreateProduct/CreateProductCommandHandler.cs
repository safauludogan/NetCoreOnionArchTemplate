using MediatR;
using NetCoreOnionArchTemplate.Application.Abstractions.Hubs;
using NetCoreOnionArchTemplate.Application.Interfaces.UnitOfWorks;
using NetCoreOnionArchTemplate.Domain.Entities;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.Products.CreateProduct
{
	public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
	{
		private readonly IProductHubService _productHubService;
		private readonly IUnitOfWork _unitOfWork;
        public CreateProductCommandHandler(IProductHubService productHubService, IUnitOfWork unitOfWork)
        {
            _productHubService = productHubService;
            _unitOfWork = unitOfWork;
        }
        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
		{
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
