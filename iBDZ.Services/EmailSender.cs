using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iBDZ.Services
{
	public class AuthMessageSenderOptions
	{
		public string SendGridUser { get; set; }
		public string SendGridKey { get; set; }
	}

	public class EmailSender : IEmailSender
	{
		public EmailSender(IOptions<AuthMessageSenderOptions> options)
		{
			Options = options.Value;
		}
		
		public AuthMessageSenderOptions Options { get; }	

		public Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			var client = new SendGridClient(Options.SendGridKey);
			var msg = new SendGridMessage()
			{
				From = new EmailAddress("test@example.com", "iBDZ (PracticallyNothing)"),
				Subject = subject,
				PlainTextContent = htmlMessage,
				HtmlContent = htmlMessage
			};
			msg.AddTo(new EmailAddress(email));
			msg.SetClickTracking(false, false);

			Task<Response> response = client.SendEmailAsync(msg);
			return response;
		}
	}
}
