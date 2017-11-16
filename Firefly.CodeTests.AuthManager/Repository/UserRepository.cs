using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Firefly.CodeTests.AuthManager.Entity;

namespace Firefly.CodeTests.AuthManager.Repository
{
	public class UserRepository : IRepository<User, string>
	{
		private static readonly string connectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
		public void Add(User item)
		{
			using ( var connection = new SqlConnection(connectionString))
			{
				using (var command = new SqlCommand("spAddUser", connection) { CommandType = CommandType.StoredProcedure })
				{
					command.Parameters.AddWithValue("@UserName", item.UserName);
					command.Parameters.AddWithValue("@Password", item.Password);

					command.Connection.Open();

					command.ExecuteNonQuery();
				}
			}
		}

		public void Delete(string key)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				using (var command = new SqlCommand("spDeleteUser", connection) { CommandType = CommandType.StoredProcedure })
				{
					command.Parameters.AddWithValue("@UserName", key);

					command.Connection.Open();

					command.ExecuteNonQuery();
				}
			}
		}

		public User Find(string key)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				using (var command = new SqlCommand("spFindUser", connection) { CommandType = CommandType.StoredProcedure })
				{
					command.Parameters.AddWithValue("@UserName", key);

					command.Connection.Open();

					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							return new User
							{
								UserName = reader["UserName"] as string,
								Password = reader["Password"] as string,
							};
						}
					}
				}
			}
			return null;
		}
	}
}
