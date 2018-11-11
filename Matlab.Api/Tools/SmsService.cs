using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Matlab.Logger;
using Microsoft.AspNet.Identity;

namespace Matlab.Api.Tools
{
	public class SmsService : IIdentityMessageService
	{
		public async Task SendAsync(IdentityMessage message)
		{
			LogThis.VerificationCode(message.Body, message.Destination);
		}
	}
}