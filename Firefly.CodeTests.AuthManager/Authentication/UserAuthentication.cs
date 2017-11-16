using System.Collections.Generic;
using Firefly.CodeTests.AuthManager.Entity;
using Firefly.CodeTests.AuthManager.Repository;
using Firefly.CodeTests.AuthManager.ResponseCode;
using Firefly.CodeTests.AuthManager.Validator;

namespace Firefly.CodeTests.AuthManager.Authentication
{
	public class UserAuthentication : IUserAuthentication<User>
	{
		private readonly IRepository<User, string> userRepository;

		private readonly IHashProvider hashProvider;

		public IEnumerable<IValidator<User>> Validators { get; set; } = new List<IValidator<User>>() { new EmailValidator(), new PasswordValidator() };

		public UserAuthentication(IRepository<User, string> userRepository, IHashProvider hashProvider)
		{
			this.userRepository = userRepository;
			this.hashProvider = hashProvider;
		}

		public UserAuthenticationResponseCode Authenticate(User user)
		{
			var foundUser = userRepository.Find(user.UserName);

			if (foundUser == null) return UserAuthenticationResponseCode.WrongUserName;

			if(foundUser.Password != hashProvider.GetHash(user.Password)) return UserAuthenticationResponseCode.WrongPassword;

			return UserAuthenticationResponseCode.Success;
		}
	}
}
