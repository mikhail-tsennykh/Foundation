
## Foundation - .NET MVC boilerplate Visual Studio solution (alpha)

#### Introduction

.NET MVC boilerplate solution complete whith Unit-of-Work pattern, Autofac dependency injection, and one click-database setup incluing roles and memberships based on Altairis.Web.Security.

#### How to use

Just clone it, open solution, check connection string, run web site, then click 'Admin' > 'Initialize Database and Users'.
It will create default user 'admin' with password 'admin108' being a memeber of 'admin' role.

If you want to configure the default name/login/password modify 'SetupController.cs' > 'InitializeDatabase()' method.