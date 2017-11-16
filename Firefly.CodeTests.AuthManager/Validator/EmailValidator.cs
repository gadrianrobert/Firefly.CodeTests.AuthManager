using System;
using System.Net.Mail;
using Firefly.CodeTests.AuthManager.Entity;
using Firefly.CodeTests.AuthManager.Utility;

namespace Firefly.CodeTests.AuthManager.Validator
{
	public class EmailValidator : IValidator<User>
	{
		public bool Validate(User item)
		{
			try
			{
				new MailAddress(item.UserName);
			}
			catch (FormatException ex)
			{
				Logger.Instance.LogException(ex);
				return false;
			}
			return true;
		}
	}
}
