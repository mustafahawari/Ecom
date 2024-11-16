using System;
using System.Linq.Expressions;

namespace Ecom.DataAccess.Repository
{
	public interface IRepository<T> where T: class
	{
		IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter);
		T? Get(Expression<Func<T, bool>> filter);
		void Add(T entity);
		void Remove(T entity);
		void RemoveRange(IEnumerable<T> entities);
	}
}

