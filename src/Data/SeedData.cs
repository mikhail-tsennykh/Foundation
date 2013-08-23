using System;
using System.Data.Entity;
using Model;

namespace Data {
  public class SeedData : CreateDatabaseIfNotExists<DataContext> {
    protected override void Seed(DataContext context) {
      base.Seed(context);

      //var product = new Product();
      //product.Name = "Test";
      //product.CreatedDate = DateTime.Now;
      //product.CreatedBy = "system";
      //product.ModifiedDate = product.CreatedDate;
      //product.ModifiedBy = product.CreatedBy;

      //context.Products.Add(product);
      //context.Commit();

    }
    public void SeedDb(DataContext context) {
      Seed(context);
    }
  }
}