using System;
using Firefly.CodeTests.AuthManager.Entity;
using Firefly.CodeTests.AuthManager.Repository;
using Firefly.CodeTests.AuthManager.ResponseCode;
using Firefly.CodeTests.AuthManager.Utility;

namespace Firefly.CodeTests.AuthManager.Authentication
{
	public class UserStore : IUserStore<User, string>
	{
		private readonly IRepository<User, string> userRepository;
		private readonly IUserValidation<User> userValidation;
		private readonly IHashProvider hashProvider;

		public UserStore(IRepository<User, string> userRepository, IUserValidation<User> userValidation, IHashProvider hashProvider)
		{
			this.userRepository = userRepository;
			this.userValidation = userValidation;
			this.hashProvider = hashProvider;
		}
		public UserStoreResponseCode Add(User user)
		{
			if (!userValidation.IsValidUserName(user)) return UserStoreResponseCode.InvalidUserName;
			if (!userValidation.IsValidPassword(user)) return UserStoreResponseCode.InvalidPassword;
			if(Find(user.UserName) != null) return UserStoreResponseCode.UserAlreadyExists;

			try
			{
				userRepository.Add(new User
				{
					UserName = user.UserName,
					Password = hashProvider.GetHash(user.Password)
				});
			}
			catch (Exception ex)
			{
				Logger.Instance.LogException(ex);
				return UserStoreResponseCode.Error;
			}
			return UserStoreResponseCode.Success;
		}

		public bool Delete(string key)
		{
			try
			{
				userRepository.Delete(key);
			}
			catch (Exception ex)
			{
				Logger.Instance.LogException(ex);
				return false;
			}
			return true;
		}

		public User Find(string key)
		{
			try
			{
				return userRepository.Find(key);
			}
			catch (Exception ex)
			{
				Logger.Instance.LogException(ex);
				return null;
			}
		}
	}
}
