using System.Linq;
using Data.Data.Infrastructure;
using Data.Model;

namespace Data.Data.Repositories {
  public interface IBrandRepository : IRepository<Brand> {
    Brand Get(string name);
  }
  public class BrandRepository : RepositoryBase<Brand>, IBrandRepository {
    public BrandRepository(IDatabaseFactory databaseFactory) : base(databaseFactory) {}
    public Brand Get(string name) {
      return DataContext.Brands.SingleOrDefault(x => x.Name == name);
    }
  }
}