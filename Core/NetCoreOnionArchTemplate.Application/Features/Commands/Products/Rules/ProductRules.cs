using NetCoreOnionArchTemplate.Application.Base;
using NetCoreOnionArchTemplate.Application.Features.Commands.Products.Exceptions;
using NetCoreOnionArchTemplate.Domain.Entities;


namespace NetCoreOnionArchTemplate.Application.Features.Commands.Products.Rules
{
    public class ProductRules : BaseRules
    {
        public Task ProductNameMustNotBeSame(IList<Product> products, string requestName)
        {
            if (products.Any(x => x.Name == requestName)) throw new ProductNameMustNotBeSameException();
            return Task.CompletedTask;
        }
    }
}
