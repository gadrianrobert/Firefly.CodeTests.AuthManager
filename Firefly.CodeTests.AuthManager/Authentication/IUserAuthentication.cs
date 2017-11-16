using Firefly.CodeTests.AuthManager.ResponseCode;

namespace Firefly.CodeTests.AuthManager.Authentication
{
	public interface IUserAuthentication<in TUser> where TUser : class
	{
		UserAuthenticationResponseCode Authenticate(TUser user);
	}
}
