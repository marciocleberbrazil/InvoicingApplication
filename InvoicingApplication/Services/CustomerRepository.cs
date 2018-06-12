using InvoicingApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace InvoicingApplication.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        public void Delete(int id)
        {
            using (var context = new AppContext())
            {
                Customer customer = context.Customers.Find(id);
                context.Customers.Remove(customer);
                context.SaveChanges();
            }
        }

        public void Save(Customer item)
        {
            using (var context = new AppContext())
            {
                if (item.CustomerId > 0)
                {
                    item.Updated = DateTime.Now;

                    context.Customers.Attach(item);
                    context.Entry(item).State = EntityState.Modified;
                    context.Entry(item).Property(x => x.Created).IsModified = false;
                }
                else
                {
                    item.Created = DateTime.Now;
                    item.Updated = DateTime.Now;
                    context.Customers.Add(item);
                }

                context.SaveChanges();
            }
        }

        public Customer GetById(int? id)
        {
            using (var context = new AppContext())
            {
                return context.Customers.Find(id);
            }
        }

        public List<Customer> GetAll()
        {
            using (var context = new AppContext())
            {
                return context.Customers.ToList();
            }
        }
    }
}