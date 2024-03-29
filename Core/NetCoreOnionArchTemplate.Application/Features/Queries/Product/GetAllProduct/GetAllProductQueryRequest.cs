﻿using MediatR;
using NetCoreOnionArchTemplate.Application.RequestParameters;

namespace NetCoreOnionArchTemplate.Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryRequest : IRequest<GetAllProductQueryResponse>
    {
        public Pagination _Pagination { get; set; }
        public GetAllProductQueryRequest(Pagination Pagination)
        {
            _Pagination = Pagination;
        }
    }
}
