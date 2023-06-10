using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Application.Email.Models
{
	public class MailMessage
	{
		public MailMessage(IDictionary<string, string> to, string subject, string body)
		{
			To = new List<MailboxAddress>();
			To.AddRange(to.Select(recipient => new MailboxAddress(recipient.Key, recipient.Value)));
			Subject = subject;
			Body = body;
		}
		public List<MailboxAddress> To { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
	}
}
