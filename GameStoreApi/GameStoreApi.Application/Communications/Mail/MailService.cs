using GameStoreApi.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System;

namespace GameStoreApi.Application.Communications.Mail
{
	public class MailService
	{
		private readonly AppDbContext context;
        public MailService(AppDbContext context)
        {
            this.context = context;
        }
		public async Task SendMessage(int clientId, string subject, string body)
		{
			var user = await context.Users.SingleOrDefaultAsync(u => u.Id == clientId);

			var toAddress = new MailAddress(user.Email);
			var fromAddress = new MailAddress("");
			MailMessage message = new(fromAddress, toAddress)
			{
				Subject = subject,
				Body = user.Username + ", " + body,
				BodyEncoding = Encoding.UTF8,
				IsBodyHtml = true
			};

			SmtpClient client = new("smtp.mail.ru", 587);
			NetworkCredential basicCredemtial = new(fromAddress.Address, "");

			client.EnableSsl = true;
			client.UseDefaultCredentials = false;
			client.Credentials = basicCredemtial;

			try
			{
				client.Send(message);
			}
			catch (Exception exception)
			{
				throw exception;
			}
		}
	}
}
