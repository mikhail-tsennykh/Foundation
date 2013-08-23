using System;

namespace Model {
  public abstract class AuditableEntity {
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string ModifiedBy { get; set; }
  }
}