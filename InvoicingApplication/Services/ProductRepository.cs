using InvoicingApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace InvoicingApplication.Services
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(AppContext db) : base(db)
        {

        }

        public override void Update(Product ojb)
        {
            _db.Products.Attach(ojb);
            _db.Entry(ojb).State = EntityState.Modified;
            _db.Entry(ojb).Property(x => x.Created).IsModified = false;
        }
    }
}