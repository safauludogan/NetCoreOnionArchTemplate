namespace NetCoreOnionArchTemplate.Application.Features.Commands.DeleteProduct
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
