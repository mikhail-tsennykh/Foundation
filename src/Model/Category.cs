using System.Collections.Generic;

namespace Data.Model {
  public class Category : AuditableEntity {
    public int Id { get; set; }
    public string Name { get; set; }

    public virtual IList<Product> Products { get; set; }
  }
}