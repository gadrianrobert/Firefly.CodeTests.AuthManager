using System.Collections.Generic;
using Firefly.CodeTests.AuthManager.Authentication;
using Firefly.CodeTests.AuthManager.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Firefly.CodeTests.AuthManager.Repository;
using Firefly.CodeTests.AuthManager.ResponseCode;
using Firefly.CodeTests.AuthManager.Validator;

namespace Firefly.CodeTests.AuthManager.UnitTests
{
	[TestClass]
	public class AuthManagerIntegrationTest
	{
		private const string TestUserName = "user@test.com";
		private const string TestPassword = "12QWaszx";
		private const string InvalidUserName = "invalid";
		private const string InvalidActiveDirectoryUserName = "domain\\user";
		private const string WrongPassword = "wrongpwd";

		private readonly User validUser = new User
		{
			UserName = TestUserName,
			Password = TestPassword
		};

		private IRepository<User, string> GetUserRepository()
		{
			return new UserRepository();
		}

		private IUserValidation<User> GetUserValidation()
		{
			return new UserValidation
			{
				PasswordValidators = new List<IValidator<User>> { new PasswordValidator() }
			};
		}

		private IHashProvider GetHashProvider()
		{
			return new HashProvider();
		}

		private IUserStore<User, string> GetUserStore()
		{
			return new UserStore(GetUserRepository(), GetUserValidation(), GetHashProvider());
		}

		private IUserAuthentication<User> GetUserAuthentication()
		{
			return new UserAuthentication(GetUserRepository(), GetHashProvider());
		}

		[TestInitialize]
		public void TestInitialize()
		{
			//TODO: implement DB cleanup

			var userStore = GetUserStore();
			userStore.Delete(TestUserName);
		}

		[TestMethod]
		public void CreateNewUser_UserNameValidationFails_1()
		{
			//TODO: implement a test where, during a new User creation, the UserName custom validation fails
			//	custom validation checks for UserName: should be a valid email address

			var userValidation = GetUserValidation();
			var userStore = GetUserStore();
			userValidation.UserNameValidators = new List<IValidator<User>> { new EmailValidator() };

			var response = userStore.Add(new User { UserName = InvalidUserName });

			Assert.IsTrue(response == UserStoreResponseCode.InvalidUserName);
		}

		[TestMethod]
		public void CreateNewUser_UserNameValidationFails_2()
		{
			//TODO: implement a test where, during a new User creation, the UserName custom validation fails
			//	custom validation checks for UserName: should be a valid active directory account (domain\username)

			var userValidation = GetUserValidation();
			var userStore = GetUserStore();
			userValidation.UserNameValidators = new List<IValidator<User>> { new ActiveDirectoryValidator() };

			var response = userStore.Add(new User { UserName = InvalidActiveDirectoryUserName });

			Assert.IsTrue(response == UserStoreResponseCode.InvalidUserName);
		}

		[TestMethod]
		public void CreateNewUser_Succeeds()
		{
			//TODO: implement a test where the creation succeed

			var userStore = GetUserStore();
			var response = userStore.Add(validUser);

			Assert.IsTrue(response == UserStoreResponseCode.Success);
		}

		[TestMethod]
		public void CreateNewUser_UserNameAlreadyExists()
		{
			//TODO: implement a test where the creation fails because a User with the same UserName already exists

			var userStore = GetUserStore();
			userStore.Add(validUser);
			var response = userStore.Add(validUser);

			Assert.IsTrue(response == UserStoreResponseCode.UserAlreadyExists);
		}

		[TestMethod]
		public void GetUser_Succeeds()
		{
			//TODO: implement a test that retrieve, using the UserName, the user created with the test CreateNewUser_Succeededs

			var userStore = GetUserStore();
			userStore.Add(validUser);
			var user = userStore.Find(validUser.UserName);


			Assert.IsNotNull(user);
		}

		[TestMethod]
		public void GetUser_Fails()
		{
			//TODO: implement a test that doesn't retrieve a User with a speficied UserName

			var userStore = GetUserStore();
			var user = userStore.Find(InvalidUserName);

			Assert.IsNull(user);
		}

		[TestMethod]
		public void AuthenticaUserCredentialsOk_Succeeds()
		{
			//TODO: implement a test in which an account is successfully authenticated against an exsisting user previously created in CreateNewUser_Succeededs

			var userStore = GetUserStore();
			var userAuthentication = GetUserAuthentication();
			userStore.Add(validUser);

			var response = userAuthentication.Authenticate(validUser);

			Assert.IsTrue(response == UserAuthenticationResponseCode.Success);
		}

		[TestMethod]
		public void AuthenticateUser_WrongPassword()
		{
			//TODO: implement a test in which user authentication fails bacause the password is wrong

			var userStore = GetUserStore();
			var userAuthentication = GetUserAuthentication();
			userStore.Add(validUser);

			var response = userAuthentication.Authenticate(new User { UserName = TestUserName, Password = WrongPassword });

			Assert.IsTrue(response == UserAuthenticationResponseCode.WrongPassword);
		}
	}
}
