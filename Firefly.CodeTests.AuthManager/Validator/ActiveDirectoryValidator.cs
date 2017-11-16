using System;
using System.DirectoryServices.AccountManagement;
using Firefly.CodeTests.AuthManager.Entity;
using Firefly.CodeTests.AuthManager.Utility;

namespace Firefly.CodeTests.AuthManager.Validator
{
	public class ActiveDirectoryValidator : IValidator<User>
	{
		public bool Validate(User item)
		{
			var parts = item.UserName.Split(new[] {"\\"}, StringSplitOptions.None);
			if (parts.Length != 2) return false;
			var domainUser = new {Domain = parts[0], UserName = parts[1], item.Password};

			try
			{
				using (var context = new PrincipalContext(ContextType.Domain, domainUser.Domain))
				{
					return context.ValidateCredentials(domainUser.UserName, domainUser.Password);
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.LogException(ex);
				return false;
			}
		}
	}
}