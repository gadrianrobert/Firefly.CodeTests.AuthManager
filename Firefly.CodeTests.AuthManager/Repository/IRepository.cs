namespace Firefly.CodeTests.AuthManager.Repository
{
	public interface IRepository<TItem, in TKey> where TItem : class
	{
		void Add(TItem item);

		void Delete(TKey key);

		TItem Find(TKey key);
	}
}
