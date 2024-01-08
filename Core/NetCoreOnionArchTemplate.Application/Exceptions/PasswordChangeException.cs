namespace NetCoreOnionArchTemplate.Application.Exceptions
{
	public class PasswordChangeException : Exception
	{
		public PasswordChangeException() : base("Şifre güncellenirken bir sorun oluştu.")
		{
		}

		public PasswordChangeException(string? message) : base(message)
		{
		}

		public PasswordChangeException(string? message, Exception? innerException) : base(message, innerException)
		{
		}
	}
}
