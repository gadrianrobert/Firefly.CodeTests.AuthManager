using System.Collections.Generic;
using Firefly.CodeTests.AuthManager.Entity;
using Firefly.CodeTests.AuthManager.Validator;

namespace Firefly.CodeTests.AuthManager.Authentication
{
	public class UserValidation : IUserValidation<User>
	{
		public IEnumerable<IValidator<User>> UserNameValidators { get; set; } = new List<IValidator<User>> { new EmailValidator() };

		public IEnumerable<IValidator<User>> PasswordValidators { get; set; } = new List<IValidator<User>> { new PasswordValidator() };

		public bool IsValidUserName(User user)
		{
			return IsValid(user, UserNameValidators);
		}

		public bool IsValidPassword(User user)
		{
			return IsValid(user, PasswordValidators);
		}

		private static bool IsValid(User item, IEnumerable<IValidator<User>> validators)
		{
			foreach (var validator in validators)
			{
				var isValid = validator.Validate(item);
				if (!isValid) return false;
			}
			return true;
		}
	}
}
