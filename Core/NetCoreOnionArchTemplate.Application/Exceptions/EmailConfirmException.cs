
namespace NetCoreOnionArchTemplate.Application.Exceptions
{
    public class EmailConfirmException : Exception
    {
        public EmailConfirmException() : base("E-Posta doğrulanmadı!")
        {

        }
        public EmailConfirmException(string? message) : base(message)
        {
        }

        public EmailConfirmException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
