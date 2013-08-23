using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model {
  [Table("Todo")]
  public class Todo : AuditableEntity {
    public int Id { get; set; }
    public string Name { get; set; }
    
    public virtual IList<TodoItem> TodoItems { get; set; }
  }
}