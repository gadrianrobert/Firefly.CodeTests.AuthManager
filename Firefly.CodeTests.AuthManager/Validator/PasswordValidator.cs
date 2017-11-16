using Firefly.CodeTests.AuthManager.Entity;

namespace Firefly.CodeTests.AuthManager.Validator
{
	public class PasswordValidator : IValidator<User>
	{
		public int MinimumLength { get; set; }
		public bool Validate(User item)
		{
			return item.Password?.Length >= MinimumLength;
		}
	}
}
