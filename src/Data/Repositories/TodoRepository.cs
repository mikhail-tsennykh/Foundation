using System.Linq;
using Data.Infrastructure;
using Model;

namespace Repositories {
  public interface ITodoRepository : IRepository<Todo> {
    Todo Get(string name);
  }
  public class TodoRepository : RepositoryBase<Todo>, ITodoRepository {
    public TodoRepository(IDatabaseFactory databaseFactory) : base(databaseFactory) {}
    public Todo Get(string name) {
      return DataContext.Todos.SingleOrDefault(x => x.Name == name);
    }
  }
}