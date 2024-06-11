using System;
using Ecom.Models.Category;
namespace Ecom.DataAccess.Repository
{
	public interface ICategoryRepository: IRepository<Category>
	{
		void Update(Category obj);
		void Save();
	}
}

