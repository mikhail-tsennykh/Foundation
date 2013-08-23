using System.ComponentModel.DataAnnotations.Schema;

namespace Model {
  [Table("TodoItem")]
  public class TodoItem : AuditableEntity {
    public int Id { get; set; }
    public string Name { get; set; }
    public TodoStatus Status { get; set; }
  }
}