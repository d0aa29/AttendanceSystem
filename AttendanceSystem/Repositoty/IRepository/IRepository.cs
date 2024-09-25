using System.Linq.Expressions;

namespace Repository.IRepository
{
	public interface IRepository<T> where T : class
	{

        Task<List<T>> GetAll(Expression<Func<T, bool>>? fillter = null,
          string? includProperties = null);

        Task<T> Get(Expression<Func<T, bool>> fillter = null, bool tracked = true,
            string? includProperties = null);
        Task Create(T entity);

        Task Remove(T entity);
        Task Save();
    }
}
