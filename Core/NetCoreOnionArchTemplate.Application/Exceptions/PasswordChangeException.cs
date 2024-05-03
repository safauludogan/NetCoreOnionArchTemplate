namespace NetCoreOnionArchTemplate.Application.Exceptions
{
	public class PasswordChangeException : Exception
	{
		public PasswordChangeException() : base("Şifre güncellenirken bir sorun oluştu. Link'in süresi dolmuş olabilir.")
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
