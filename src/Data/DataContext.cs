using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using Data.Model;

namespace Data.Data {
  public class DataContext : DbContext {

    // data binding
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Brand> Brands { get; set; }

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