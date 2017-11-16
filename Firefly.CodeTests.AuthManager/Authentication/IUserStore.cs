using Firefly.CodeTests.AuthManager.ResponseCode;

namespace Firefly.CodeTests.AuthManager.Authentication
{
	public interface IUserStore<TUser, in TKey> where TUser : class
	{
		UserStoreResponseCode Add(TUser user);

		bool Delete(TKey key);

		TUser Find(TKey key);
	}
}
