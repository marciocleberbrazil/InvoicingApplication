using InvoicingApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace InvoicingApplication.Services
{
    public class ProductRepository : IProductRepository
    {
        public void Delete(int id)
        {
            using (var context = new AppContext())
            {
                Product product = context.Products.Find(id);
                context.Products.Remove(product);
                context.SaveChanges();
            }
        }

        public void Save(Product item)
        {
            using (var context = new AppContext())
            {
                if (item.ProductId > 0)
                {
                    item.Updated = DateTime.Now;

                    context.Products.Attach(item);
                    context.Entry(item).State = EntityState.Modified;
                    context.Entry(item).Property(x => x.Created).IsModified = false;
                }
                else
                {
                    item.Created = DateTime.Now;
                    item.Updated = DateTime.Now;
                    context.Products.Add(item);
                }

                context.SaveChanges();
            }
        }

        public Product GetById(int? id)
        {
            using (var context = new AppContext())
            {
                return context.Products.Find(id);
            }
        }

        public List<Product> GetAll()
        {
            using (var context = new AppContext())
            {
                return context.Products.ToList();
            }
        }
    }
}