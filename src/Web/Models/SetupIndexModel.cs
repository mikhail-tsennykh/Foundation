namespace Web.Models {
  public class SetupIndexModel {
    public string DatabaseName { get; set; }
    public bool DatabaseExists { get; set; }
    public string ServerName { get; set; }
    public bool RolesTableExists { get; set; }
    public bool UsersTableExists { get; set; }
  }
}