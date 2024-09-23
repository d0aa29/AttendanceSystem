using System.Linq.Expressions;
using AttendanceSystem.Data;
using Microsoft.EntityFrameworkCore;
using Repository.IRepository;

namespace Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
      
        private readonly ApplicationDbContext _db;
		internal DbSet<T> dbSet;
		public Repository(ApplicationDbContext db)
		{
			_db = db;
			this.dbSet = _db.Set<T>();

		}
		public void Add(T entity)
		{
			dbSet.Add(entity);
		}

		public T Get(Expression<Func<T, bool>> Filter, string? includeproperties, bool tracked = false)
		{
			IQueryable<T> query;

            if (tracked)
			{
                query = dbSet;
				
			}
			else
			{
                query = dbSet.AsNoTracking(); 
            }
            query = query.Where(Filter);
            if (!string.IsNullOrEmpty(includeproperties))
            {
                foreach (var property in includeproperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }

            }

            return query.FirstOrDefault();
        }

		public IEnumerable<T> GetAll(Expression<Func<T, bool>>? Filter, string? includeproperties = null)
		{

			IQueryable<T> query = dbSet;
			if(Filter!=null)
            query = query.Where(Filter);
            if (!string.IsNullOrEmpty( includeproperties) )
			{
				foreach (var property in includeproperties
					.Split(new char[]{ ','} ,StringSplitOptions.RemoveEmptyEntries))
				{
					query=query.Include(property);
				}

			}
			return query;
	    }

		public void Remove(T entity)
		{
			dbSet.Remove(entity);
		}

		public void RemoveRange(IEnumerable<T> entity)
		{
			dbSet.RemoveRange(entity);
		}
	}
}
