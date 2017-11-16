using System.Security.Cryptography;
using System.Text;

namespace Firefly.CodeTests.AuthManager.Authentication
{
	public class HashProvider : IHashProvider
	{
		public string GetHash(string input)
		{
			var hashAlgorithm = MD5.Create();
			return Encoding.UTF8.GetString(hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input)));
		}
	}
}
