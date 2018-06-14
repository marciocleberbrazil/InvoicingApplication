using InvoicingApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace InvoicingApplication.Services
{
    public class OrderLineRepository : RepositoryBase<OrderLine>, IOrderLineRepository
    {
        public OrderLineRepository(AppContext db) : base(db)
        {

        }

        public override void Update(OrderLine ojb)
        {
            _db.OrderLines.Attach(ojb);
            _db.Entry(ojb).State = EntityState.Modified;
            _db.Entry(ojb).Property(x => x.Created).IsModified = false;
        }
    }
}