using Data.Infrastructure;
using Model;

namespace Repositories {
  public interface ITodoItemRepository : IRepository<TodoItem> {}
  public class TodoItemRepository : RepositoryBase<TodoItem>, ITodoItemRepository {
    public TodoItemRepository(IDatabaseFactory databaseFactory) : base(databaseFactory) {}
  }
}