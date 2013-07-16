using System.Linq;
using Data.Data.Infrastructure;
using Data.Model;

namespace Data.Data.Repositories {
  public interface ICategoryRepository : IRepository<Category> {
    Category Get(string name);
  }
  public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository {
    public CategoryRepository(IDatabaseFactory databaseFactory) : base(databaseFactory) {}
    public Category Get(string name) {
      return DataContext.Categories.SingleOrDefault(x => x.Name == name);
    }
  }
}