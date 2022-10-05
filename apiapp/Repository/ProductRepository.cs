using apiapp.Interfaces;
using apiapp.Model;
using apiapp.Repository;

namespace apiapp
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(IMongoContext context) : base(context)
        {
        }
    }
}
