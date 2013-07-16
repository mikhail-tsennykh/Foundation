using System.Collections.Generic;

namespace Data.Model {
  public class Brand : AuditableEntity {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    // Non-GMO project specific
    public int? BrandId { get; set; }

    public virtual IList<Product> Products { get; set; }
  }
}