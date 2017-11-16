namespace Firefly.CodeTests.AuthManager.ResponseCode
{
	public enum UserStoreResponseCode
	{
		Success = 0,
		Error,
		InvalidUserName,
		InvalidPassword,
		UserAlreadyExists
	}
}
