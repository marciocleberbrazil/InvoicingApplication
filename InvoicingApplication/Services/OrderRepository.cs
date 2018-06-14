using InvoicingApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace InvoicingApplication.Services
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(AppContext db) : base(db)
        {

        }

        public override void Update(Order ojb)
        {
            _db.Orders.Attach(ojb);
            _db.Entry(ojb).State = EntityState.Modified;
            _db.Entry(ojb).Property(x => x.Created).IsModified = false;
        }
    }
}