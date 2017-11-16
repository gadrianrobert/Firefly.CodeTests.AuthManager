namespace Firefly.CodeTests.AuthManager.Validator
{
	public interface IValidator<in TItem> where TItem: class
	{
		bool Validate(TItem item);
	}
}
