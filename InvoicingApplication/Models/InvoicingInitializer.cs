using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoicingApplication.Models
{
    public class InvoicingInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<AppContext>
    {
        protected override void Seed(AppContext context)
        {
            var products = new List<Product>
            {
            new Product{Description="iPhone 10", Price=150.0, Created=DateTime.Now, Updated=DateTime.Now},
            new Product{Description="iPad Air", Price=200.0, Created=DateTime.Now, Updated=DateTime.Now},
            new Product{Description="MacBook Pro", Price=500.0, Created=DateTime.Now, Updated=DateTime.Now},
            };

            products.ForEach(s => context.Products.Add(s));
            context.SaveChanges();

            var customers = new List<Customer>
            {
            new Customer{FirstName="Marcio", Created=DateTime.Now, Updated=DateTime.Now},
            new Customer{FirstName="Nurgul Eren", Created=DateTime.Now, Updated=DateTime.Now},
            new Customer{FirstName="Felipe Prandini", Created=DateTime.Now, Updated=DateTime.Now},
            new Customer{FirstName="Thamy Prandini", Created=DateTime.Now, Updated=DateTime.Now},
            new Customer{FirstName="Ana Claudia", Created=DateTime.Now, Updated=DateTime.Now},
            };

            customers.ForEach(s => context.Customers.Add(s));
            context.SaveChanges();
        }
    }
}