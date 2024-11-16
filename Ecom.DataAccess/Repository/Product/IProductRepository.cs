using Ecom.Models;

namespace Ecom.DataAccess.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product obj);
        void Save();
    }
}
