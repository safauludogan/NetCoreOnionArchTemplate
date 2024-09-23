using NetCoreOnionArchTemplate.Application.Base;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.Products.Exceptions
{
    public class ProductNameMustNotBeSameException : BaseException
    {
        public ProductNameMustNotBeSameException() : base("Ürün adı zaten var!") { }
    }
}
