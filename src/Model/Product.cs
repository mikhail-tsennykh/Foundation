namespace Data.Model {
  public class Product : AuditableEntity {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Gmo { get; set; }
    public bool Organic { get; set; }
    public Status Status { get; set; }
    public string Image { get; set; }

    // Non-GMO project specific
    public int? ProductId { get; set; }
    public string Package { get; set; }
    public string UnfiUpc { get; set; }
    public string Ean { get; set; }

    public virtual Brand Brand { get; set; }
    public virtual Category Category { get; set; }
  }
}