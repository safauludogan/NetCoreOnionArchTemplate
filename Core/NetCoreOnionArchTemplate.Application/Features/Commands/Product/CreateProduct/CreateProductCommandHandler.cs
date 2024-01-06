using MediatR;
using NetCoreOnionArchTemplate.Application.Abstractions.Hubs;
using NetCoreOnionArchTemplate.Application.Repositories;


namespace NetCoreOnionArchTemplate.Application.Features.Commands.Product.CreateProduct
{
	public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
	{
		private readonly IProductWriteRepository _productWriteRepository;
		private readonly IProductHubService _productHubService;

		public CreateProductCommandHandler(IProductWriteRepository productWriteRepository, IProductHubService productHubService)
		{
			_productWriteRepository = productWriteRepository;
			_productHubService = productHubService;
		}
		public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
		{
			Domain.Entities.Product product = new()
			{
				Name = request.Name,
				Price = request.Price,
				Stock = request.Stock
			};
			await _productWriteRepository.AddAsync(product);
			var response = new CreateProductCommandResponse();
			response.IsSuccess = await _productWriteRepository.SaveAsync() > 0 ? true : false;

			//Hub service ile client'a anlık olarak bildirim göndermek için kullanılır. 
			if (response.IsSuccess)
				await _productHubService.ProductAddedMessageAsync($"{request.Name} isminde ürün eklenmiştir");
			await _productHubService.ProductAddedMessageAsync($"{request.Name} ürünü eklenemedi");

			return response;
		}
	}
}
