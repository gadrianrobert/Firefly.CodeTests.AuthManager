namespace Firefly.CodeTests.AuthManager.Authentication
{
	public interface IHashProvider
	{
		string GetHash(string input);
	}
}
