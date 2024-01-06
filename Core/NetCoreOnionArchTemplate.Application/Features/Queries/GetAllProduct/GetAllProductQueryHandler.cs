using MediatR;
using Microsoft.Extensions.Logging;
using NetCoreOnionArchTemplate.Application.Repositories;

namespace NetCoreOnionArchTemplate.Application.Features.Queries.GetAllProduct
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly ILogger<GetAllProductQueryHandler> _logger;
		public GetAllProductQueryHandler(IProductReadRepository productReadRepository, ILogger<GetAllProductQueryHandler> logger)
		{
			_productReadRepository = productReadRepository;
			_logger = logger;
		}

		public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get all products");
			var totalCount = _productReadRepository.GetAll().Count();
            var products = _productReadRepository.GetAll(tracking: false)
            .Skip(request._Pagination.Page * request._Pagination.Size).Take(request._Pagination.Size).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate
            }).ToList();

            return new()
            {
                Products = products,
                TotalCount = totalCount
            };
        }
    }
}
