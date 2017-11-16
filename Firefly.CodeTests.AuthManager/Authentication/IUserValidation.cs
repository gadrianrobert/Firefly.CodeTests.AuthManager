using System.Collections.Generic;
using Firefly.CodeTests.AuthManager.Validator;

namespace Firefly.CodeTests.AuthManager.Authentication
{
	public interface IUserValidation<TUser> where TUser : class
	{
		IEnumerable<IValidator<TUser>> UserNameValidators { get; set; }

		IEnumerable<IValidator<TUser>> PasswordValidators { get; set; }

		bool IsValidUserName(TUser user);

		bool IsValidPassword(TUser user);
	}
}
