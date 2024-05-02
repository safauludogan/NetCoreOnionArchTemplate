namespace NetCoreOnionArchTemplate.Application.Abstractions.Services
{
	public interface IMailService
	{
		Task SendMailAsync(string to, string subject,string body,
			bool isBodyHtml = true);
		Task SendMailAsync(string[] to, string subject, string body,
			bool isBodyHtml = true);

		Task SendPasswordResetMailAsync(string to, Guid userId, string resetToken);
	}
}
