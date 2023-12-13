namespace NetCoreOnionArchTemplate.Application.Features.Commands.Product.DeleteProduct
{
    public class DeleteProductCommandResponse
    {
        public bool IsSuccess { get; set; }

        public DeleteProductCommandResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}
