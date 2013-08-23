using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using Data;
using Data.Infrastructure;
using Web.Models;

namespace Web.Controllers {
  public class SetupController : Controller {
    private readonly IUnitOfWork _unitOfWork;
    public SetupController(IUnitOfWork unitOfWork) {
      _unitOfWork = unitOfWork;
    }

    public ActionResult Index() {
      var model = new SetupIndexModel();
      using (var context = new DataContext()) {
        if (context.Database.Exists()) {
          context.Database.Connection.Open();

          model.ServerName = context.Database.Connection.DataSource;
          model.DatabaseName = context.Database.Connection.Database;
          model.DatabaseExists = true;

          // Check if Roles and Users tables exist in the database
          var rolesExist = context.Database.SqlQuery<int>
            ("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Roles'").ToList();
          var usersExist = context.Database.SqlQuery<int>
            ("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Users'").ToList();

          model.RolesTableExists = rolesExist[0] > 0;
          model.UsersTableExists = usersExist[0] > 0;

          context.Database.Connection.Close();
        }
      }
      return View(model);
    }

    public ActionResult InitializeDatabase() {
      // create base administrator
      var admin = "admin";
      var adminPw = "admin108";
      var appPath = HttpContext.Request.PhysicalApplicationPath;
      var message = new StringBuilder();

      using (var context = new DataContext()) {
        // Create database if not exists
        if (!context.Database.Exists()) {
          // Initializer must be set in Global.asax before creating database, e.g.:
          //Database.SetInitializer(new CreateDatabaseIfNotExists<DataContext>());
          context.Database.Create();
          message.AppendLine("Database created successfully!");
        }
        else
          message.AppendLine("Database already exists.");

        // Create Altairis web security tables
        var usersSql = System.IO.File.ReadAllText(appPath + "_setup/AltairisDb.sql");
        if (!string.IsNullOrEmpty(usersSql))
          context.Database.ExecuteSqlCommand(usersSql);

        // Create roles
        if (!Roles.RoleExists(admin)) Roles.CreateRole(admin);

        // Create base users
        if (Membership.GetUser(admin) == null) {
          // Create admin user
          MembershipCreateStatus membershipCreateStatus;
          Membership.CreateUser
            (admin, adminPw, "m108@outlook.com",
             "Home city?", "voronezh", true, out membershipCreateStatus);

          // Add users to roles
          if (!Roles.IsUserInRole(admin)) Roles.AddUserToRole(admin, admin);

          message.AppendLine("Admin user added!");
          TempData.Add("AdminLogin", "Admin Login: " + admin);
          TempData.Add("AdminPassword", "Admin Password: " + adminPw);
        }
        else
          message.AppendLine("Admin user already exists.");
      }

      TempData.Add("Message", message.ToString());

      return RedirectToAction("Index");
    }

    public ContentResult GetDatabaseSchema() {
      return Content(_unitOfWork.GetDatabaseSchema());
    }

    public ActionResult DeleteDatabase() {
      using (var context = new DataContext()) {
        if (context.Database.Exists()) {
          try {
            context.Database.Delete();
            TempData.Add("Message", "Database was deleted successfully.");
          }
          catch (Exception ex) {
            TempData.Add("Message", ex.Message);
          }
        }
        else
          TempData.Add("Message", "Database does not exist!");
      }
      return RedirectToAction("Index");
    }

  }
}