using HumanResourcesProject.Application.Email.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Application.Email.Services
{
	public interface IEmailService
	{
		void SendEmail(MailMessage mailMessage);
	}
}
