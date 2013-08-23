using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Repositories;
using Service;

namespace Web.App_Start {
  public static class AutofacRegisterComponents {
    public static void Start() {
      var builder = new ContainerBuilder();

      // Register this project
      builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());

      // Register Infrastructure
      builder.RegisterType<Data.Infrastructure.UnitOfWork>().As<Data.Infrastructure.IUnitOfWork>().InstancePerLifetimeScope();
      builder.RegisterType<Data.Infrastructure.DatabaseFactory>().As<Data.Infrastructure.IDatabaseFactory>().InstancePerLifetimeScope();

      // Register Repositories
      builder.RegisterAssemblyTypes(typeof (TodoRepository).Assembly)
             .Where(t => t.Name.EndsWith("Repository"))
             .AsImplementedInterfaces().InstancePerLifetimeScope();

      // Register Services
      builder.RegisterAssemblyTypes(typeof (TodoDataService).Assembly)
             .Where(t => t.Name.EndsWith("Service"))
             .AsImplementedInterfaces().InstancePerLifetimeScope();

      builder.RegisterFilterProvider();
      var container = builder.Build();
      DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
    }
  }
}