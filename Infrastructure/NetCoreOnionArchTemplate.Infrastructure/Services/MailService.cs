
using Microsoft.Extensions.Configuration;
using NetCoreOnionArchTemplate.Application.Abstractions.Services;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace NetCoreOnionArchTemplate.Infrastructure.Services
{
	public class MailService : IMailService
	{
		private readonly IConfiguration _configuration;

		public MailService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
		{
			await SendMailAsync(new[] { to }, subject, body, isBodyHtml);
		}

		public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
		{
			using (MailMessage mail = new MailMessage())
			{
				mail.IsBodyHtml = isBodyHtml;
				foreach (var to in tos)
					mail.To.Add(to);
				mail.Subject = subject;
				mail.Body = body;
				mail.From = new MailAddress(_configuration["Mail:Username"], "VibeBilisim", Encoding.UTF8);

				using (SmtpClient smtp = new SmtpClient())
				{
					smtp.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
					smtp.Port = 587;
					//smtp.EnableSsl = true;
					smtp.UseDefaultCredentials = false;
					smtp.Host = _configuration["Mail:Host"];
					await smtp.SendMailAsync(mail);
				}
			}
		}

		public async Task SendPasswordResetMailAsync(string to, Guid userId, string resetToken)
		{
            StringBuilder mail = new StringBuilder();
            mail.AppendLine("Merhaba,<br>");
            mail.AppendLine("Eğer yeni şifre talebinde bulunduysanız aşağıdaki linkten şifrenizi yenileyebilirsiniz:<br>");
            mail.AppendLine("<strong><a target=\"_blank\" href=\"");
            mail.Append(_configuration["ClientUrl"]);
            mail.Append("/update-password/{userId}/{resetToken}");
            mail.Replace("{userId}", userId.ToString());
            mail.Replace("{resetToken}", resetToken);
            mail.AppendLine("\">Yeni şifre talebi için tıklayınız...</a></strong><br><br>");
            mail.AppendLine("<span style=\"font-size:12px;\">NOT : Eğer ki bu talep tarafınızca gerçekleştirilmemişse lütfen bu maili ciddiye almayınız.</span><br>");
            mail.AppendLine("Saygılarımızla...<br><br><br>VibeBilişim");

            await SendMailAsync(to, "Şifre Yenileme Talebi", mail.ToString());

        }
	}
}
