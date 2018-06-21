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
                new Product{Description="Aero Graphic Black/Steel Grey", Price=35, Created=DateTime.Now, Updated=DateTime.Now},
                new Product{Description="Top Navy", Price=25, Created=DateTime.Now, Updated=DateTime.Now},
                new Product{Description="Urban Basic Grey/Dark Brown", Price=50, Created=DateTime.Now, Updated=DateTime.Now},
                new Product{Description="Rubber Logo Black/Blue", Price=30, Created=DateTime.Now, Updated=DateTime.Now},
                new Product{Description="Teams England White/Navy", Price=35, Created=DateTime.Now, Updated=DateTime.Now},
                new Product{Description="Metal Logo Black/Silver", Price=40, Created=DateTime.Now, Updated=DateTime.Now},
            };

            products.ForEach(s => context.Products.Add(s));
            context.SaveChanges();

            var customers = new List<Customer>
            {
                new Customer{FirstName="Marcio", LastName="Barboza", Address="7b Mount Street", City="Sydney", State="NSW", PostCode="2205", Created=DateTime.Now, Updated=DateTime.Now},
                new Customer{FirstName="Nurgul Eren", LastName="Eren", Address="501 King Street", City="Sydney", State="NSW", PostCode="2005", Created=DateTime.Now, Updated=DateTime.Now},
                new Customer{FirstName="Felipe", LastName="Prandini", Address="300 Clearence Street", City="Sydney", State="NSW", PostCode="2200", Created=DateTime.Now, Updated=DateTime.Now},
                new Customer{FirstName="Thamy", LastName="Prandini", Address="123 Church Street", City="Sydney", State="NSW", PostCode="2005", Created=DateTime.Now, Updated=DateTime.Now},
                new Customer{FirstName="Ana Claudia", LastName="Reis", Address="153b Harris Street", City="Sydney", State="NSW", PostCode="2000", Created=DateTime.Now, Updated=DateTime.Now},
            };

            customers.ForEach(s => context.Customers.Add(s));
            context.SaveChanges();
        }
    }
}