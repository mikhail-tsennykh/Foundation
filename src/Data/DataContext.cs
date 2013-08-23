using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using Model;

namespace Data {
  public class DataContext : DbContext {

    // data binding
    public DbSet<Todo> Todos { get; set; }
    public DbSet<TodoItem> TodoItems { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder) {
      base.OnModelCreating(modelBuilder);
    }

    // other methods
    public virtual void Commit() {
      try {
        base.SaveChanges();
      }
      catch (DbEntityValidationException ex) {
        var sb = new StringBuilder();
        var currentTitle = string.Empty;
        foreach (var entityValidationResult in ex.EntityValidationErrors.ToList()) {
          if (!entityValidationResult.IsValid) {
            if (currentTitle != entityValidationResult.Entry.Entity.ToString()) {
              currentTitle = entityValidationResult.Entry.Entity.ToString();
              sb.AppendLine(currentTitle);
              foreach (var dbValidationError in entityValidationResult.ValidationErrors) {
                sb.AppendLine("Property: " + dbValidationError.PropertyName +
                              "; Error: " + dbValidationError.ErrorMessage);
              }
            }
          }
        }
        throw new DbEntityValidationException("Entity Validation Exception!\n\n" + sb);
      }
    }
    public string DumpScript() {
      return ((IObjectContextAdapter) this).ObjectContext.CreateDatabaseScript();
    }

  }
}