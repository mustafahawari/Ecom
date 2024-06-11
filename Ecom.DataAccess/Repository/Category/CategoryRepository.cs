using System;
using Ecom.DataAccess.Data;
using Ecom.Models.Category;

namespace Ecom.DataAccess.Repository
{
	public class CategoryRepository: Repository<Category>, ICategoryRepository
	{
        private readonly ApplicationDbContext _db;
		public CategoryRepository(ApplicationDbContext db) : base(db)
		{
            _db = db;
		}

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Category obj)
        {
            _db.Category.Update(obj);
        }
    }
}

