using System;
using Ecom.Models;
namespace Ecom.DataAccess.Repository
{
	public interface ICategoryRepository: IRepository<Category>
	{
		void Update(Category obj);
		void Save();
	}
}

