using System.Linq;
using Data.Data.Infrastructure;
using Data.Model;

namespace Data.Data.Repositories {
  public interface IProductRepository : IRepository<Product> {
    Product Get(string name);
  }
  public class ProductRepository : RepositoryBase<Product>, IProductRepository {
    public ProductRepository(IDatabaseFactory databaseFactory) : base(databaseFactory) {}
    public Product Get(string name) {
      return DataContext.Products.SingleOrDefault(x => x.Name == name);
    }
  }
}