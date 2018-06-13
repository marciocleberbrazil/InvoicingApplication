using InvoicingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoicingApplication.Services
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private AppContext context = new AppContext();
        private ProductRepository productRepository;
        private CustomerRepository customerRepository;

        public ProductRepository ProductRepository
        {
            get
            {

                if (this.productRepository == null)
                {
                    this.productRepository = new ProductRepository(context);
                }
                return productRepository;
            }
        }

        public CustomerRepository CustomerRepository
        {
            get
            {

                if (this.customerRepository == null)
                {
                    this.customerRepository = new CustomerRepository(context);
                }
                return customerRepository;
            }
        }


        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}